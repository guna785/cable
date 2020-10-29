using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.service;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace cable.Controllers
{
    public class DeleteController : Controller
    {
        private IGenericBL<user> _users;
        private IGenericBL<customer> _customers;
        private IGenericBL<area> _areas;
        private IGenericBL<package> _packages;
        private IGenericBL<zone> _zones;
        private IGenericBL<provider> _providers;
        private IGenericBL<logs> _log;
        private IGenericBL<role> _roles;
        private IGenericBL<sms> _smss;
        private IGenericBL<stb> _stbss;
        private IGenericBL<email> _emails;
        private IGenericBL<gateway> _gateways;
        private IGenericBL<collection> _collections;
        private IGenericBL<invoice> _invoicess;
        private IGenericBL<userlogic> _userlogics;
        private IGenericBL<admin> _admins;
        public DeleteController(IGenericBL<user> users, IGenericBL<customer> customers, IGenericBL<area> areas, IGenericBL<package> packages,
            IGenericBL<zone> zones, IGenericBL<provider> providers, IGenericBL<logs> log, IGenericBL<role> roles, IGenericBL<sms> smss,
            IGenericBL<stb> stbss, IGenericBL<email> emails, IGenericBL<gateway> gateways, IGenericBL<collection> collections, IGenericBL<invoice> invoicess,
            IGenericBL<admin> admins, IGenericBL<userlogic> userlogics)
        {
            _admins = admins;
            _userlogics = userlogics;
            _users = users;
            _customers = customers;
            _areas = areas;
            _packages = packages;
            _zones = zones;
            _providers = providers;
            _log = log;
            _roles = roles;
            _smss = smss;
            _stbss = stbss;
            _emails = emails;
            _gateways = gateways;
            _collections = collections;
            _invoicess = invoicess;
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCollection([FromBody] string Id)
        {
            var col = _collections.AsQueryable().Where(x => x.Id == ObjectId.Parse(Id)).FirstOrDefault();
            if (col != null)
            {
                var inv = _invoicess.AsQueryable().AsEnumerable().Where(x => col.invid.AsEnumerable().Any(y => x.Id == (y.invId== "advance"?new ObjectId(): ObjectId.Parse(y.invId))));
                foreach(var i in inv)
                {
                    var am = col.balancests.AsEnumerable().Where(x => x.invId == i.Id.ToString()).FirstOrDefault();
                    if (am != null)
                    {
                        i.balance = (Convert.ToDouble(i.balance) + Convert.ToDouble(am.amt)).ToString();
                        i.status =i.balance==i.amount? "unpaid": "PartiallyPaid";
                        await _invoicess.ReplaceOneAsync(i);
                    }
                }
                col.status = "Deleted";
                var res =await _collections.ReplaceOneAsync(col);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "Payment is Canceled  " + col.payid,
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        zone = "root",
                        remarks = "none",
                        subject = "Data Deletion",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "Payment Sucessfully Canceled" };
                    return Ok(result);
                }
            }
            return BadRequest("Invalid Requist");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteInvoice([FromBody] string Id)
        {
            var inv = _invoicess.AsQueryable().Where(x => x.Id == ObjectId.Parse(Id)).FirstOrDefault();
            if (inv != null)
            {
                var col = _collections.AsQueryable().AsEnumerable().Where(x => x.status!= "Deleted" && x.invid.AsEnumerable().Any(y => inv.Id == (y.invId == "advance" ? new ObjectId() : ObjectId.Parse(y.invId))));
                if (col.Count()==0)
                {
                    inv.status = "Deleted";
                    var res = await _invoicess.ReplaceOneAsync(inv);
                    if (res)
                    {
                        var l = new logs()
                        {
                            msg = "Invoice is Canceled  " + inv.invid,
                            name = "EventLog",
                            uid = HttpContext.User.Identity.Name,
                            zone = "root",
                            remarks = "none",
                            subject = "Data Deletion",
                            CreatedAt = DateTime.Now
                        };
                        var r = await _log.InsertOneAsync(l);
                        var result = new { status = "Invoice Sucessfully Canceled" };
                        return Ok(result);
                    }
                }
                return BadRequest("Invoice is Assciate with Collection "+Newtonsoft.Json.JsonConvert.SerializeObject( col.AsEnumerable().Select(x=> new { collectionID =x.payid})));

            }
            return BadRequest("Invalid Requist");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSTB([FromBody] string Id)
        {
            var st = _stbss.AsQueryable().Where(x => x.Id == ObjectId.Parse(Id)).FirstOrDefault();
            if (st != null)
            {
                st.cid = "Available";
                var res =await _stbss.ReplaceOneAsync(st);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "STB is Removed from associated customer STN No : " + st.stbno,
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        zone = "root",
                        remarks = "none",
                        subject = "Data Deletion",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "STB is Removed from associated customer STN No : " + st.stbno };
                    return Ok(result);
                }

            }
            return BadRequest("Invalid Requist");
        }
    }
}
