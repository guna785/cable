using GSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.SchemaModel
{
    public class EditCustomerSchema
    {
        [GSchema("Id", "ID", "hidden", true)]
        public string Id { get; set; }
        [GSchema("name", "Name", "string", true, getHtmlClass = "col-md-4")]
        public string name { get; set; }
        [GSchema("cid", "Customer ID", "string", true, getHtmlClass = "col-md-4")]
        public string cid { get; set; }
        [GSchema("phone", "Phone No", "string", true, getHtmlClass = "col-md-4")]
        public string phone { get; set; }
        [GSchema("email", "Email", "email", false, getHtmlClass = "col-md-4")]
        public string email { get; set; }
        [GSchema("address", "Address", "string", true, getHtmlClass = "col-md-4")]
        public string address { get; set; }
        [GSchema("area", "Area", "string", true, getEnumVal = "area", getHtmlClass = "col -md-4", getfieldHtmlClass = "select2 selectfield")]
        public string area { get; set; }
        [GSchema("city", "City", "string", true, getHtmlClass = "col-md-4")]
        public string city { get; set; }
        [GSchema("zone", "Zone", "string", true, getEnumVal = "zone", getHtmlClass = "col -md-4", getfieldHtmlClass = "select2 selectfield")]
        public string zone { get; set; }


        [GSchema("state", "State", "string", true, getHtmlClass = "col-md-4")]
        public string state { get; set; }
        [GSchema("country", "Country", "string", true, getHtmlClass = "col-md-4")]
        public string country { get; set; }
        [GSchema("pincode", "Pincode", "number", true, getHtmlClass = "col-md-4")]
        public string pincode { get; set; }
        [GSchema("provider", "Provider", "string", true, getEnumVal = "provider", getHtmlClass = "col -md-4", getfieldHtmlClass = "select2 selectfield")]
        public string provider { get; set; }
        [GSchema("discount", "Discount", "string", true, getHtmlClass = "col-md-4")]

        public string discount { get; set; }
        [GSchema("status", "Status", "string", true, getEnumVal = "status", getHtmlClass = "col-md-4", getfieldHtmlClass = "select2 selectfield")]
        public string status { get; set; }

        [GSchema("remarks", "Remarks", "string", true, getHtmlClass = "col-md-4")]
        public string remarks { get; set; }
       
    }
}
