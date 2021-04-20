using DAL.Models.enums;
using DAL.SharedModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
        public string ExternalId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string AppId { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public bool IsOnline { get; set; }
        public List<Connection> Connections { get; set; }
        public List<Activity> Activities { get; set; }

        [BsonIgnore]
        public Application Application { get; set; }
    }
    public class Connection
    {
        public string ConnectionId { get; set; }

        public string JWTToken { get; set; }
    }
    public class Activity
    {
        public ActivityType ActivityType { get; set; }
        public DateTime Date { get; set; }
        public string ConnectionId { get; set; }
    }
    [BsonCollection("Application")]
    public class Application:CommonModel
    {        
        public string Name { get; set; }
        public string APIKey { get; set; }
    }
    [BsonCollection("ChatConversation")]
    public class ChatConversation:CommonModel
    {
       

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string Text { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string FileId { get; set; }
        public DateTime Date { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ParentConversationId { get; set; }
        public List<ChatConversationReader> ChatConversationReaders { get; set; }

        [BsonIgnore]
        public chatUser User { get; set; }

    }
    public class ChatConversationReader
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public DateTime Date { get; set; }

        [BsonIgnore]
        public chatUser User { get; set; }
    }
    public class Chat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string AppId { get; set; }
        public ChatType ChatType { get; set; }
        public List<ChatMember> ChatMembers { get; set; }
        public List<ChatConversation> ChatConversations { get; set; }

        [BsonIgnore]
        public Application Application { get; set; }

    }
    public class ChatMember
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonIgnore]
        public chatUser User { get; set; }
    }
    public class FileMetadata
    {
        public string ContentType { get; set; }
    }
    [BsonCollection("File")]
    public class File:CommonModel
    {
        
        public string Filename { get; set; }
        public DateTime UploadDate { get; set; }
        public FileMetadata Metadata { get; set; }

        [BsonRepresentation(BsonType.Binary)]
        public BsonBinaryData Data { get; set; }
    }
}
