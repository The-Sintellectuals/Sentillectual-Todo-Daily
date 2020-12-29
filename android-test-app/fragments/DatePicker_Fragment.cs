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
        Action<DateTime> mDateSelectedHandler = delegate { };

        public DatePicker_Fragment(Action<DateTime> onDateSelected)
        {
            mDateSelectedHandler = onDateSelected;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime now = DateTime.Now;
            return new DatePickerDialog(Activity, this, now.Year, now.Month, now.Day);
        }

        public void OnDateSet(DatePicker view, int year, int month, int day)
        {
            DateTime selectedDate = new DateTime(year, month+1, day);
            mDateSelectedHandler(selectedDate);
        }
    }
}