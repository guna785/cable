using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatApp.Adapter
{
    public class ChatAdapter : BaseAdapter
    {
        List<MessageDta> messages;
        public event EventHandler<int> ItemClick;
        private Context context;
        private LayoutInflater inflater;
        public void add(MessageDta message)
        {
            this.messages.Add(message);
            NotifyDataSetChanged(); // to render the list we need to notify
        }
        public override int Count => messages.Count;

        public ChatAdapter(Activity _context, List<MessageDta> _messages):base()
        {
            this.context = _context;
            this.messages = _messages;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
           
            LayoutInflater messageInflater = (LayoutInflater)context.GetSystemService(Activity.LayoutInflaterService);
            MessageDta message = messages[position];

            if (message.isBelongsToCurrentUser())
            { // this message was sent by us so let's create a basic chat bubble on the right
                convertView = messageInflater.Inflate(Resource.Layout.my_message, null);
               TextView messageBody = convertView.FindViewById<TextView>(Resource.Id.message_body);
                //convertView.Tag=holder;
                messageBody.SetText(message.getText(),null);
            }
            else
            { // this message was sent by someone else so let's create an advanced chat bubble on the left
                convertView = messageInflater.Inflate(Resource.Layout.their_message, null);
                View avatar = convertView.FindViewById<View>(Resource.Id.avatar);
                TextView name = convertView.FindViewById<TextView>(Resource.Id.name);
                TextView messageBody = convertView.FindViewById<TextView>(Resource.Id.message_body);                

                name.SetText(message.getMemberData().getName(),null);
                messageBody.SetText(message.getText(),null);
                GradientDrawable drawable = (GradientDrawable)avatar.Background;
                drawable.SetColor(Color.ParseColor(message.getMemberData().getColor()));

            }

            return convertView;
        }
    }
   

}