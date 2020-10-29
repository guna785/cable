using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Models
{
    public class BlInvoice
    {
        public ObjectId Id { get; set; }
        public string invid { get; set; }
        public string cid { get; set; }
        public string area { get; set; }
        public string month { get; set; }
        public string amount { get; set; }
        public string balance { get; set; }
        public string zone { get; set; }
        public string status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
