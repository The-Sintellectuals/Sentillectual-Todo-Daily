using DialogFragment = Android.Support.V4.App.DialogFragment;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android_test_app.fragments
{
    class DatePicker_Fragment : DialogFragment, DatePickerDialog.IOnDateSetListener
    {
        // ---------- Initialization ----------
        Action<DateTime> mDateSelectedHandler = delegate { };



        // ---------- Constructor ----------
        public DatePicker_Fragment(Action<DateTime> onDateSelected)
        {
            mDateSelectedHandler = onDateSelected;
        }



        // ---------- Overrides ----------
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime now = DateTime.Now;
            return new DatePickerDialog(Activity, this, now.Year, now.Month-1, now.Day);
        }

        public void OnDateSet(DatePicker view, int year, int month, int day)
        {
            // This function is called when the user selects a date

            DateTime selectedDate = new DateTime(year, month+1, day); 
            mDateSelectedHandler(selectedDate);                     // the date data is then passed to the event handler reference
        }
    }
}