using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using android_test_app.MyActionMode;
using android_test_app.otherCs;
using System;
using System.Collections.Generic;
using android_test_app.fragments;
using System.Linq;
using System.Text;
using ActionMode = Android.Views.ActionMode;
using Android.Graphics;

namespace android_test_app.Adapters
{
    internal class RecyclerAdapter : RecyclerView.Adapter
    {
        // -------------- Initialization --------------
        public List<Task> taskList { get; set; }
        RecyclerView recyclerView;
        Context context;
        public View todos_view;
        private Todo_ActionMode mActionMode;
        private ActionMode actionMode;

        // Events
        public event EventHandler<Task> TaskClicked;
        public event EventHandler<bool> freezeLayout;



        // -------------- Constructor --------------
        public RecyclerAdapter(List<Task> taskList, RecyclerView recyclerView, View view)
        {
            this.taskList = taskList;           // Used for displaying the task related details
            this.recyclerView = recyclerView;   // Used to get the reference of the item in a Recycler View
            context = view.Context;             // Mostly used in Toasts
            todos_view = view;                  // Used to add the Action Mode to the View
        }



        // -------------- View Holder Class --------------
        public class myViewHolder : RecyclerView.ViewHolder
        {
            public TextView taskTitle { get; private set; }
            public TextView taskDate { get; private set; }
            public LinearLayout cardView { get; private set; }
            public View mainView { get; set; }

            public myViewHolder(View itemView) : base(itemView)
            {
                mainView = itemView;
                taskTitle = itemView.FindViewById<TextView>(Resource.Id.taskTitle);
                taskDate = itemView.FindViewById<TextView>(Resource.Id.taskDate);
                cardView = itemView.FindViewById<LinearLayout>(Resource.Id.cardLayout);
            }
        }



        // -------------- Overrides --------------
        public override int ItemCount => taskList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            myViewHolder vh = holder as myViewHolder;
            Task taskClicked = taskList[position];

            vh.mainView.Click -= onClick;
            vh.mainView.Click += onClick;

            vh.mainView.LongClick -= test_longClick;
            vh.mainView.LongClick += test_longClick;
            
            vh.taskTitle.Text = taskClicked.TaskName;
            vh.taskDate.Text = taskClicked.date.ToString();

            vh.cardView.SetBackgroundColor(taskList[position].isSelected ? Color.LightGreen : Color.White);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.taskCard_layout, parent, false);

            // Create a View Holder
            myViewHolder holder = new myViewHolder(view);

            return holder;
        }



        // -------------- Other Functions --------------
        private void test_longClick(object sender, View.LongClickEventArgs e)
        {
            if (actionMode == null)
            {
                mActionMode = new Todo_ActionMode();
                actionMode = todos_view.StartActionMode(mActionMode);
                mActionMode.CloseActionBar -= MActionMode_CloseActionBar;
                mActionMode.CloseActionBar += MActionMode_CloseActionBar;
                freezeLayout?.Invoke(this, true);
            }
        }

        private void MActionMode_CloseActionBar(object sender, bool e)
        {
            if (actionMode != null)
            {
                actionMode.Finish();
                actionMode = null;
                freezeLayout?.Invoke(this, false);

                // Deselect selected tasks
                for (int i = 0; i < taskList.Count; i++)
                {
                    if (taskList[i].isSelected) taskList[i].isSelected = false;
                }
                NotifyDataSetChanged();
            }
        }

        void onClick(Object sender, EventArgs e)
        {
            int position = recyclerView.GetChildAdapterPosition((View)sender);
            if (actionMode != null)
            {
                // Multi select option
                // Toast.MakeText(this.context, "action mode is visible", ToastLength.Long).Show();
                taskList[position].isSelected = !taskList[position].isSelected;
                NotifyDataSetChanged();
            }
            else
            {
                // Open detail Option
                // Toast.MakeText(this.context, "action mode is invisible", ToastLength.Long).Show();
                TaskClicked?.Invoke(this, taskList[position]);
            }

        }
    }
}