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

namespace ChatApp.Fragments
{
    public class SignInEventArgs : EventArgs
    {
        private string xuname,xpassword;
        public string uname
        {
            get { return xuname; }
            set { xuname = value; }
        }
        public string password
        {
            get { return xpassword; }
            set { xpassword = value; }
        }
        public SignInEventArgs(string _uname,string _password)
        {
            uname = _uname;
            password = _password;
        }

    }
    class Dialog_SignIn : DialogFragment
    {
        private EditText uname, password;
        private Button signIn;

        public event EventHandler<SignInEventArgs> OnSignInEvent;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.Diaglog_SignIn, container, false);
            uname = view.FindViewById<EditText>(Resource.Id.txtUsername);
            password = view.FindViewById<EditText>(Resource.Id.txtPassword);

            signIn = view.FindViewById<Button>(Resource.Id.SignIn_pro);
            signIn.Click += (object sender, EventArgs args) =>
            {
                OnSignInEvent.Invoke(this,new SignInEventArgs(uname.Text,password.Text));
                this.Dismiss();
            };

            return view;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.Dialog_animation;
        }
    }
    
}