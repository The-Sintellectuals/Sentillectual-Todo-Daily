using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using android_test_app.Adapters;
using android_test_app.Anim;
using android_test_app.fragments;
using android_test_app.otherCs;

using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using FragmentManage = Android.Support.V4.App.FragmentManager;
using Android.Content;

namespace android_test_app
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : FragmentActivity
    {
        // ------------ Initialization ------------
        // ViewPager and Bottom Navbar initialization
        LockableViewPager _viewPager;
        BottomNavigationView _navigationView;

        // Fragment Activity Initialization
        dash_fragment dashFrag = new dash_fragment();
        todos_fragment todoFrag = new todos_fragment();
        calendar_fragment calendarFrag = new calendar_fragment();
        settings_fragment settingFrag = new settings_fragment();
        Stack<int> NavHeights = new Stack<int>();


        // Buttons and Events
        private FloatingActionButton Fab_addbtn;
        private Button TaskCreate_btn, TagCreate_btn;

        public static MainActivity mainInstance;


        // ------------ Overrides ------------
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            //Sets The layout to content_main.xml
            SetContentView(Resource.Layout.content_main);

            // ------ViewPager Code------
            _viewPager = FindViewById<LockableViewPager>(Resource.Id.viewpager);
            _viewPager.PageSelected += ViewPager_PageSelected;
            // Creates an adapter and adds fragments
            ViewPagerAdapter adapter = new ViewPagerAdapter(SupportFragmentManager);
            adapter.AddFragment(dashFrag, "Dashboard");
            adapter.AddFragment(todoFrag, "Todos");
            todoFrag.OpenTaskDetail = showTaskDetail_Dialog;
            adapter.AddFragment(calendarFrag, "Calendar");
            adapter.AddFragment(settingFrag, "Settings");
            _viewPager.Adapter = adapter;


            // ------Bottom navigation Code------
            _navigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            _navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;


            // ------FAB Code------
            Fab_addbtn = FindViewById<FloatingActionButton>(Resource.Id.fab_main);
            Fab_addbtn.Click += (Object sender, EventArgs e) =>
            {
                FragmentTransaction trans = SupportFragmentManager.BeginTransaction();
                TaskCreation_Dialog taskCreate = new TaskCreation_Dialog();
                taskCreate.Show(trans, "Task Creation Dialog");
            };

            mainInstance = this;
        }

        protected override void OnDestroy()
        {
            _viewPager.PageSelected -= ViewPager_PageSelected;
            _navigationView.NavigationItemSelected -= NavigationView_NavigationItemSelected;
            base.OnDestroy();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }



        // ------------ Other Functions ------------
        // Event Handlers
        public void actionModeActivated(bool locked)
        {
            _viewPager = this.FindViewById<LockableViewPager>(Resource.Id.viewpager);
            _viewPager.SwipeLocked = locked;        // Lock swipe

            _navigationView = this.FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            NavHeights.Push(_navigationView.Height);
            
            if (locked)
            {
                // slide down
                CustomAnimation anim = new CustomAnimation(_navigationView, 0);
                anim.Duration = 250;
                _navigationView.StartAnimation(anim);
                anim.AnimationStart += Anim_AnimationStart_Down;
            }
            else
            {
                // slide up
                NavHeights.Pop();
                CustomAnimation anim = new CustomAnimation(_navigationView, NavHeights.Peek());
                anim.Duration = 250;
                _navigationView.StartAnimation(anim);
                anim.AnimationStart += Anim_AnimationStart_Down;
            }
            
        }

        public void showTaskDetail_Dialog(Task task, Context context)
        {
            FragmentTransaction fragmentTransaction = SupportFragmentManager.BeginTransaction();
            TaskDetail_Fragment taskDetail = new TaskDetail_Fragment(task, context);
            taskDetail.Show(fragmentTransaction, "Task Detail Dialog");
        }

        //Animations
        private void Anim_AnimationStart_Down(object sender, Android.Views.Animations.Animation.AnimationStartEventArgs e)
        {
            
        }

        // Bottom Navigation
        private void NavigationView_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            // Binds changes in the Bottom Navigation to the view pager
            _viewPager.SetCurrentItem(e.Item.Order, true);
            Console.WriteLine("nav pressed "+e.Item);
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            // Binds changes in the view pager to the bottom navigation
            var item = _navigationView.Menu.GetItem(e.Position);
            _navigationView.SelectedItemId = item.ItemId;
        }

    }
}
