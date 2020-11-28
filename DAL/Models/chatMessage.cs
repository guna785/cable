using DAL.SharedModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("chatMessage")]
    public class chatMessage: CommonModel
    {
        public string message { get; set; }
        public ObjectId? groupId { get; set; }
        public ObjectId fromID { get; set; }
        public ObjectId? toID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    [BsonCollection("chatGroup")]
    public class chatGroup : CommonModel
    {
        public string name { get; set; }
        public List<ObjectId> chatmembers { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    [BsonCollection("chatUser")]
    public class chatUser : CommonModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
