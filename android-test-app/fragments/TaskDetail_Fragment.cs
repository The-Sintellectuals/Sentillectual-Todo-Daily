using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android_test_app.otherCs;
using Android.Support.V7.Widget;
using android_test_app.Adapters;

namespace android_test_app.fragments
{
    public class TaskDetail_Fragment : DialogFragment
    {
        // ----------- Initialization -----------
        Task task;
        Context todo_context;
        RecyclerView recyclerView;
        RecyclerSubTaskAdapter subAdapter;
        RecyclerView.LayoutManager layoutManager;


        ImageButton exitBtn;

        TextView DueDate;
        DateTime selectedDate;



        // ----------- Constructor -----------
        public TaskDetail_Fragment(Task task, Context context)
        {
            // todo_context = context;
            this.task = task;
        }



        // ----------- Overrides -----------
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetStyle(DialogFragment.StyleNormal, Resource.Style.FullscreenDialogTheme);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.TaskDetail_layout, container, false);

            view.FindViewById<TextView>(Resource.Id.Task_Name).Text = task.TaskName;

            // view.FindViewById<TextView>(Resource.Id.description).Text = 

            // ------- date picker dialog -------
            DueDate = view.FindViewById<TextView>(Resource.Id.dueDate);
            DueDate.Click += DueDate_Click;


            // ------- Recycler Sub Task View -------
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.subTask_viewpager);
            recyclerView.NestedScrollingEnabled = false;

            layoutManager = new LinearLayoutManager(view.Context);

            recyclerView.SetLayoutManager(layoutManager);

            subAdapter = new RecyclerSubTaskAdapter(task.childTask());

            recyclerView.SetAdapter(subAdapter);


            // ------- exit btn -------
            exitBtn = view.FindViewById<ImageButton>(Resource.Id.exitTaskDetail);
            exitBtn.Click += ExitBtn_Click;

            


            return view;
        }


        //public override void OnDismiss(IDialogInterface dialog)
        //{
        //    base.OnDismiss(dialog);
        //    Toast.MakeText(todo_context, "detail view is destroyed", ToastLength.Long).Show();
        //}



        // ----------- Other functions -----------
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Dismiss();
        }

        private void DueDate_Click(object sender, EventArgs e)
        {
            new DatePicker_Fragment(delegate (DateTime time)
            {
                selectedDate = time;
                DueDate.Text = time.ToLongDateString();
            }).Show(FragmentManager, "Date Picker Dialog");
        }

    }
}