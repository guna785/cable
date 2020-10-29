using GSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.SchemaModel
{
    public class BulkUploadSchema
    {
        [GSchema("dbtable", "Select DB Table", "string", true,getEnumVal ="DBProperty", getHtmlClass = "col-md-12", getfieldHtmlClass = "select2 selectfield")]
        public string dbtable { get; set; }
        [GSchema("photo", "Company Logo", "file", false, getHtmlClass = "col-md-12")]
        public byte[] photo { get; set; }
    }
}
