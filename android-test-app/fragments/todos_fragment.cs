using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using System;
using System.Collections.Generic;
using Toast = Android.Widget.Toast;
using System.Linq;
using System.Text;
using android_test_app.otherCs;
using Android.Support.V7.Widget;
using android_test_app.Adapters;
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
        private RecyclerAdapter mAdapter;
        private RecyclerAdapterFilter mAdapterFilter;
        private RecyclerView.LayoutManager layoutManager, layoutManagerFilter;

        // Delegates
        public Action<Task, Context> OpenTaskDetail { get; set; } = delegate { };
        public Action<bool> activateActionMode { get; set; } = delegate { };


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
            mAdapter = new RecyclerAdapter(taskList, recyclerView, view);
            mAdapter.TaskClicked += MAdapter_TaskClicked;
            mAdapter.freezeLayout += MAdapter_freezeLayout;
            mAdapterFilter = new RecyclerAdapterFilter(filterList, view);
            mAdapterFilter.onFilterClicked += MAdapterFilter_onFilterClicked;

            recyclerView.SetAdapter(mAdapter);
            recyclerViewFilter.SetAdapter(mAdapterFilter);




            return view;
        }

        public override void OnDestroy()
        {
            mAdapter.TaskClicked -= MAdapter_TaskClicked;
            mAdapter.freezeLayout -= MAdapter_freezeLayout;
            mAdapterFilter.onFilterClicked -= MAdapterFilter_onFilterClicked;
        }



        // ------------ Other Functions ------------
        // Event Handlers
        private void MAdapter_TaskClicked(object sender, Task task)
        {
            OpenTaskDetail(task, recyclerView.Context);
        }

        private void MAdapter_freezeLayout(object sender, bool freeze)
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

        private void MAdapterFilter_onFilterClicked(object sender, Filter chosenFilter)
        {
            // !!!!!----- connect to database -----!!!!!

            // this function connects to the database to retrieve the todolist/task objects under the chosenFilter
            if (chosenFilter.filterName == "Today")
            {
                List<Task> newTaskList = new List<Task>
                {
                    new Task(0, "Wash the dishes", new DateTime(2002, 8, 10)),
                    new Task(0, "Wash the car", new DateTime(2002, 8, 10)),
                    new Task(0, "Kill the racoons", new DateTime(2002, 8, 10)),
                };
                mAdapter.taskList = newTaskList;
                mAdapter.NotifyDataSetChanged();
            }
            else
            {
                mAdapter.taskList = fillTaskList();
                mAdapter.NotifyDataSetChanged();
            }
        }

        // Temp func
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
                new Task(0, "play games", new DateTime(2002, 8, 10)),
                new Task(1, "Feed the dogs", new DateTime(2002, 8, 11)),
                new Task(2, "Make an app", new DateTime(2002, 8, 12)),
                new Task(3, "Filipino Quipper", new DateTime(2002, 8, 13)),
                new Task(4, "Participate in a Marathon", new DateTime(2002, 8, 14)),
                new Task(5, "Finish the app", new DateTime(2002, 8, 15)),
                new Task(6, "Empty my tank", new DateTime(2002, 8, 16)),
                new Task(7, "Refill my tank", new DateTime(2002, 8, 17)),
                new Task(8, "Feed the dogs", new DateTime(2002, 8, 18)),
                new Task(4, "Participate in a Marathon", new DateTime(2002, 8, 19)),
                new Task(3, "Filipino Quipper", new DateTime(2002, 8, 20)),
                new Task(1, "Play games", new DateTime(2002, 8, 21)),
                new Task(3, "Filipino Quipper", new DateTime(2002, 8, 22))
            };

            return mtaskList;
        }

    }
}