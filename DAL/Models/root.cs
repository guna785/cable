using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("root")]
    public class root : CommonModel
    {
        public string uname { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
