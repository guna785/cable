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

namespace ChatApp.Models
{
    class ChatModel
    {
    }
    public class MemberData
    {
        private string name;
        private string color;

        public MemberData(string name, string color)
        {
            this.name = name;
            this.color = color;
        }

        // Add an empty constructor so we can later parse JSON into MemberData using Jackson
        public MemberData()
        {
        }

        public string getName()
        {
            return name;
        }

        public string getColor()
        {
            return color;
        }
    }
    public class MessageDta
    {
        private string text; // message body
        private MemberData memberData; // data of the user that sent this message
        private bool belongsToCurrentUser; // is this message sent by us?

        public MessageDta(string text, MemberData memberData, bool belongsToCurrentUser)
        {
            this.text = text;
            this.memberData = memberData;
            this.belongsToCurrentUser = belongsToCurrentUser;
        }

        public string getText()
        {
            return text;
        }

        public MemberData getMemberData()
        {
            return memberData;
        }

        public bool isBelongsToCurrentUser()
        {
            return belongsToCurrentUser;
        }
    }
}