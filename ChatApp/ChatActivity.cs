using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Models;
using ChatApp.Adapter;

namespace ChatApp
{
    [Activity(Label = "ChatActivity")]
    public class ChatActivity : Activity
    {
        private HubConnection Hub;
        private EditText editText;
        public ListView list_of_message;
        List<MessageDta> messages=new List<MessageDta>();
        private ChatAdapter adapter;
        private ImageButton send;
        private string getRandomName()
        {
            Random random = new Random();
            string[] adjs = { "autumn", "hidden", "bitter", "misty", "silent", "empty", "dry", "dark", "summer", "icy", "delicate", "quiet", "white", "cool", "spring", "winter", "patient", "twilight", "dawn", "crimson", "wispy", "weathered", "blue", "billowing", "broken", "cold", "damp", "falling", "frosty", "green", "long", "late", "lingering", "bold", "little", "morning", "muddy", "old", "red", "rough", "still", "small", "sparkling", "throbbing", "shy", "wandering", "withered", "wild", "black", "young", "holy", "solitary", "fragrant", "aged", "snowy", "proud", "floral", "restless", "divine", "polished", "ancient", "purple", "lively", "nameless" };
            string[] nouns = { "waterfall", "river", "breeze", "moon", "rain", "wind", "sea", "morning", "snow", "lake", "sunset", "pine", "shadow", "leaf", "dawn", "glitter", "forest", "hill", "cloud", "meadow", "sun", "glade", "bird", "brook", "butterfly", "bush", "dew", "dust", "field", "fire", "flower", "firefly", "feather", "grass", "haze", "mountain", "night", "pond", "darkness", "snowflake", "silence", "sound", "sky", "shape", "surf", "thunder", "violet", "water", "wildflower", "wave", "water", "resonance", "sun", "wood", "dream", "cherry", "tree", "fog", "frost", "voice", "paper", "frog", "smoke", "star" };
            return (
                adjs[(int)Math.Floor(random.NextDouble() * adjs.Length)] +
                "_" +
                nouns[(int)Math.Floor(random.NextDouble() * nouns.Length)]
            );
        }
        public void populatData()
        {
            for (var i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    messages.Add(new MessageDta("Hi How R U?", new MemberData(getRandomName(), getRandomColor()), false));
                }
                else
                {
                    messages.Add(new MessageDta("Hi How R U?", new MemberData(getRandomName(), getRandomColor()), true));
                }
              
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.ChatActivity);
            editText = FindViewById<EditText>(Resource.Id.editText);
            send = FindViewById<ImageButton>(Resource.Id.Send);

            list_of_message = FindViewById<ListView>(Resource.Id.messages_view);
            populatData();
            adapter = new ChatAdapter(this, messages);
            list_of_message.Adapter = adapter;
            send.Click += (object sender, EventArgs args) =>
             {
                 adapter.add(new MessageDta(editText.Text, new MemberData(getRandomName(), getRandomColor()), true));
                 editText.SetText("", null);
                 list_of_message.SetSelection(adapter.Count - 1);
             };
                Hub = new HubConnectionBuilder()
                        .WithUrl("https://chatapp.b2lsolution.in/chatHub")
                        .Build();
            // Create your application here
            Hub.On<string, string,string,string>("ReceiveMessage", (user, touser, groupId, message) =>
            {
                //do something on your UI maybe?
            });
        }
        async Task Connect()
        {
            await Hub.StartAsync();
        }
        async Task Disconnect()
        {
            await Hub.StopAsync();
        }
        async Task SendMessage(string user, string touser, string groupId, string message)
        {
            await Hub.InvokeAsync("SendMessage", user,touser, groupId, message);
        }
        private String getRandomColor()
        {
            Random rnd = new Random();
            string hexOutput = String.Format("{0:X}", rnd.Next(0, 0xFFFFFF));
            while (hexOutput.Length < 6)
                hexOutput = "0" + hexOutput;
            return "#" + hexOutput;
        }
    }
}