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
    public class SignUpEventArgs : EventArgs
    {
        private string xname, xuname, xpassword;
        public string name { get { return xname; } set { xname = value; } }
        public string uname { get { return xuname; } set { xuname = value; } }
        public string password { get { return xpassword; } set { xpassword = value; } }
        public SignUpEventArgs(string _name, string _uname, string _password) : base()
        {
            name = _name;
            uname = _uname;
            password = _password;
        }
    }
    class Dialog_SignUp : DialogFragment
    {
        private EditText name, uname, password;
        private Button signUp;

        public event EventHandler<SignUpEventArgs> OnSignUpEvent;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.Dialog_signUp, container, false);
            name = view.FindViewById<EditText>(Resource.Id.txtName);
            uname = view.FindViewById<EditText>(Resource.Id.txtUsername);
            password = view.FindViewById<EditText>(Resource.Id.txtPassword);

            signUp = view.FindViewById<Button>(Resource.Id.SignUp_pro);
            signUp.Click += (object sender, EventArgs args) =>
            {
                OnSignUpEvent.Invoke(this, new SignUpEventArgs(name.Text, uname.Text, password.Text));
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