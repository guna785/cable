using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("customer")]
    public class customer : CommonModel
    {
        public string name { get; set; }
        public string cid { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string area { get; set; }
        public string zone { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pincode { get; set; }
        public string provider { get; set; }
        public string noofstb { get; set; }
        public string discount { get; set; }
        public string createdBy { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
