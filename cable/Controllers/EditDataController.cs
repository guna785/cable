using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using BL.SchemaModel;
using BL.security;
using BL.service;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cable.Controllers
{
    [Authorize]
    public class EditDataController : Controller
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
        private IGenericBL<admin> _admins;
        private IGenericBL<userlogic> _userlogics;
        public EditDataController(IGenericBL<user> users, IGenericBL<customer> customers, IGenericBL<area> areas, IGenericBL<package> packages,
            IGenericBL<zone> zones, IGenericBL<provider> providers, IGenericBL<logs> log, IGenericBL<role> roles, IGenericBL<sms> smss,
            IGenericBL<stb> stbss, IGenericBL<email> emails, IGenericBL<gateway> gateways, IGenericBL<collection> collections, IGenericBL<invoice> invoicess,
            IGenericBL<admin> admins, IGenericBL<userlogic> userlogics)
        {
            _admins = admins;
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
            _userlogics = userlogics;
        }
        [HttpPost]
        public async Task<IActionResult> EditZone([FromBody] EditZoneSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var d = _zones.FindById(zon.Id);
            if (d != null)
            {
                d.name = zon.name;
                d.remarks = zon.remarks;
                d.status = zon.status;
                d.web = zon.web;
                d.photo = zon.photo == null ? d.photo : zon.photo;
                d.email = zon.email;
                d.Cphone = zon.Cphone;
                d.Cname = zon.Cname;
                d.caddress = zon.caddress;
                

                var res = await _zones.ReplaceOneAsync(d);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "Zone Name  " + zon.name + " is Updated Successfully",
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        remarks = "none",
                        subject = "Data Updation",
                        zone = "root",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "Zone Sucessfully Updated" };
                    return Ok(result);
                }
            }
          
            return BadRequest("Bad Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditArea([FromBody] EditAreaSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var d = _areas.FindById(zon.Id);
            if (d != null)
            {
                d.name = zon.name;
                d.remarks = zon.remarks;
                d.status = zon.status;
                d.zone = zon.zone;
               

                var res = await _areas.ReplaceOneAsync(d);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "Area Name  " + zon.name + " is Updated Successfully",
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        remarks = "none",
                        subject = "Data Updation",
                        zone = "root",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "Area Sucessfully Updated" };
                    return Ok(result);
                }
            }

            return BadRequest("Bad Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditProvider([FromBody] EditProviderSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var d = _providers.FindById(zon.Id);
            if (d != null)
            {
                d.name = zon.name;
                d.remarks = zon.remarks;
                d.status = zon.status;
                d.zone = zon.zone;              


                var res = await _providers.ReplaceOneAsync(d);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "Provider Name  " + zon.name + " is Updated Successfully",
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        remarks = "none",
                        subject = "Data Updation",
                        zone = "root",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "Provider Sucessfully Updated" };
                    return Ok(result);
                }
            }

            return BadRequest("Bad Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditPackage([FromBody] EditPackageSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var d = _packages.FindById(zon.Id);
            if (d != null)
            {
                d.name = zon.name;
                d.remarks = zon.remarks;
                d.status = zon.status;
                d.zone = zon.zone;
                d.type = zon.type;
                d.amount = zon.amount;


                var res = await _packages.ReplaceOneAsync(d);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "Provider Name  " + zon.name + " is Updated Successfully",
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        remarks = "none",
                        subject = "Data Updation",
                        zone = "root",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "Provider Sucessfully Updated" };
                    return Ok(result);
                }
            }

            return BadRequest("Bad Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditAdmin([FromBody] EditAdminSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var d = _admins.FindById(zon.Id);
            if (d != null)
            {
                d.name = zon.name;
                d.remarks = zon.remarks;
                d.status = zon.status;
                d.phone = zon.phone;
                d.gst = zon.gst;
                d.email = zon.email;
                d.address = zon.address;
                d.remarks = zon.remarks;
                d.uname = zon.uname;
                d.zone = zon.zone;
                d.photo = zon.photo == null ? d.photo : zon.photo;
                if (!string.IsNullOrWhiteSpace(zon.password))
                {
                    var salt = HasherService.GenerateSalt();
                    var hashedPassword = HasherService.HashPasswordWithSalt(Encoding.UTF8.GetBytes(zon.password), salt);
                    d.password = Convert.ToBase64String(hashedPassword);
                    var logic = _userlogics.AsQueryable().Where(x => x.uid == d.Id).FirstOrDefault();
                    logic.uid = d.Id;
                    logic.usrToken = salt;
                    await _userlogics.ReplaceOneAsync(logic);
                }

                var res = await _admins.ReplaceOneAsync(d);
              
                if (res)
                {
                    var l = new logs()
                    {
                        msg = "Admin User Name  " + zon.uname + " is Updated Successfully",
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        remarks = "none",
                        subject = "Data Updation",
                        zone = "root",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "Admin User Sucessfully Updated" };
                    return Ok(result);
                }
            }

            return BadRequest("Bad Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditCustomer([FromBody] EditCustomerSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var d = _customers.FindById(zon.Id);
            if (d != null)
            {
                d.name = zon.name;
                d.remarks = zon.remarks;
                d.status = zon.status;
                d.phone = zon.phone;
                d.email = zon.email;
                d.address = zon.address;
                d.remarks = zon.remarks;
                d.zone = zon.zone;
                d.state = zon.state;
                d.provider = zon.provider;
                d.pincode = zon.pincode;
                d.discount = zon.discount;
                d.country = zon.country;
                d.city = zon.city;
                d.cid = d.cid;
                d.area = d.area;
                
                var res = await _customers.ReplaceOneAsync(d);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = " Customer ID  " + zon.cid + " is Updated Successfully",
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        remarks = "none",
                        subject = "Data Updation",
                        zone = "root",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "Customer Sucessfully Updated" };
                    return Ok(result);
                }
            }

            return BadRequest("Bad Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditUser([FromBody] EditTechnitianSchema zon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request");
            }
            var d = _users.FindById(zon.Id);
            if (d != null)
            {
                d.name = zon.name;
                d.remarks = zon.remarks;
                d.status = zon.status;
                d.phone = zon.phone;
                d.email = zon.email;
                d.address = zon.address;
                d.remarks = zon.remarks;
                d.uname = zon.uname;
                d.zone = zon.zone;
                d.type = zon.type;
                if (!string.IsNullOrWhiteSpace(zon.password))
                {
                    var salt = HasherService.GenerateSalt();
                    var hashedPassword = HasherService.HashPasswordWithSalt(Encoding.UTF8.GetBytes(zon.password), salt);
                    d.password = Convert.ToBase64String(hashedPassword);
                    var logic = _userlogics.AsQueryable().Where(x => x.uid == d.Id).FirstOrDefault();
                    logic.uid = d.Id;
                    logic.usrToken = salt;
                    await _userlogics.ReplaceOneAsync(logic);
                }

                var res = await _users.ReplaceOneAsync(d);
                if (res)
                {
                    var l = new logs()
                    {
                        msg = " User Name  " + zon.uname + " is Updated Successfully",
                        name = "EventLog",
                        uid = HttpContext.User.Identity.Name,
                        remarks = "none",
                        subject = "Data Updation",
                        zone = "root",
                        CreatedAt = DateTime.Now
                    };
                    var r = await _log.InsertOneAsync(l);
                    var result = new { status = "User Sucessfully Updated" };
                    return Ok(result);
                }
            }

            return BadRequest("Bad Request");
        }
    }
}
