using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("logs")]
    public class logs : CommonModel
    {
        public string name { get; set; }
        public string uid { get; set; }
        public string msg { get; set; }
        public string subject { get; set; }
        public string remarks { get; set; }
        public string zone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
