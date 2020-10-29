using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("sms")]
    public class sms : CommonModel
    {
        public string name { get; set; }
        public string url { get; set; }
        public string senderID { get; set; }
        public string skey { get; set; }
        public string route { get; set; }
        public string contenttype { get; set; }
        public string zone { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
