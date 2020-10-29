using GSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.SchemaModel
{
    public class ZoneSchema
    {
        [GSchema("name", "Name", "string", true, getHtmlClass = "col-md-12")]
        public string name { get; set; }
        [GSchema("status", "Status", "string", true,getEnumVal ="status", getHtmlClass = "col-md-12", getfieldHtmlClass = "select2 selectfield")]
        public string status { get; set; }
        [GSchema("remarks", "Remarks", "string", true, getHtmlClass = "col-md-12")]
        public string remarks { get; set; }
        [GSchema("cname", "Company Name", "string", true, getHtmlClass = "col-md-12")]
        public string Cname { get; set; }
        [GSchema("Cphone", "Company Phone", "string", true, getHtmlClass = "col-md-12")]
        public string Cphone { get; set; }
        [GSchema("caddress", "Company Address", "string", true, getHtmlClass = "col-md-12")]
        public string caddress { get; set; }
        [GSchema("email", "Company Email", "string", true, getHtmlClass = "col-md-12")]
        public string email { get; set; }
        [GSchema("web", "Company Website", "string", true, getHtmlClass = "col-md-12")]
        public string web { get; set; }
        [GSchema("photo", "Company Logo", "file", false, getHtmlClass = "col-md-12")]
        public byte[] photo { get; set; }

    }
}
