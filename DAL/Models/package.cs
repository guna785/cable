using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("package")]
    public class package : CommonModel
    {
        public string name { get; set; }
        public string type { get; set; }
        public string zone { get; set; }
        public string amount { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
