using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Models
{
    public class QrModel
    {
        public string cid { get; set; }
        public string name { get; set; }
        public string zone { get; set; }
        public byte[] img { get; set; }
    }
}
