using GSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.SchemaModel
{
    public class AnnouncementSchema
    {
        [GSchema("notifyType", "Send Via", "string", true, getEnumVal = "notifyType", getHtmlClass = "col-md-12", getfieldHtmlClass = "select2 selectfield")]
        public string notifyType { get; set; }
        [GSchema("customers", "Customers", "string", true, getEnumVal = "customers",getMulitple =true, getHtmlClass = "col -md-12",getfieldHtmlClass = "select2 selectfield selectMultiple")]
        public string customers { get; set; }
        [GSchema("subject", "Subject", "string", true, getHtmlClass = "col-md-12")]
        public string subject { get; set; }
        [GSchema("message", "Message", "string", true, getHtmlClass = "col-md-12")]
        public string message { get; set; }
    }
}
