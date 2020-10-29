using GSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.SchemaModel
{
    public class EditPackageSchema
    {
        [GSchema("Id", "ID", "hidden", true)]
        public string Id { get; set; }
        [GSchema("name", "Name", "string", true, getHtmlClass = "col-md-12")]
        public string name { get; set; }
        [GSchema("type", "Package Type", "string", true, getEnumVal = "packType", getHtmlClass = "col-md-12", getfieldHtmlClass = "select2 selectfield")]
        public string type { get; set; }
        [GSchema("amount", "Amount", "string", true, getHtmlClass = "col-md-12")]
        public string amount { get; set; }
        [GSchema("status", "Status", "string", true, getEnumVal = "status", getHtmlClass = "col-md-12", getfieldHtmlClass = "select2 selectfield")]
        public string status { get; set; }
        [GSchema("zone", "Zone", "string", true, getEnumVal = "zone", getHtmlClass = "col -md-12", getfieldHtmlClass = "select2 selectfield")]
        public string zone { get; set; }
        [GSchema("remarks", "Remarks", "string", true, getHtmlClass = "col-md-12")]
        public string remarks { get; set; }
    }
}
