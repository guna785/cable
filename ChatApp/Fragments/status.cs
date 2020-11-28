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

using Fragment = Android.Support.V4.App.Fragment;

namespace ChatApp.Fragments
{
    class status : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            // Use this to return your custom view for this Fragment
            var fragmentView = inflater.Inflate(Resource.Layout.status, container, false);
            return fragmentView;
        }
    }
}