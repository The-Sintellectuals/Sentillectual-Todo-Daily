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

namespace android_test_app.fragments
{
    public class TaskDetail_Fragment : DialogFragment
    {
        Task task;
        RecyclerView recyclerView;

        public TaskDetail_Fragment(Task task)
        {
            this.task = task;
        }

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

            // view.FindViewById<TextView>(Resource.Id.dueDate).Text = 

            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.subTask_viewpager);
            recyclerView.NestedScrollingEnabled = false;

            return view;
        }
    }
}