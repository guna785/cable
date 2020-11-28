using BL.service;
using DAL.Models;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppApi
{
    public class SignalHub : Hub
    {
        private IGenericBL<chatMessage> _chat;
        private IGenericBL<chatGroup> _chatGroup;
        private IGenericBL<chatUser> _user;
        public SignalHub(IGenericBL<chatMessage> chat, IGenericBL<chatGroup> chatGroup, IGenericBL<chatUser> user)
        {
            _chat = chat;
            _chatGroup = chatGroup;
            _user = user;
        }
        public async Task SendMessage(string user, string touser, string message)
        {
           
                 await _chat.InsertOneAsync(new chatMessage()
                {
                    CreatedAt = DateTime.Now,
                    message = message,
                    fromID = ObjectId.Parse(user),
                    toID = ObjectId.Parse(touser)
                });
             
            await Clients.All.SendAsync("ReceiveMessage", user, touser, message);
        }
        public Task SendPrivateMessage(string user, string touser, string message)
        {
            return Clients.User(user).SendAsync("ReceiveMessage", message);
        }
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}
