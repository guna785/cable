using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChatApp.Fragments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ChatApp
{
    [Activity(Label = "LoginPage", MainLauncher = true)]
    public class LoginPage : Activity
    {
        Button signIn, signUp;
        ProgressBar progress;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.login_page);
            // Create your application here
            signIn = FindViewById<Button>(Resource.Id.SignIn);
            signUp = FindViewById<Button>(Resource.Id.SignUp);
            progress = FindViewById<ProgressBar>(Resource.Id.progress);
            progress.Visibility = ViewStates.Invisible;
            signIn.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                Dialog_SignIn dialog = new Dialog_SignIn();
                dialog.Show(transaction, "Sign In Fragment");
                dialog.Cancelable = false;
                dialog.OnSignInEvent += SignIn_dialogComplete;
            };
            signUp.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                Dialog_SignUp dialog = new Dialog_SignUp();
                dialog.Show(transaction, "Sign Up Fragment");
                dialog.Cancelable = false;
                dialog.OnSignUpEvent += SignUp_dialogComplete;
            };
        }
        private void SignIn_dialogComplete(object sender, SignInEventArgs e)
        {
            progress.Visibility = ViewStates.Visible;
            Thread th = new Thread(SignInAction);
            th.Start();
        }
        private void SignUp_dialogComplete(object sender, SignUpEventArgs e)
        {
            progress.Visibility = ViewStates.Visible;
            Thread th = new Thread(SignUpAction);
            th.Start();
        }
        private void SignUpAction()
        {
            Thread.Sleep(3000);
            RunOnUiThread(() =>
            {
                progress.Visibility = ViewStates.Invisible;
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            });
        }
        private void SignInAction()
        {
            Thread.Sleep(3000);
            RunOnUiThread(() =>
            {
                progress.Visibility = ViewStates.Invisible;
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            });
        }
    }
}