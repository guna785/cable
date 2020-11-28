using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ChatApp.Adapter;
using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fragment = Android.Support.V4.App.Fragment;

namespace ChatApp.Fragments
{
    class Home: Fragment
    {
        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        PhotoAlbum mPhotoAlbum;
        ListItemAdapter mAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        View fragmentView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            mPhotoAlbum = new PhotoAlbum();
            // Use this to return your custom view for this Fragment
            fragmentView = inflater.Inflate(Resource.Layout.home, container, false);
            mRecycleView = fragmentView.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mLayoutManager = new LinearLayoutManager(fragmentView.Context);
            mRecycleView.SetLayoutManager(mLayoutManager);
            mAdapter = new ListItemAdapter(mPhotoAlbum);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);
            return fragmentView;
        }
        private void MAdapter_ItemClick(object sender, int e)
        {
            int photoNum = e + 1;
            //Toast.MakeText(fragmentView.Context, "This is photo number " + photoNum, ToastLength.Short).Show();
            var intent = new Intent(fragmentView.Context, typeof(ChatActivity));
            StartActivity(intent);
        }
    }
}