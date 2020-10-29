﻿using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("user")]
    public class user : CommonModel
    {
        public string name { get; set; }
        public string uname { get; set; }
        public string password { get; set; }
        public string type { get; set; }
        public string zone { get; set; }
        public string address { get; set; }
        public DateTime CreatedAt { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string area { get; set; }
        public string deviceID { get; set; }
        public string deviceStatus { get; set; }
        public string location { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
    }
}
