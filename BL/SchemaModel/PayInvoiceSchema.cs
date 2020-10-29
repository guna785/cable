using GSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.SchemaModel
{
    public class PayInvoiceSchema
    {
        [GSchema("Id", "ID", "hidden", true)]
        public string Id { get; set; }
        [GSchema("payonlyinv", "payonlyinv", "hidden", true)]
        public bool payonlyinv { get; set; }
        [GSchema("amount", "Amount", "number", true,getminimum ="0", getHtmlClass = "col-md-12")]
        public int amount { get; set; }
        [GSchema("mode", "Mode of Payment", "string", true, getEnumVal ="paymode", getHtmlClass = "col-md-12", getfieldHtmlClass = "select2 selectfield")]
        public string mode { get; set; }
        [GSchema("paynote", "Payment Note", "string", true, getHtmlClass = "col-md-12")]
        public string paynote { get; set; }

    }

}
