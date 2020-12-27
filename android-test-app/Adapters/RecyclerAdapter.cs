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
        List<Task> taskList;
        RecyclerView recyclerView;
        Context context;
        todos_fragment parent_fragment;
        public View mActivity;
        private Todo_ActionMode mActionMode;
        private ActionMode actionMode;



        // -------------- Constructor --------------
        public RecyclerAdapter(List<Task> taskList, RecyclerView recyclerView, View view, todos_fragment fragment)
        {
            this.taskList = taskList;
            this.recyclerView = recyclerView;
            context = view.Context;
            mActivity = view;
            parent_fragment = fragment;
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
                mActionMode = new Todo_ActionMode(mActivity.Context, this);
                actionMode = mActivity.StartActionMode(mActionMode);
                parent_fragment.freeze_layout(true);
            }
        }

        public void FinishActionMode()
        {
            if (actionMode != null)
            {
                actionMode.Finish();
                actionMode = null;
                parent_fragment.freeze_layout(false);
            }
        }

        void onClick(Object sender, EventArgs e)
        {
            View mview = (View)sender;
            int position = recyclerView.GetChildAdapterPosition(mview);
            if (actionMode != null)
            {
                Toast.MakeText(this.context, "action mode is visible", ToastLength.Long).Show();
                taskList[position].isSelected = !taskList[position].isSelected;
                NotifyDataSetChanged();
            }
            else
            {
                // Toast.MakeText(this.context, "action mode is invisible", ToastLength.Long).Show();
                MainActivity.mainInstance.showTaskDetail_Dialog(taskList[position]);
            }
                
        }

    }
}