using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ChatApp.Fragments;
using Fragment = Android.Support.V4.App.Fragment;

namespace ChatApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            SupportFragmentManager.BeginTransaction()
                                 .Add(Resource.Id.container, new Home(), "Main FragMent") // Add the fragment
                                 .Commit();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Fragment f = null;
            bool IsCond = false;
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    f = new Home();
                    IsCond = true;
                    break;
                case Resource.Id.navigation_dashboard:
                    f = new status();
                    IsCond = true;
                    break;

                case Resource.Id.navigation_notifications:
                    f = new Calls();
                    IsCond = true;
                    break;

            }
            if (f != null)
            {
                var transaction = SupportFragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.container, f, "Main Content");
                transaction.Commit();
            }

            return IsCond;
        }
    }
}

