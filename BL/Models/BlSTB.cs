using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Models
{
    public class BlSTB
    {
        public ObjectId Id { get; set; }
        public string stbno { get; set; }
        public string cid { get; set; }
        public string cusname { get; set; }
        public string cusAddress { get; set; }
        public string cusarea { get; set; }
        public string stbtype { get; set; }
        public string package { get; set; }
        public string addonpack { get; set; }
        public string status { get; set; }
        public string zone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
