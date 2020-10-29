using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Models
{
    public class InvPrintrintModel
    {
        public invoice inv { get; set; }
        public customer cus { get; set; }
        public zone adm { get; set; }
    }
}
