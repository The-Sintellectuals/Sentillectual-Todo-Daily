using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android_test_app.Anim
{
    class CustomAnimation : Animation
    {
        // ------------ Initialization ------------
        View mViews;
        private int OriginalHeight;
        private int GrowBy;



        // ------------ Constructor ------------
        public CustomAnimation(View mViews, int targetHeight)
        {
            this.mViews = mViews;
            OriginalHeight = mViews.Height;
            GrowBy = targetHeight - OriginalHeight;
        }



        // ------------ Overrides ------------
        protected override void ApplyTransformation(float interpolatedTime, Transformation t)
        {
            mViews.LayoutParameters.Height = (int)(OriginalHeight + (GrowBy * interpolatedTime));
            mViews.RequestLayout();
        }

        public override bool WillChangeBounds()
        {
            return true;
        }
    }
}