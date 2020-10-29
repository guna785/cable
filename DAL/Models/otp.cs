using DAL.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonCollection("otp")]
    public class otp : CommonModel
    {
        public string otpvalue { get; set; }
        public string imei { get; set; }
        public string tech { get; set; }
        public string zone { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
