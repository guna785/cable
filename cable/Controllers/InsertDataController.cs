using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BL.Helper;
using BL.SchemaModel;
using BL.security;
using BL.service;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace cable.Controllers
{
    [Authorize]
    public class InsertDataController : Controller
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
        public InsertDataController(IGenericBL<user> users, IGenericBL<customer> customers, IGenericBL<area> areas, IGenericBL<package> packages,
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
        public async Task<IActionResult> InsertZone([FromBody] ZoneSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var adm = _zones.AsQueryable().Where(x => x.name == zon.name).FirstOrDefault();
            if (adm == null)
            {
                var d = new zone()
                {
                    caddress = zon.caddress,
                    Cname = zon.Cname,
                    Cphone = zon.Cphone,
                    CreatedAt = DateTime.Now,
                    email = zon.email,
                    photo = zon.photo == null ? null : zon.photo,
                    web = zon.web,
                    name = zon.name,
                    status = zon.status,
                    remarks = zon.remarks
                };
                var res = await _zones.InsertOneAsync(d);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "New Zone is Added  " + zon.name,
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        zone = "root",
                        remarks = "none",
                        subject = "Data Insertion",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "New Zone Sucessfully Added" };
                    return Ok(result);
                }
            }
            return BadRequest("Zone Name Already Exists");
        }
        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] TechnitianSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var adm = _users.AsQueryable().Where(x => x.uname == zon.uname).FirstOrDefault();
            if (adm == null)
            {
                var salt = HasherService.GenerateSalt();

                var hashedPassword = HasherService.HashPasswordWithSalt(Encoding.UTF8.GetBytes(zon.password), salt);
                var d = new user()
                {
                    name = zon.name,
                    status = zon.status,
                    remarks = zon.remarks,
                    uname = zon.uname,
                    phone = zon.phone,
                    type = zon.type,
                    address = zon.address,
                    area = zon.area,
                    CreatedAt = DateTime.Now,
                    email = zon.email,
                    zone = zon.zone,
                    password = Convert.ToBase64String(hashedPassword)

                };
                var res = await _users.InsertOneAsync(d);

                if (res)
                {
                    var logic = new userlogic();
                    logic.uid = _users.AsQueryable().Where(x => x.uname == d.uname).FirstOrDefault().Id;
                    logic.usrToken = salt;
                    await _userlogics.InsertOneAsync(logic);
                    var l = new logs()
                    {
                        msg = "New User is Added  " + zon.name,
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        zone = "root",
                        remarks = "none",
                        subject = "Data Insertion",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "New User Sucessfully Added" };
                    return Ok(result);
                }
            }
            return BadRequest("User Name Already Exists");
        }
        [HttpPost]
        public async Task<IActionResult> InsertArea([FromBody] AreaSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var adm = _areas.AsQueryable().Where(x => x.name == zon.name && x.zone == zon.zone).FirstOrDefault();
            if (adm == null)
            {
                var d = new area()
                {
                    name = zon.name,
                    status = zon.status,
                    remarks = zon.remarks,
                    CreatedAt = DateTime.Now,
                    zone = zon.zone
                };
                var res = await _areas.InsertOneAsync(d);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "New Area is Added  " + zon.name,
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        zone = "root",
                        remarks = "none",
                        subject = "Data Insertion",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "New Area Sucessfully Added" };
                    return Ok(result);
                }
            }
            return BadRequest("Area Name Already Exists");
        }
        [HttpPost]
        public async Task<IActionResult> InsertProvider([FromBody] ProviderSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var adm = _providers.AsQueryable().Where(x => x.name == zon.name && x.zone == zon.zone).FirstOrDefault();
            if (adm == null)
            {
                var d = new provider()
                {
                    name = zon.name,
                    status = zon.status,
                    remarks = zon.remarks,
                    CreatedAt = DateTime.Now,
                    zone = zon.zone
                };
                var res = await _providers.InsertOneAsync(d);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "New Provider is Added  " + zon.name,
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        zone = "root",
                        remarks = "none",
                        subject = "Data Insertion",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "New Provider Sucessfully Added" };
                    return Ok(result);
                }
            }
            return BadRequest("Provider Name Already Exists");
        }
        [HttpPost]
        public async Task<IActionResult> InsertPackage([FromBody] PackageSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var adm = _packages.AsQueryable().Where(x => x.name == zon.name && x.zone == zon.zone).FirstOrDefault();

            if (adm == null)
            {
                var d = new package()
                {
                    name = zon.name,
                    status = zon.status,
                    remarks = zon.remarks,
                    CreatedAt = DateTime.Now,
                    amount = zon.amount,
                    zone = zon.zone,
                    type = zon.type
                };
                var res = await _packages.InsertOneAsync(d);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "New Package is Added  " + zon.name,
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        zone = "root",
                        remarks = "none",
                        subject = "Data Insertion",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "New Package Sucessfully Added" };
                    return Ok(result);
                }
            }
            return BadRequest("Provider Name Already Exists");
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomerSTB([FromBody] cusSTBSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            if (String.IsNullOrEmpty(zon.Id))
            {

                var s = _stbss.AsQueryable().Where(x => x.stbno == zon.stbno && x.zone == zon.zone).FirstOrDefault();
                if (s == null)
                {
                    var amt = (_packages.AsQueryable().Where(x => x.name == zon.planname).FirstOrDefault() == null ? 0 : Convert.ToInt32(_packages.AsQueryable().Where(x => x.name == zon.planname).FirstOrDefault().amount)) + (_packages.AsQueryable().Where(x => x.name == zon.addonpack).FirstOrDefault() == null ? 0 : Convert.ToInt32(_packages.AsQueryable().Where(x => x.name == zon.planname).FirstOrDefault().amount));

                    var res = await _stbss.InsertOneAsync(new stb()
                    {
                        doorno = "",
                        CreatedAt = DateTime.Now,
                        stbno = zon.stbno,
                        addonpack = zon.addonpack,
                        cid = zon.cusID,
                        mdate = DateTime.Now,
                        planname = zon.planname,
                        remarks = "none",
                        type = zon.type,
                        zone = zon.zone,
                        status = "Active",
                        amount = amt.ToString()
                    });
                    if (res)
                    {
                        var l = new logs()
                        {
                            msg = "New STB  is Added and alloted  " + zon.stbno,
                            name = "EventLog",
                            uid = HttpContext.User.Identity.Name,
                            zone = "root",
                            remarks = "none",
                            subject = "Data Insertion",
                            CreatedAt = DateTime.Now
                        };
                        var r = await _log.InsertOneAsync(l);
                        var result = new { status = "New STB  is Added and alloted " + zon.stbno };
                        return Ok(result);
                    }

                }
                else if (s.cid == "available")
                {
                    s.mdate = DateTime.Now;
                    s.planname = zon.planname;
                    s.stbno = zon.stbno;
                    s.addonpack = zon.addonpack;
                    s.cid = zon.cusID;
                    s.type = zon.type;
                    s.zone = zon.zone;
                    var res = await _stbss.ReplaceOneAsync(s);
                    if (res)
                    {
                        var l = new logs()
                        {
                            msg = "Existing STB  is alloted  " + zon.stbno,
                            name = "EventLog",
                            uid = HttpContext.User.Identity.Name,
                            zone = "root",
                            remarks = "none",
                            subject = "Data Insertion",
                            CreatedAt = DateTime.Now
                        };
                        var r = await _log.InsertOneAsync(l);
                        var result = new { status = "Existing STB  is alloted " + zon.stbno };
                        return Ok(result);
                    }
                }
                else if (s.cid != "available")
                {
                    s.mdate = DateTime.Now;
                    s.planname = zon.planname;
                    s.stbno = zon.stbno;
                    s.addonpack = zon.addonpack;
                    s.cid = zon.cusID;
                    s.type = zon.type;
                    s.zone = zon.zone;
                    var res = await _stbss.ReplaceOneAsync(s);
                    if (res)
                    {
                        var l = new logs()
                        {
                            msg = "Existing STB  is Shifted  " + zon.stbno,
                            name = "EventLog",
                            uid = HttpContext.User.Identity.Name,
                            zone = "root",
                            remarks = "none",
                            subject = "Data Insertion",
                            CreatedAt = DateTime.Now
                        };
                        var r = await _log.InsertOneAsync(l);
                        var result = new { status = "Existing STB  is Shifted " + zon.stbno };
                        return Ok(result);
                    }
                }
            }
            else
            {
                var s = _stbss.AsQueryable().Where(x => x.Id == ObjectId.Parse(zon.Id)).FirstOrDefault();
                if (s != null)
                {
                    s.mdate = DateTime.Now;
                    s.planname = zon.planname;
                    s.stbno = zon.stbno;
                    s.addonpack = zon.addonpack;
                    s.cid = zon.cusID;
                    s.type = zon.type;
                    s.zone = zon.zone;
                    var res = await _stbss.ReplaceOneAsync(s);
                    if (res)
                    {
                        var l = new logs()
                        {
                            msg = "Existing STB is Updated  " + zon.stbno,
                            name = "EventLog",
                            uid = HttpContext.User.Identity.Name,
                            zone = "root",
                            remarks = "none",
                            subject = "Data Insertion",
                            CreatedAt = DateTime.Now
                        };
                        var r = await _log.InsertOneAsync(l);
                        var result = new { status = "Existing STB is Updated Sucessfully" };
                        return Ok(result);
                    }
                }
            }

            return BadRequest("Bad Request");
        }
        [HttpPost]
        public async Task<IActionResult> InsertAdmin([FromBody] AdminSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var salt = HasherService.GenerateSalt();

            var hashedPassword = HasherService.HashPasswordWithSalt(Encoding.UTF8.GetBytes(zon.password), salt);
            var adm = _admins.AsQueryable().Where(x => x.uname == zon.uname).FirstOrDefault();
            if (adm == null)
            {
                var d = new admin()
                {
                    zone = zon.zone,
                    photo = zon.photo == null ? null : zon.photo,
                    phone = zon.phone,
                    address = zon.address,
                    CreatedAt = DateTime.Now,
                    email = zon.email,
                    gst = zon.gst,
                    password = Convert.ToBase64String(hashedPassword),
                    type = "admin",
                    uname = zon.uname,
                    name = zon.name,
                    status = zon.status,
                    remarks = zon.remarks
                };
                var res = await _admins.InsertOneAsync(d);

                if (res)
                {
                    var logic = new userlogic();
                    logic.uid = _admins.AsQueryable().Where(x => x.uname == d.uname).FirstOrDefault().Id;
                    logic.usrToken = salt;
                    await _userlogics.InsertOneAsync(logic);
                    var l = new logs()
                    {
                        msg = "New Admin is Added  " + zon.name,
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        zone = "root",
                        remarks = "none",
                        subject = "Data Insertion",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "New Admin Sucessfully Added" };
                    return Ok(result);
                }
            }


            return BadRequest("User Name Already Exists");
        }
        [HttpPost]
        public async Task<IActionResult> GenerateInvoice([FromBody] GenerateInvSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var adm = _invoicess.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zon.zone && x.cid == zon.cid && x.month == DateConfig.getMonthIntfromNumber(zon.month) && x.year == Convert.ToInt32(zon.year)).FirstOrDefault();
            if (adm == null)
            {
                var stbs = _stbss.AsQueryable().Where(x => x.cid == zon.cid && x.zone == zon.zone).AsEnumerable().Sum(x => Convert.ToDouble(x.amount));
                var dicount = _customers.AsQueryable().Where(x => x.cid == zon.cid && x.zone == zon.zone).FirstOrDefault() == null ? 0 : Convert.ToDouble(_customers.AsQueryable().Where(x => x.cid == zon.cid).FirstOrDefault().discount);
                var invid = _invoicess.AsQueryable().Where(x => x.zone == zon.zone).OrderByDescending(x => x.CreatedAt).FirstOrDefault() == null ? 1 : Convert.ToInt32(_invoicess.AsQueryable().Where(x => x.zone == zon.zone).OrderByDescending(x => x.CreatedAt).FirstOrDefault().invid) + 1;

                var res = await _invoicess.InsertOneAsync(new invoice()
                {
                    amount = (stbs - dicount).ToString(),
                    balance = (stbs - dicount).ToString(),
                    cid = zon.cid,
                    comments = "none",
                    CreatedAt = zon.cdate,
                    month = DateConfig.getMonthIntfromNumber(zon.month),
                    year = Convert.ToInt32(zon.year),
                    invid = invid.ToString(),
                    noofstbs = _stbss.AsQueryable().Where(x => x.cid == zon.cid && x.zone == zon.zone).Count().ToString(),
                    zone = zon.zone,
                    remarks = "none",
                    status = "UnPaid"

                });

                if (res)
                {
                    var l = new logs()
                    {
                        msg = "New Invoice is Added  " + zon.cid,
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        zone = "root",
                        remarks = "none",
                        subject = "Data Insertion",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "New Invoice Sucessfully Added" };
                    return Ok(result);
                }

            }


            return BadRequest("User Name Already Exists");
        }
        [HttpPost]
        public async Task<IActionResult> InsertCustomer([FromBody] CustomerSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }

            var adm = _customers.AsQueryable().Where(x => x.cid == zon.cid).FirstOrDefault();
            if (adm == null)
            {
                var stbs = zon.stbs.Where(x => !_stbss.AsQueryable().AsEnumerable().Any(y => y.stbno == x.stbno && y.cid != "available"));
                var d = new customer()
                {
                    zone = zon.zone,
                    phone = zon.phone,
                    address = zon.address,
                    CreatedAt = DateTime.Now,
                    email = zon.email,
                    name = zon.name,
                    status = zon.status,
                    remarks = zon.remarks,
                    area = zon.area,
                    cid = zon.cid,
                    city = zon.city,
                    country = zon.country,
                    createdBy = HttpContext.User.Identity.Name,
                    discount = zon.discount,
                    noofstb = zon.stbs.Count.ToString(),
                    pincode = zon.pincode,
                    provider = zon.provider,
                    state = zon.state,


                };

                var res = await _customers.InsertOneAsync(d);
                if (res)
                {
                    var Tamount = 0;
                    foreach (var s in stbs)
                    {
                        var amt = (_packages.AsQueryable().Where(x => x.name == s.planname).FirstOrDefault() == null ? 0 : Convert.ToInt32(_packages.AsQueryable().Where(x => x.name == s.planname).FirstOrDefault().amount)) + (_packages.AsQueryable().Where(x => x.name == s.addonpack).FirstOrDefault() == null ? 0 : Convert.ToInt32(_packages.AsQueryable().Where(x => x.name == s.planname).FirstOrDefault().amount));
                        Tamount = Tamount + amt + Convert.ToInt32(zon.installationamt);
                        await _stbss.InsertOneAsync(new stb()
                        {
                            cid = zon.cid,
                            addonpack = s.addonpack,
                            amount = (amt).ToString(),
                            CreatedAt = DateTime.Now,
                            doorno = "",
                            mdate = DateTime.Now,
                            planname = s.planname,
                            remarks = "none",
                            status = "Active",
                            stbno = s.stbno,
                            type = s.type,
                            zone = zon.zone
                        });
                    }

                    var invid = _invoicess.AsQueryable().Where(x => x.zone == d.zone).OrderByDescending(x => x.CreatedAt).FirstOrDefault() == null ? 1 : Convert.ToInt32(_invoicess.AsQueryable().Where(x => x.zone == d.zone).OrderByDescending(x => x.CreatedAt).FirstOrDefault().invid) + 1;
                    await _invoicess.InsertOneAsync(new invoice()
                    {
                        amount = Tamount.ToString(),
                        balance = Tamount.ToString(),
                        cid = d.cid,
                        comments = "none",
                        CreatedAt = DateTime.Now,
                        noofstbs = stbs.Count().ToString(),
                        remarks = "none",
                        status = "UnPaid",
                        zone = d.zone,
                        invid = (invid).ToString(),
                        month = DateTime.Now.Month,
                        year = DateTime.Now.Year

                    });

                }


                if (res)
                {

                    var l = new logs()
                    {
                        msg = "Invoice Generated Successfully  " + zon.name,
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        zone = "root",
                        remarks = "none",
                        subject = "Data Insertion",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "Invoice Generated Successfully  " };
                    return Ok(result);
                }
            }


            return BadRequest("Customer ID Already Exists");
        }
        [HttpPost]
        public async Task<IActionResult> PayCus([FromBody] PayInvoiceSchema pay)
        {
            var inv = _invoicess.AsQueryable().Where(x => x.Id == ObjectId.Parse(pay.Id)).FirstOrDefault();
            if (inv != null)
            {
                if (pay.payonlyinv)
                {
                    var bal = Convert.ToDouble(inv.balance) - Convert.ToDouble(pay.amount);
                    if (bal >= 0)
                    {
                        inv.balance = bal.ToString();
                        inv.status = bal == 0 ? "paid" : "PartiallyPaid";
                        await _invoicess.ReplaceOneAsync(inv);
                        var payID = _collections.AsQueryable().Where(x => x.zone == inv.zone).OrderByDescending(x => x.CreatedAt).FirstOrDefault() == null ? 1 : Convert.ToInt32(_collections.AsQueryable().Where(x => x.zone == inv.zone).OrderByDescending(x => x.CreatedAt).FirstOrDefault().payid.Replace(inv.zone, "")) + 1;
                        var res = await _collections.InsertOneAsync(new collection()
                        {
                            status = "paid",
                            amount = pay.amount.ToString(),
                            balance = bal.ToString(),
                            balancests = new List<balsts>() { new balsts() { invId = inv.Id.ToString(), amt = pay.amount.ToString() } },
                            cid = inv.cid,
                            collectedby = HttpContext.User.Identity.Name,
                            comments = "none",
                            CreatedAt = DateTime.Now,
                            invid = new List<invs>() { new invs() { invId = inv.Id.ToString() } },
                            payid = inv.zone + payID.ToString(),
                            zone = inv.zone,
                            paymentMode = pay.mode,
                            paymenyNote = pay.paynote,
                            remarks = "none"
                        });
                        if (res)
                        {

                            var l = new logs()
                            {
                                msg = "Payment Done Successfully for " + inv.cid,
                                name = "EventLog",
                                uid = HttpContext.User.Identity.Name,
                                zone = "root",
                                remarks = "none",
                                subject = "Data Insertion",
                                CreatedAt = DateTime.Now
                            };
                            var r = await _log.InsertOneAsync(l);
                            var result = new { status = "Payment Done Successfully   " };
                            return Ok(result);
                        }
                    }
                    else
                    {
                        var avbal = Convert.ToDouble(pay.amount) - Convert.ToDouble(inv.balance);
                        var invbal = inv.balance;
                        inv.balance = "0";
                        inv.status = "paid";
                        await _invoicess.ReplaceOneAsync(inv);
                        var payID = _collections.AsQueryable().Where(x => x.zone == inv.zone).OrderByDescending(x => x.CreatedAt).FirstOrDefault() == null ? 1 : Convert.ToInt32(_collections.AsQueryable().Where(x => x.zone == inv.zone).OrderByDescending(x => x.CreatedAt).FirstOrDefault().payid.Replace(inv.zone, "")) + 1;
                        var res = await _collections.InsertOneAsync(new collection()
                        {
                            status = "paid",
                            amount = pay.amount.ToString(),
                            balance = "0",
                            balancests = new List<balsts>() { new balsts() { invId = inv.Id.ToString(), amt = invbal }, new balsts() { amt = avbal.ToString(), invId = "advance" } },
                            cid = inv.cid,
                            collectedby = HttpContext.User.Identity.Name,
                            comments = "none",
                            CreatedAt = DateTime.Now,
                            invid = new List<invs>() { new invs() { invId = inv.Id.ToString() }, new invs() { invId = "advance" } },
                            payid = inv.zone + payID.ToString(),
                            zone = inv.zone,
                            paymentMode = pay.mode,
                            paymenyNote = pay.paynote,
                            remarks = "none"
                        });
                        if (res)
                        {

                            var l = new logs()
                            {
                                msg = "Payment Done Successfully for " + inv.cid,
                                name = "EventLog",
                                uid = HttpContext.User.Identity.Name,
                                zone = "root",
                                remarks = "none",
                                subject = "Data Insertion",
                                CreatedAt = DateTime.Now
                            };
                            var r = await _log.InsertOneAsync(l);
                            var result = new { status = "Payment Done Successfully   " };
                            return Ok(result);
                        }
                    }
                }


            }
            return BadRequest("Unable to process Payment");
        }
        [HttpPost]
        public async Task<IActionResult> InsertBulk([FromBody] BulkUploadSchema bulk)
        {
            string s = System.Text.Encoding.UTF8.GetString(bulk.photo, 0, bulk.photo.Length);
            dynamic json = JValue.Parse(s);
            if (bulk.dbtable == "Area")
            {
                foreach (var j in json)
                {
                    string name = j.name;
                    string z = j.zone;
                    var area = _areas.AsQueryable().Where(x => x.name == name).FirstOrDefault();
                    var zon = _zones.AsQueryable().Where(x => x.name == z).FirstOrDefault();
                    if (area == null && zon != null)
                    {
                        var res = await _areas.InsertOneAsync(new area()
                        {
                            CreatedAt = Convert.ToDateTime(j.date),
                            name = name,
                            zone = z,
                            remarks = j.remarks,
                            status = j.status
                        });
                    }

                }
                var l = new logs()
                {
                    msg = "Area Bulk Upload Done",
                    name = "EventLog",
                    uid = HttpContext.User.Identity.Name,
                    zone = "root",
                    remarks = "none",
                    subject = "Data Insertion",
                    CreatedAt = DateTime.Now
                };
                var r = await _log.InsertOneAsync(l);
                var result = new { status = "Area Bulk Upload Done" };
                return Ok(result);
            }

            if (bulk.dbtable == "Provider")
            {
                foreach (var j in json)
                {
                    string name = j.name;
                    string z = j.zone;
                    var prov = _providers.AsQueryable().Where(x => x.name == name && x.zone == z).FirstOrDefault();
                    var zon = _zones.AsQueryable().Where(x => x.name == z).FirstOrDefault();
                    if (prov == null && zon != null)
                    {
                        var res = await _providers.InsertOneAsync(new provider()
                        {
                            CreatedAt = Convert.ToDateTime(j.date),
                            name = name,
                            zone = z,
                            remarks = j.remarks,
                            status = j.status

                        });
                    }

                }
                var l = new logs()
                {
                    msg = "Provider Bulk Upload Done",
                    name = "EventLog",
                    uid = HttpContext.User.Identity.Name,
                    zone = "root",
                    remarks = "none",
                    subject = "Data Insertion",
                    CreatedAt = DateTime.Now
                };
                var r = await _log.InsertOneAsync(l);
                var result = new { status = "Provider Bulk Upload Done" };
                return Ok(result);
            }
            if (bulk.dbtable == "Package")
            {
                foreach (var j in json)
                {
                    string name = j.name;
                    string z = j.zone;
                    var prov = _packages.AsQueryable().Where(x => x.name == name).FirstOrDefault();
                    var zon = _zones.AsQueryable().Where(x => x.name == z).FirstOrDefault();
                    if (prov == null && zon != null)
                    {
                        var res = await _packages.InsertOneAsync(new package()
                        {
                            CreatedAt = Convert.ToDateTime(j.date),
                            name = name,
                            zone = z,
                            remarks = j.remarks,
                            status = j.status,
                            amount = j.amount,
                            type = j.type

                        });
                    }

                }
                var l = new logs()
                {
                    msg = "Provider Bulk Upload Done",
                    name = "EventLog",
                    uid = HttpContext.User.Identity.Name,
                    zone = "root",
                    remarks = "none",
                    subject = "Data Insertion",
                    CreatedAt = DateTime.Now
                };
                var r = await _log.InsertOneAsync(l);
                var result = new { status = "Provider Bulk Upload Done" };
                return Ok(result);
            }
            if (bulk.dbtable == "Customer Bulk Info")
            {
                await _customers.DeleteManyAsync(x => true);
                await _stbss.DeleteManyAsync(x => true);
                await _invoicess.DeleteManyAsync(x => true);
                await _collections.DeleteManyAsync(x => true);
                foreach (var c in json.customer)
                {
                    string cid = c.cusid;
                    string z = c.zone;
                    var zon = _zones.AsQueryable().Where(x => x.name == z).FirstOrDefault();
                    var cus = _customers.AsQueryable().Where(x => x.cid == cid && x.zone == z).FirstOrDefault();
                    if (cus == null && zon != null)
                    {
                        try
                        {
                            var res = await _customers.InsertOneAsync(new customer()
                            {
                                address = c.address,
                                area = c.area,
                                cid = c.cusid,
                                city = c.city,
                                name = c.cusname,
                                country = c.country,
                                CreatedAt = Convert.ToDateTime(c.createddate),
                                createdBy = c.creadtedby,
                                discount = c.discount,
                                email = c.email,
                                noofstb = c.noofstb,
                                phone = c.phone,
                                pincode = c.pincode,
                                provider = c.provider,
                                remarks = c.remarks,
                                state = c.state,
                                status = c.status,
                                zone = c.zone
                            });
                        }
                        catch (Exception ex)
                        {

                        }
                        foreach (var st in json.stb)
                        {
                            string stno = st.stdno;
                            if (st.cid == cid && stno != "")
                            {
                                try
                                {
                                    await _stbss.InsertOneAsync(new stb()
                                    {
                                        addonpack = st.addonpack,
                                        amount = st.amount,
                                        cid = st.cid,
                                        CreatedAt = Convert.ToDateTime(st.cdate),
                                        mdate = DateTime.Now,
                                        doorno = st.doorno,
                                        planname = st.planname,
                                        remarks = st.remaks,
                                        status = st.status,
                                        stbno = st.stdno,
                                        type = st.type,
                                        zone = st.zone
                                    });
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                        foreach (var i in json.inv)
                        {
                            if (i.cid == cid)
                            {
                                string sts = i.date;
                                DateTime d1 = sts == "0000-00-00 00:00:00" ? DateTime.Now : Convert.ToDateTime(sts);
                                try
                                {
                                    await _invoicess.InsertOneAsync(new invoice()
                                    {
                                        amount = i.amount,
                                        balance = i.balance,
                                        cid = i.cid,
                                        comments = "",
                                        invid = i.invid,
                                        CreatedAt = d1,
                                        month = Convert.ToInt32(i.month),
                                        year = i.year == "" ? d1.Year : Convert.ToInt32(i.year),
                                        noofstbs = i.noofstb,
                                        remarks = i.remarks,
                                        status = i.status,
                                        zone = i.zone
                                    });
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                        foreach (var p in json.col)
                        {
                            string sts = p.date;
                            if (p.cid == cid)
                            {
                                var blsts = new List<balsts>();
                                var inv = new List<invs>();
                                string bls = p.balsts;
                                var ivs = !bls.StartsWith("[") ? new List<upIV>() { new upIV() { amt = p.amount, invid = p.invid } } : (bls == "[]" ? new List<upIV>() : Newtonsoft.Json.JsonConvert.DeserializeObject<List<upIV>>(bls));
                                foreach (var i in ivs)
                                {
                                    string invids = i.invid;
                                    if (invids != "advance")
                                    {

                                        inv.Add(new invs()
                                        {
                                            invId = _invoicess.AsQueryable().Where(x => x.invid == invids).FirstOrDefault() == null ? new ObjectId().ToString() : _invoicess.AsQueryable().Where(x => x.invid == invids).FirstOrDefault().Id.ToString()
                                        });
                                        blsts.Add(new balsts()
                                        {
                                            amt = i.amt,
                                            invId = _invoicess.AsQueryable().Where(x => x.invid == invids).FirstOrDefault() == null ? new ObjectId().ToString() : _invoicess.AsQueryable().Where(x => x.invid == invids).FirstOrDefault().Id.ToString()
                                        });


                                    }
                                    else
                                    {
                                        inv.Add(new invs()
                                        {
                                            invId = "advance"
                                        });
                                        blsts.Add(new balsts()
                                        {
                                            amt = i.amt,
                                            invId = "advance"
                                        });
                                    }

                                }
                                try
                                {
                                    await _collections.InsertOneAsync(new collection()
                                    {
                                        cid = p.cid,
                                        amount = p.amount,
                                        balance = p.balance,
                                        collectedby = p.remarks,
                                        comments = p.comment,
                                        CreatedAt = sts == "0000-00-00 00:00:00" ? DateTime.Now : Convert.ToDateTime(p.date),
                                        payid = p.payid,
                                        paymentMode = p.mode,
                                        paymenyNote = p.paynote,
                                        remarks = "none",
                                        status = p.status,
                                        zone = p.zone,
                                        invid = inv,
                                        balancests = blsts

                                    });
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }


                    }
                }
                var l = new logs()
                {
                    msg = "Customer Bulk Info  Upload Done",
                    name = "EventLog",
                    uid = HttpContext.User.Identity.Name,
                    zone = "root",
                    remarks = "none",
                    subject = "Data Insertion",
                    CreatedAt = DateTime.Now
                };
                var r = await _log.InsertOneAsync(l);
                var result = new { status = "Customer Bulk Info Upload Done" };
                return Ok(result);
            }
            return BadRequest("Unable to process Request");
        }
        [HttpPost]
        public async Task<IActionResult> EmailConfigSetting([FromBody] EmailSchema mail)
        {
            var ml = _emails.AsQueryable().Where(x => x.zone == mail.zone).FirstOrDefault();
            if ((string.IsNullOrEmpty(mail.Id) || mail.Id.Equals("000000000000000000000000")) && ml==null)
            {
               
                var res = await _emails.InsertOneAsync(new email()
                {
                    CreatedAt = DateTime.Now,
                    emailaddress = mail.emailaddress,
                    name = mail.name,
                    password = mail.password,
                    remarks = "none",
                    smtpport = mail.smtpport,
                    smtpserver = mail.smtpserver,
                    zone = mail.zone,
                    status = "active"
                });
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "Email Insertion  Done",
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        zone = "root",
                        remarks = "none",
                        subject = "Data Insertion",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "Email Insertion  Done" };
                    return Ok(result);
                }
            }
            else if(!string.IsNullOrEmpty(mail.Id) && ml != null)
            {
                ml.emailaddress = mail.emailaddress;
                ml.name = mail.name;
                ml.password = mail.password;
                ml.smtpport = mail.smtpport;
                ml.smtpserver = mail.smtpserver;
                ml.zone = mail.zone;
                
                var res = await _emails.ReplaceOneAsync(ml);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "Email Updation  Done",
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        zone = "root",
                        remarks = "none",
                        subject = "Data Insertion",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "Email Updation  Done" };
                    return Ok(result);
                }
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> Announcement([FromBody] AnnouncementSchema announcement)
        {
            var cusids = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(announcement.customers);
            

            return BadRequest("Invalid Request");
        }
    }
}