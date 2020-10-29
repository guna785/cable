using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Models
{
    public class serviceModel
    {
        public ObjectId Id { get; set; }

        public string location { get; set; }
        public string service { get; set; }
        public string contractPeriod { get; set; }
        public string contractTimeline { get; set; }
        public string price { get; set; }
        public string uid { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; }
    }
    public class serviceContractModel : serviceModel
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
