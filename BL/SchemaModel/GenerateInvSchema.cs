using GSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.SchemaModel
{
    public class GenerateInvSchema
    {
       
        [GSchema("month", "Month", "string", true, getEnumVal = "month", getHtmlClass = "col -md-12", getfieldHtmlClass = "select2 selectfield")]
        public string month { get; set; }
        [GSchema("year", "Year", "string", true, getEnumVal = "year", getHtmlClass = "col -md-12", getfieldHtmlClass = "select2 selectfield")]
        public string year { get; set; }
        [GSchema("cid", "cusID", "hidden", true)]
        public string cid { get; set; }
        [GSchema("zone", "Zone", "string", true, getEnumVal = "zone", getHtmlClass = "col -md-12", getfieldHtmlClass = "select2 selectfield")]
        public string zone { get; set; }
        [GSchema("cdate", "Invoice Date", "date", true, getHtmlClass = "col-md-12")]
        public DateTime cdate { get; set; }
    }
}
