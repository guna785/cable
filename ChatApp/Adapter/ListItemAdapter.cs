using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatApp.Models;

namespace ChatApp.Adapter
{
    public class ListItemAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public PhotoAlbum mPhotoAlbum;
        public ListItemAdapter(PhotoAlbum photoAlbum)
        {
            mPhotoAlbum = photoAlbum;
        }
        public override int ItemCount
        {
            get { return mPhotoAlbum.numPhoto; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PhotoViewHolder vh = holder as PhotoViewHolder;
            vh.Image.SetImageResource(mPhotoAlbum[position].mPhotoID);
            vh.Caption.Text = mPhotoAlbum[position].mCaption;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListItem, parent, false);
            PhotoViewHolder vh = new PhotoViewHolder(itemView, OnClick);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
}