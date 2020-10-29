using GSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.SchemaModel
{
    public class cusSTBSchema
    {
        [GSchema("Id", "ID", "hidden", false)]
        public string Id { get; set; }
        [GSchema("cusID", "cusID", "hidden", true)]
        public string cusID { get; set; }
        [GSchema("stbno", "STB No", "string", true, getHtmlClass = "col-md-6")]
        public string stbno { get; set; }
        [GSchema("type", "STB Type", "string", true, getEnumVal = "stbType", getHtmlClass = "col-md-6", getfieldHtmlClass = "select2 selectfield")]
        public string type { get; set; }
        [GSchema("planname", "Main Pack", "string", true, getEnumVal = "planName", getHtmlClass = "col-md-6", getfieldHtmlClass = "select2 selectfield")]
        public string planname { get; set; }
        [GSchema("addonpack", "Add on Pack", "string", true, getEnumVal = "AddonPack", getHtmlClass = "col-md-6", getfieldHtmlClass = "select2 selectfield")]
        public string addonpack { get; set; }
        [GSchema("zone", "Zone", "string", true, getEnumVal = "zone", getHtmlClass = "col -md-6")]
        public string zone { get; set; }

    }
}
