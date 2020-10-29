using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("stb")]
    public class stb : CommonModel
    {
        public string stbno { get; set; }
        public string cid { get; set; }
        public string planname { get; set; }
        public string addonpack { get; set; }
        public string amount { get; set; }
        public string type { get; set; }
        public string doorno { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string zone { get; set; }
        public DateTime mdate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
