using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("zone")]
    public class zone : CommonModel
    {
        public string name { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public DateTime CreatedAt { get; set; }      
        public string Cname { get; set; }
        public string Cphone { get; set; }
        public string caddress { get; set; }
        public string email { get; set; }
        public string web { get; set; }
        public byte[] photo { get; set; }
    }
}
