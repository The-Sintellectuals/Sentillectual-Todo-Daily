using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using android_test_app.otherCs;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android_test_app.fragments;

namespace android_test_app.Adapters
{
    class RecyclerSubTaskAdapter : RecyclerView.Adapter
    {
        // -------------- Initialization --------------
        List<Task> subTaskList;


        // -------------- Constructor --------------
        public RecyclerSubTaskAdapter(List<Task> subTaskList)
        {
            this.subTaskList = subTaskList;
        }


        // -------------- ViewHolder Class --------------
        public class subTaskViewHolder : RecyclerView.ViewHolder
        {
            public CheckBox checkbox { get; private set; }
            public CheckedTextView cb_text { get; private set; }
            public View mainView { get; set; }

            public subTaskViewHolder (View itemView) : base(itemView)
            {
                mainView = itemView;
                checkbox = itemView.FindViewById<CheckBox>(Resource.Id.ch_box);
                cb_text = itemView.FindViewById<CheckedTextView>(Resource.Id.cb_text);
            }
        }


        // -------------- Overrides --------------
        public override int ItemCount => subTaskList.Count();

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            subTaskViewHolder vh = holder as subTaskViewHolder;

            vh.checkbox.Checked = subTaskList[position].completed;
            vh.cb_text.Text = subTaskList[position].TaskName;

            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.subTask_layout, parent, false);

            // Create a View Holder
            subTaskViewHolder holder = new subTaskViewHolder(view);

            return holder;
        }


    }
}