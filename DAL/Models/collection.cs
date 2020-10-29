using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("collection")]
    public class collection : CommonModel
    {
        public List<invs> invid { get; set; }
        public string cid { get; set; }
        public string payid { get; set; }
        public string amount { get; set; }
        public string balance { get; set; }
        public List<balsts> balancests { get; set; }
        public string zone { get; set; }
        public string paymentMode { get; set; }
        public string paymenyNote { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string collectedby { get; set; }
        public string comments { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class invs
    {
        public string invId { get; set; }        

    }
    public class upIV
    {
        public string amt { get; set; }
        public string invid { get; set; }
    }
    public class balsts
    {
        public string invId { get; set; }
        public string amt { get; set; }
    }
}
