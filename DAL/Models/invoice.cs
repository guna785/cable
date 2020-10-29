using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("invoice")]
    public class invoice : CommonModel
    {
        public string invid { get; set; }
        public string cid { get; set; }
        public string amount { get; set; }
        public string balance { get; set; }
        public string zone { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public string noofstbs { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string comments { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
