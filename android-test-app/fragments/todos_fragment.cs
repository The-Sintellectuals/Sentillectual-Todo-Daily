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
using Filter = android_test_app.otherCs.Filter;
using android_test_app.ItemDecoration;
using Android.Support.Design.Widget;

namespace android_test_app.fragments
{
    public class todos_fragment : Fragment
    {
        // ------------ Initialization ------------
        // Temp Database Initialization
        List<Task> taskList = new List<Task>();
        List<Filter> filterList = new List<Filter>();

        // RecyclerView Initialization
        private RecyclerView recyclerView, recyclerViewFilter;
        private RecyclerView.Adapter mAdapter, mAdapterFilter;
        private RecyclerView.LayoutManager layoutManager, layoutManagerFilter;

        

        // ------------ Overrides ------------
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

        } // Unused

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.todos_layout, container, false);

            // Add entries to temp task list
            taskList = fillTaskList();
            filterList = fillFilterList();

            // ----RecyclerView Code----
            // RecyclerView Assignment
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.TasksLists);
            recyclerViewFilter = view.FindViewById<RecyclerView>(Resource.Id.FilterLists);

            // Layout Manager Assignment
            layoutManager = new LinearLayoutManager(view.Context);
            layoutManagerFilter = new LinearLayoutManager(view.Context, LinearLayoutManager.Horizontal, false);

            recyclerView.SetLayoutManager(layoutManager);
            recyclerViewFilter.SetLayoutManager(layoutManagerFilter);
            
            // Task Decoration Assignment
            TaskDecoration taskDecoration = new TaskDecoration(view.Context, 12, 9, 12, 9);
            FilterDecoration filterDecoration = new FilterDecoration(10, 0, 5, 2, view.Context);

            recyclerView.AddItemDecoration(taskDecoration);
            recyclerViewFilter.AddItemDecoration(filterDecoration);
            
            // Recycler Adapter Assignment
            mAdapter = new RecyclerAdapter(taskList, recyclerView, view, this);
            mAdapterFilter = new RecyclerAdapterFilter(filterList,  view);
            
            recyclerView.SetAdapter(mAdapter);
            recyclerViewFilter.SetAdapter(mAdapterFilter);

            


            return view;
        }



        // ------------ Other Functions ------------
        public void freeze_layout(bool freeze)
        {
            MainActivity main_act = (MainActivity)this.Activity;
            if (freeze)
            {
                // hide bottom navbar
                main_act.actionModeActivated(true);         // Lock Fragment
            }
            else
            {
                // Unhide bottom navbar
                main_act.actionModeActivated(false);        // Unlock Fragment
            }
        }

        public void onTaskCreation_Click()
        {
            FragmentTransaction trans = FragmentManager.BeginTransaction();
            var fragment = new TaskCreation_Dialog();
            fragment.Show(trans, "Dialog Fragment");
        }

        private List<Filter> fillFilterList()
        {
            List<Filter> mfilterList = new List<Filter> {
                new Filter("All", 0),
                new Filter("Today", 1),
                new Filter("7 Days", 2),
                new Filter("Completed", 3),
                new Filter("School", 4)
            };

            return mfilterList;
        }

        private List<Task> fillTaskList()
        {
            List<Task> mtaskList = new List<Task> {
                new Task(0, "play games", 2000),
                new Task(1, "Feed the dogs", 2002),
                new Task(2, "Make an app", 2004),
                new Task(3, "Filipino Quipper", 2050),
                new Task(4, "Participate in a Marathon", 2100),
                new Task(5, "Finish the app", 2004),
                new Task(6, "Empty my tank", 2152),
                new Task(7, "Refill my tank", 2006),
                new Task(8, "Feed the dogs", 2000),
                new Task(4, "Participate in a Marathon", 2100),
                new Task(3, "Filipino Quipper", 2050),
                new Task(1, "Play games", 2015),
                new Task(3, "Filipino Quipper", 2050)
            };

            return mtaskList;
        }

    }
}