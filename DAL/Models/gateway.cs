using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("gateway")]
    public class gateway : CommonModel
    {
        public string name { get; set; }
        public string redirecturl { get; set; }
        public string Mkey { get; set; }
        public string salt { get; set; }
        public string defaultmail { get; set; }
        public string defaultnumber { get; set; }
        public string extras { get; set; }
        public string zone { get; set; }
        public string status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
