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

namespace ChatApp.Models
{
    public class Photo
    {
        public int mPhotoID { get; set; }
        public string mCaption { get; set; }
    }
    public class PhotoAlbum
    {
        static Photo[] listPhoto =
        {
            new Photo() {mPhotoID = Resource.Drawable.ic_dashboard_black_24dp, mCaption = "Ahsan 1"},
            new Photo() {mPhotoID = Resource.Drawable.ic_dashboard_black_24dp, mCaption = "Ahsan 2"},
            new Photo() {mPhotoID = Resource.Drawable.ic_dashboard_black_24dp, mCaption = "Ahsan 3"},
            new Photo() {mPhotoID = Resource.Drawable.ic_dashboard_black_24dp, mCaption = "Ahsan 4"},
            new Photo() {mPhotoID = Resource.Drawable.ic_dashboard_black_24dp, mCaption = "Ahsan 5"},
            new Photo() {mPhotoID = Resource.Drawable.ic_dashboard_black_24dp, mCaption = "Ahsan 6"},
            new Photo() {mPhotoID = Resource.Drawable.ic_dashboard_black_24dp, mCaption = "Ahsan 7"},
            new Photo() {mPhotoID = Resource.Drawable.ic_dashboard_black_24dp, mCaption = "Ahsan 8"},
            new Photo() {mPhotoID = Resource.Drawable.ic_dashboard_black_24dp, mCaption = "Ahsan 9"},
            new Photo() {mPhotoID = Resource.Drawable.ic_dashboard_black_24dp, mCaption = "Ahsan 10"},
        };
        private Photo[] photos;
        Random random;
        public PhotoAlbum()
        {
            this.photos = listPhoto;
            random = new Random();
        }
        public int numPhoto
        {
            get
            {
                return photos.Length;
            }
        }
        public Photo this[int i]
        {
            get { return photos[i]; }
        }
    }
    public class PhotoViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }
        public TextView Caption { get; set; }
        public PhotoViewHolder(View itemview, Action<int> listener) : base(itemview)
        {
            Image = itemview.FindViewById<ImageView>(Resource.Id.imageView);
            Caption = itemview.FindViewById<TextView>(Resource.Id.textView);
            itemview.Click += (sender, e) => listener(base.Position);
        }
    }
}