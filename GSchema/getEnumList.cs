using DAL.DALrepo;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSchema
{
    public static class getEnumList
    {
        public async static Task<string> getEnumRecords(string val, string zone = "")
        {


            if (val.Equals("status"))
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new List<string>() { "Active", "InActive" });
            }
            if (val.Equals("area"))
            {
                GenericRepository<area> rep = new GenericRepository<area>();
                var ls = new List<string>();
                ls.Add("Not Linked");
                var iot = zone == "root" ? rep.AsQueryable().Where(x => x.status != "Deleted").GroupBy(x => new { x.name }).Select(x => x.Key.name).ToList() : rep.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone).Select(x => x.name).ToList();
                ls.AddRange(iot);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }
            if (val.Equals("provider"))
            {
                GenericRepository<provider> rep = new GenericRepository<provider>();
                var ls = new List<string>();
                ls.Add("Not Linked");
                var iot = zone == "root" ? rep.AsQueryable().Where(x => x.status != "Deleted").GroupBy(x => new { x.name }).Select(x => x.Key.name).ToList() : rep.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone).Select(x => x.name).ToList();
                ls.AddRange(iot);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }
            if (val.Equals("package"))
            {
                GenericRepository<package> rep = new GenericRepository<package>();
                var ls = new List<string>();
                ls.Add("Not Linked");
                var iot = zone == "root" ? rep.AsQueryable().Where(x => x.status != "Deleted").Select(x => x.name).ToList() : rep.AsQueryable().Where(x => x.zone == zone && x.status != "Deleted").Select(x => x.name).ToList();
                ls.AddRange(iot);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }
            if (val.Equals("zone"))
            {
                GenericRepository<zone> rep = new GenericRepository<zone>();
                var ls = new List<string>();
                ls.Add("Not Linked");

                var iot = zone == "root" ? rep.AsQueryable().Where(x => x.status != "Deleted").Select(x => x.name).ToList() : rep.AsQueryable().Where(x => x.name == zone && x.status != "Deleted").Select(x => x.name).ToList();
                ls.AddRange(iot);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }

            if (val.Contains("packType"))
            {

                var ls = new List<string>();
                ls.Add("Not Linked");
                var iot = new List<string>() { "Main", "AddOnPack" };
                ls.AddRange(iot);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }
            if (val.Contains("techType"))
            {

                var ls = new List<string>();
                ls.Add("Not Linked");
                var iot = new List<string>() { "Collection", "Technitian" };
                ls.AddRange(iot);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }
            if (val.Contains("stbType"))
            {

                var ls = new List<string>();
                ls.Add("Not Linked");
                var iot = new List<string>() { "SD", "HD" };
                ls.AddRange(iot);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }
            if (val.Contains("AddonPack"))
            {
                GenericRepository<package> rep = new GenericRepository<package>();
                var ls = new List<string>();
                ls.Add("Not Linked");
                var iot = zone == "root" || zone == "" ? rep.AsQueryable().Where(x => x.status != "Deleted" && x.type == "AddOnPack").GroupBy(x => new { x.name }).Select(x => x.Key.name).ToList() : rep.AsQueryable().Where(x => x.status != "Deleted" && x.type == "AddOnPack" && x.zone == zone).Select(x => x.name).ToList();
                ls.AddRange(iot);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }
            if (val.Contains("planName"))
            {
                GenericRepository<package> rep = new GenericRepository<package>();
                var ls = new List<string>();
                ls.Add("Not Linked");
                var iot = zone == "root" || zone == "" ? rep.AsQueryable().Where(x => x.status != "Deleted" && x.type == "Main").GroupBy(x => new { x.name }).Select(x => x.Key.name).ToList() : rep.AsQueryable().Where(x => x.status != "Deleted" && x.type == "Main" && x.zone == zone).Select(x => x.name).ToList();
                ls.AddRange(iot);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }
            if (val.Contains("month"))
            {
                var ls = new List<string>(CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }
            if (val.Contains("year"))
            {
                var ls = new List<string>();
                int y = 2017;
                for (int i = 0; i <= DateTime.Now.Year; i++)
                {
                    ls.Add(y.ToString());
                    y++;
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }
            if (val.Contains("paymode"))
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new List<string>() { "Cash", "PayTM", "Google Pay", "Phone Pay", "Online / UPI Paymnet" });
            }
            if (val.Contains("DBProperty"))
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new List<string>()
                {
                    "Customer Bulk Info","Provider","Area","Package"

                });
            }
            if (val.Contains("notifyType"))
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new List<string>()
                {
                    "Email","SMS"

                });
            }
            if (val.Contains("customers"))
            {
                GenericRepository<customer> rep = new GenericRepository<customer>();
                var ls = new List<string>();
                ls.Add("Not Linked");
                var iot = zone == "root" || zone == "" ? rep.AsQueryable().Where(x => x.status != "Deleted" ).Select(x => x.cid).ToList() : rep.AsQueryable().Where(x => x.status != "Deleted"  && x.zone == zone).Select(x => x.cid).ToList();
                ls.AddRange(iot);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }
            return "";
        }

        public async static Task<string> getVlidationMessage(string val)
        {
            var msg = new
            {
                required = val + " is Required Property",
                pattern = "Correct format of " + val

            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(msg);
        }
    }
}
