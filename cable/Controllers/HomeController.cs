using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cable.Models;
using System.Security.Policy;
using DAL.Models;
using GSchema;
using BL.SchemaModel;
using BL.SchemaEditBuilder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BL.service;
using QRCoder;
using System.Drawing;
using BL.Models;
using System.IO;
using Rotativa.AspNetCore;
using BL.Helper;

namespace cable.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EditBuilder _builder;
        private IGenericBL<user> _users;
        private IGenericBL<admin> _admins;
        private IGenericBL<customer> _customers;
        private IGenericBL<invoice> _invoices;
        private IGenericBL<collection> _collections;
        private IGenericBL<zone> _zones;
        private IGenericBL<stb> _stbs;
        private string schema;
        public HomeController(ILogger<HomeController> logger, EditBuilder builder, IGenericBL<user> users, IGenericBL<admin> admins, IGenericBL<customer> customers,
            IGenericBL<invoice> invoices, IGenericBL<collection> collections, IGenericBL<zone> zones, IGenericBL<stb> stbs)
        {
            _logger = logger;
            _builder = builder;
            _users = users;
            _admins = admins;
            _zones = zones;
            _customers = customers;
            _invoices = invoices;
            _stbs = stbs;
            _collections = collections;
        }
        private string zon;
        public IActionResult Index()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zon = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zon = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zon = "root";
            }
            ViewBag.activeCus = zon=="root"? _customers.AsQueryable().Where(x => x.status == "active").Count(): _customers.AsQueryable().Where(x => x.status == "active" && x.zone==zon).Count();
            ViewBag.allCus =zon=="root"? _customers.AsQueryable().Count() : _customers.AsQueryable().Where(x=>x.zone==zon).Count();
            ViewBag.deactiveCus = zon == "root" ? _customers.AsQueryable().Where(x => x.status == "deactive").Count(): _customers.AsQueryable().Where(x => x.status == "deactive" && x.zone==zon).Count();
            ViewBag.DeletedCus = zon == "root" ? _customers.AsQueryable().Where(x => x.status.ToLower() == "deleted").Count() : _customers.AsQueryable().Where(x => x.status.ToLower() == "deleted" && x.zone==zon).Count();
            ViewBag.allSTB = zon == "root" ? _stbs.AsQueryable().Count() : _stbs.AsQueryable().Where(x=>x.zone==zon).Count();
            ViewBag.allotedSTB = zon == "root" ? _stbs.AsQueryable().Where(x => x.cid != "available").Count(): _stbs.AsQueryable().Where(x => x.cid != "available" && x.zone==zon).Count();
            ViewBag.unallotedSTB = zon == "root" ? _stbs.AsQueryable().Where(x => x.cid == "available").Count(): _stbs.AsQueryable().Where(x => x.cid == "available" && x.zone==zon).Count();
            ViewBag.allInv = zon == "root" ? _invoices.AsQueryable().Count(): _invoices.AsQueryable().Where(x=>x.zone==zon).Count();
            ViewBag.paidInvoice = zon == "root" ? _invoices.AsQueryable().Where(x=>x.status== "paid" || x.status.ToLower()== "partiallypaid").Count() : _invoices.AsQueryable().Where(x =>( x.status == "paid" || x.status.ToLower() == "partiallypaid") && x.zone==zon).Count();
            ViewBag.unpaidInvoice = zon == "root" ? _invoices.AsQueryable().Where(x => x.status == "unpaid").Count() : _invoices.AsQueryable().Where(x => x.status == "unpaid" && x.zone==zon).Count();
            ViewBag.usr = zon == "root" ? _users.AsQueryable().Count(): _users.AsQueryable().Where(x=>x.zone==zon).Count();
            ViewBag.activeusr = zon == "root" ? _users.AsQueryable().Where(x => x.status.ToLower() == "active").Count(): _users.AsQueryable().Where(x => x.status.ToLower() == "active" && x.zone==zon).Count();
            ViewBag.deactiveusr = zon == "root" ? _users.AsQueryable().Where(x => x.status.ToLower() == "deactive").Count(): _users.AsQueryable().Where(x => x.status.ToLower() == "deactive" && x.zone==zon).Count();
            return View();
        }
        public IActionResult Customer()
        {
            return View();
        }
        public IActionResult EditCustomer(string Id)
        {
            var ids = Id.Split('-');
            ViewBag.ID = ids[0];
            ViewBag.cid = ids[1];
            return View();
        }
        public IActionResult Stbs()
        {
            return View();
        }
        public IActionResult invoice()
        {
            return View();
        }
        public IActionResult payment()
        {
            return View();
        }
        public IActionResult Technitian()
        {
            return View();
        }
        public IActionResult Package()
        {
            return View();
        }
        public IActionResult Provider()
        {
            return View();
        }
        public IActionResult Area()
        {
            return View();
        }
        public IActionResult Zone()
        {
            return View();
        }
        public IActionResult Email()
        {
            return View();
        }
        public IActionResult Announcement()
        {
            return View();
        }
        public IActionResult Admins()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult BulkUpload()
        {
            return View();
        }
        public IActionResult Logs(string Id)
        {
            ViewBag.id = Id;
            return View();
        }
        public IActionResult QRcode()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            var zone = "";
            if (roles.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (roles.FirstOrDefault().Value == "Collection" || roles.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (roles.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }
            var list = new List<QrModel>();
            var cus = zone == "root" ? _customers.AsQueryable() : _customers.AsQueryable().Where(x => x.zone == zone);

            foreach (var c in cus)
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(c.cid, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                list.Add(new QrModel()
                {
                    cid = c.cid,
                    img = BitmapToBytes(qrCodeImage),
                    name = c.name,
                    zone = c.zone
                });
            }

            ViewBag.lst = list;
            return View();
        }
        public IActionResult DataTableRenderer(string Id)
        {
            return View();
        }

        public IActionResult invoiceDownload(string Id)
        {
            var inv = _invoices.FindById(Id);
            
            if (inv != null)
            {
                var cus = _customers.AsQueryable().Where(x => x.cid == inv.cid).FirstOrDefault()==null?new customer(): _customers.AsQueryable().Where(x => x.cid == inv.cid).FirstOrDefault();
                var adm = _zones.AsQueryable().Where(x => x.name == inv.zone).FirstOrDefault();
                var voucher = new InvPrintrintModel()
                {
                    inv = inv,
                    cus = cus,
                    adm = adm
                };
                return new ViewAsPdf("invoiceDownload", voucher, null);
            }
            return new ViewAsPdf("invoiceDownload", new InvPrintrintModel(), null);
        }
        public IActionResult RecieptDownload(string Id)
        {
            return null;
        }
        public IActionResult Privacy()
        {
            return View();
        }
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        public async Task<IActionResult> PopUpModelShow(string ID)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            var zone = "";
            if (roles.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (roles.FirstOrDefault().Value == "Collection" || roles.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (roles.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }

            if (ID.Contains("AddZone"))
            {
                schema = await GSgenerator.GenerateSchema<ZoneSchema>(zone);
                ViewBag.modalTitle = "AddZone";
            }
            else if (ID.Contains("EditZone"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<EditZoneSchema>(objId);

                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                schema = await GSgenerator.GenerateSchema<EditZoneSchema>(zone);
                ViewBag.modalTitle = "EditZone";
            }
            if (ID.Contains("AddAdmin"))
            {
                schema = await GSgenerator.GenerateSchema<AdminSchema>(zone);
                ViewBag.modalTitle = "AddAdmin";
            }
            else if (ID.Contains("EditAdmin"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<EditAdminSchema>(objId);

                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                schema = await GSgenerator.GenerateSchema<EditAdminSchema>(zone);
                ViewBag.modalTitle = "EditAdmin";
            }
            if (ID.Contains("AddArea"))
            {
                schema = await GSgenerator.GenerateSchema<AreaSchema>(zone);
                ViewBag.modalTitle = "AddArea";
            }
            else if (ID.Contains("EditArea"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<EditAreaSchema>(objId);

                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                schema = await GSgenerator.GenerateSchema<EditAreaSchema>(zone);
                ViewBag.modalTitle = "EditArea";
            }
            if (ID.Contains("AddProvider"))
            {
                schema = await GSgenerator.GenerateSchema<ProviderSchema>(zone);
                ViewBag.modalTitle = "AddProvider";
            }
            else if (ID.Contains("EditProvider"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<EditProviderSchema>(objId);

                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                schema = await GSgenerator.GenerateSchema<EditProviderSchema>(zone);
                ViewBag.modalTitle = "EditProvider";
            }
            if (ID.Contains("AddPackage"))
            {
                schema = await GSgenerator.GenerateSchema<PackageSchema>(zone);
                ViewBag.modalTitle = "AddPackage";
            }
            else if (ID.Contains("EditPackage"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<EditPackageSchema>(objId);

                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                schema = await GSgenerator.GenerateSchema<EditPackageSchema>(zone);
                ViewBag.modalTitle = "EditPackage";
            }
            if (ID.Contains("AddUser"))
            {
                schema = await GSgenerator.GenerateSchema<TechnitianSchema>(zone);
                ViewBag.modalTitle = "AddUser";
            }
            else if (ID.Contains("EditUser"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<EditTechnitianSchema>(objId);

                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                schema = await GSgenerator.GenerateSchema<EditTechnitianSchema>(zone);
                ViewBag.modalTitle = "EditUser";
            }
            if (ID.Contains("AddCustomer"))
            {
                schema = await GSgenerator.GenerateSchema<CustomerSchema>(zone);
                ViewBag.modalTitle = "AddCustomer";
            }
            else if (ID.Contains("EditCustomer"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<EditCustomerSchema>(objId);

                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                schema = await GSgenerator.GenerateSchema<EditCustomerSchema>(zone);
                ViewBag.modalTitle = "EditCustomer";
            }
            if (ID.Contains("AddCusSTB"))
            {
                var objId = ID.Split('-')[1];
                schema = await GSgenerator.GenerateSchema<cusSTBSchema>(zone);
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(new cusSTBSchema()
                {
                    addonpack = "",
                    Id = "",
                    planname = "",
                    stbno = "",
                    type = "",
                    zone = "",
                    cusID = objId
                });
                ViewBag.modalTitle = "AddCusSTB";
            }
            else if (ID.Contains("EditCusSTB"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<cusSTBSchema>(objId);

                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                schema = await GSgenerator.GenerateSchema<cusSTBSchema>(zone);
                ViewBag.modalTitle = "AddCusSTB";
            }
            if (ID.Contains("GenerateCusINV"))
            {
                schema = await GSgenerator.GenerateSchema<GenerateInvSchema>(zone);
                var objId = ID.Split('-')[1];
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(new GenerateInvSchema()
                {
                    cid = objId
                });
                ViewBag.modalTitle = "GenerateCusINV";
            }
            if (ID.Contains("GenerateBulkInvoice"))
            {
                schema = await GSgenerator.GenerateSchema<GenerateBulkInvSchema>(zone);
               // var objId = ID.Split('-')[1];
                ViewBag.modalTitle = "GenerateBulkInvoice";
            }
            if (ID.Contains("PayInvoice"))
            {
                var objId = ID.Split('-')[1];
                var amt = ID.Split('-')[2];
                var invPay = ID.Split('-')[3];
                schema = await GSgenerator.GenerateSchema<PayInvoiceSchema>(zone, amt);

                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(new PayInvoiceSchema()
                {
                    Id = objId,
                    amount = Convert.ToInt32(amt),
                    payonlyinv = invPay == "y" ? true : false
                });
                ViewBag.modalTitle = "PayInvoice";
            }
            if (ID.Contains("UploadBulk"))
            {
                schema = await GSgenerator.GenerateSchema<BulkUploadSchema>(zone);
                ViewBag.modalTitle = "UploadBulk";
            }
            if (ID.Contains("EmailSetting"))
            {
                schema = await GSgenerator.GenerateSchema<EmailSchema>(zone);
                var data = await _builder.ReturnObjectData<EmailSchema>(zone);

                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                ViewBag.modalTitle = "EmailSetting";

            }
            if (ID.Contains("Announcement"))
            {
                schema = await GSgenerator.GenerateSchema<AnnouncementSchema>(zone);
                ViewBag.modalTitle = "Announcement";
            }
            ViewBag.schema = schema;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> graphData([FromBody] string Id)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zon = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zon = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zon = "root";
            }
            var inv = zon=="root"? _invoices.AsQueryable().Where(x => x.year == Convert.ToInt32(Id)).AsEnumerable().GroupBy(x => new { x.month }).Select(x => new
            {
                month =x.Key.month,
                amount = x.AsEnumerable().Sum(x => (Convert.ToInt32(x.amount)))
            }).ToList() :
            _invoices.AsQueryable().Where(x => x.year == Convert.ToInt32(Id) && x.zone==zon).AsEnumerable().GroupBy(x => new { x.month }).Select(x => new
            {
                month = x.Key.month,
                amount = x.AsEnumerable().Sum(x => (Convert.ToInt32(x.amount)))
            }).ToList();
            var paidInv= zon == "root" ? _invoices.AsQueryable().Where(x => x.year == Convert.ToInt32(Id) && (x.status=="paid" || x.status== "PartiallyPaid")).AsEnumerable().GroupBy(x => new { x.month }).Select(x => new
            {
                month = x.Key.month,
                amount = x.AsEnumerable().Sum(x =>( Convert.ToInt32(x.amount)-(x.balance==""?0:Convert.ToInt32(x.balance))))
            }).ToList() :
            _invoices.AsQueryable().Where(x => x.year == Convert.ToInt32(Id) && x.zone==zon && (x.status == "paid" || x.status == "PartiallyPaid")).AsEnumerable().GroupBy(x => new { x.month }).Select(x => new
            {
                month = x.Key.month,
                amount = x.AsEnumerable().Sum(x => (Convert.ToInt32(x.amount) - (x.balance == "" ? 0 : Convert.ToInt32(x.balance))))
            }).ToList();
            var unpaidInv = zon == "root" ? _invoices.AsQueryable().Where(x => x.year == Convert.ToInt32(Id) &&( x.status == "unpaid" || x.status == "PartiallyPaid")).AsEnumerable().GroupBy(x => new { x.month }).Select(x => new
            {
                month = x.Key.month,
                amount = x.AsEnumerable().Sum(x => Convert.ToInt32(x.balance))
            }).ToList():
            _invoices.AsQueryable().Where(x => x.year == Convert.ToInt32(Id) && x.zone==zon && (x.status == "unpaid" || x.status == "PartiallyPaid")).AsEnumerable().GroupBy(x => new { x.month }).Select(x => new
            {
                month = x.Key.month,
                amount = x.AsEnumerable().Sum(x => Convert.ToInt32(x.balance))
            }).ToList();

            return Ok(new { 
             all=inv.OrderBy(x=>x.month).Select(x=>new {month=DateConfig.getMonthStingfromNumber(x.month),amount=x.amount }),

             paid=paidInv.OrderBy(x => x.month).Select(x => new { month = DateConfig.getMonthStingfromNumber(x.month), amount = x.amount }),
             unpaid=unpaidInv.OrderBy(x => x.month).Select(x => new { month = DateConfig.getMonthStingfromNumber(x.month), amount = x.amount })
            });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId =  HttpContext.TraceIdentifier });
        }
    }
}
