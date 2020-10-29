using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("role")]
    public class role : CommonModel
    {
        public string name { get; set; }
        public List<string> roles { get; set; }
        public string zone { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
