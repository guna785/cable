using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BL.DataTableModel;
using BL.Extentions;
using BL.Helper;
using BL.Models;
using BL.service;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace cable.Controllers
{
    [Authorize]
    public class ViewDataController : Controller
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
        private string zone;
        public ViewDataController(IGenericBL<user> users, IGenericBL<customer> customers, IGenericBL<area> areas, IGenericBL<package> packages,
            IGenericBL<zone> zones, IGenericBL<provider> providers, IGenericBL<logs> log, IGenericBL<role> roles, IGenericBL<sms> smss,
            IGenericBL<stb> stbss, IGenericBL<email> emails, IGenericBL<gateway> gateways, IGenericBL<collection> collections, IGenericBL<invoice> invoicess,
            IGenericBL<admin> admins)
        {
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
            _admins = admins;

        }
        [HttpPost]
        public async Task<IActionResult> LoadUsers([FromBody] DtParameters dtParameters)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = zone == "root" ? _users.AsQueryable().Where(x => x.status != "Deleted") : _users.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.name != null && r.name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.email != null && r.email.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.phone != null && r.phone.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = zone == "root" ? _users.AsQueryable().Where(x => x.status != "Deleted") : _users.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadAdmins([FromBody] DtParameters dtParameters)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = _admins.AsQueryable().Where(x => x.status != "Deleted");

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.name != null && r.name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.uname != null && r.uname.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.email != null && r.email.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.phone != null && r.phone.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _admins.AsQueryable().Where(x => x.status != "Deleted");

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadCustomers([FromBody] DtParameters dtParameters)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = zone == "root" ? _customers.AsQueryable().Where(x => x.status != "Deleted") : _customers.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.name != null && r.name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.email != null && r.email.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.phone != null && r.phone.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = zone == "root" ? _customers.AsQueryable().Where(x => x.status != "Deleted") : _customers.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadSTBs([FromBody] DtParameters dtParameters)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = zone == "root" ? _stbss.AsQueryable().Where(x => x.status != "Deleted") : _stbss.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.stbno != null && r.stbno.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.cid != null && r.cid.ToUpper().Contains(searchBy.ToUpper()) ||
                                            r.type != null && r.type.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = zone == "root" ? _stbss.AsQueryable().Where(x => x.status != "Deleted") : _stbss.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList().Select(x => new BlSTB()
                    {
                        Id = x.Id,
                        stbno = x.stbno,
                        addonpack = x.addonpack,
                        cid = x.cid,
                        CreatedAt = x.CreatedAt,
                        package = x.planname,
                        status = x.status,
                        zone = x.zone,
                        stbtype = x.type
                    })
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadInvoices([FromBody] DtParameters dtParameters)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = zone == "root" ? _invoicess.AsQueryable().Where(x => x.status != "Deleted") : _invoicess.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.invid != null && r.invid.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.amount != null && r.amount.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.cid != null && r.cid.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = zone == "root" ? _invoicess.AsQueryable().Where(x => x.status != "Deleted") : _invoicess.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length).ToList().Select(x => new BlInvoice()
                    {
                        Id = x.Id,
                        amount = x.amount,
                        balance = x.balance,
                        cid = x.cid,
                        CreatedAt = x.CreatedAt,
                        invid = x.zone + "/" + x.invid,
                        status = x.status,
                        zone = x.zone,
                        month = DateConfig.getMonthStingfromNumber(x.month)
                    })
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadPayments([FromBody] DtParameters dtParameters)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = zone == "root" ? _collections.AsQueryable().Where(x => x.status != "Deleted") : _collections.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.payid != null && r.payid.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.cid != null && r.cid.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.collectedby != null && r.collectedby.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _collections.AsQueryable().Where(x => x.status != "Deleted");

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList().Select(x => new collection()
                    {
                        CreatedAt = x.CreatedAt,
                        amount = x.amount,
                        balance = x.balance,
                        collectedby = x.collectedby,
                        payid = x.payid,
                        cid = x.cid,
                        paymentMode = x.paymentMode,
                        paymenyNote = x.paymenyNote,
                        remarks = x.remarks,
                        comments = x.comments,
                        status = x.status,
                        zone = x.zone,
                        Id = x.Id,
                        invid = x.invid.Select(y => new invs()
                        {
                            invId = y.invId == "advance" ? "advance" : _invoicess.AsQueryable().Where(z => z.Id == ObjectId.Parse(y.invId)).FirstOrDefault().invid
                        }).ToList(),
                        balancests = x.balancests

                    })
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadTechnitians([FromBody] DtParameters dtParameters)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = zone == "root" ? _users.AsQueryable().Where(x => x.status != "Deleted") : _users.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.uname != null && r.uname.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.name != null && r.name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.phone != null && r.phone.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = zone == "root" ? _users.AsQueryable().Where(x => x.status != "Deleted") : _users.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadZones([FromBody] DtParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = _zones.AsQueryable().Where(x => x.status != "Deleted");

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.name != null && r.name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.remarks != null && r.remarks.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _zones.AsQueryable().Where(x => x.status != "Deleted");

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadAreas([FromBody] DtParameters dtParameters)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = _areas.AsQueryable().Where(x => x.status != "Deleted");

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.name != null && r.name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _areas.AsQueryable().Where(x => x.status != "Deleted");

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadProviders([FromBody] DtParameters dtParameters)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = _providers.AsQueryable().Where(x => x.status != "Deleted");

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.name != null && r.name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.remarks != null && r.remarks.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _providers.AsQueryable().Where(x => x.status != "Deleted");

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadPackage([FromBody] DtParameters dtParameters)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = zone == "root" ? _packages.AsQueryable().Where(x => x.status != "Deleted") : _packages.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.name != null && r.name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.type != null && r.type.ToUpper().Contains(searchBy.ToUpper())
                                          //r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = zone == "root" ? _packages.AsQueryable().Where(x => x.status != "Deleted") : _packages.AsQueryable().Where(x => x.status != "Deleted" && x.zone == zone);
            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadCustomerSTB([FromBody] DtParameters dtParameters, string Id)
        {
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }
            var cus = _customers.AsQueryable().Where(x => x.Id == ObjectId.Parse(Id)).FirstOrDefault() == null ? new customer() : _customers.AsQueryable().Where(x => x.Id == ObjectId.Parse(Id)).FirstOrDefault();
            var result = _stbss.AsQueryable().Where(x => x.status != "Deleted" && x.cid.Equals(cus.cid));

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.stbno != null && r.stbno.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.type != null && r.type.ToUpper().Contains(searchBy.ToUpper()) ||
                                            r.planname != null && r.planname.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _stbss.AsQueryable().Where(x => x.status != "Deleted" && x.cid.Equals(cus.cid));

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadCustomerInvoice([FromBody] DtParameters dtParameters, string Id)
        {
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }
            var cus = _customers.AsQueryable().Where(x => x.Id == ObjectId.Parse(Id)).FirstOrDefault() == null ? new customer() : _customers.AsQueryable().Where(x => x.Id == ObjectId.Parse(Id)).FirstOrDefault();
            var result = _invoicess.AsQueryable().Where(x => x.status != "Deleted" && x.cid.Equals(cus.cid)).AsEnumerable().Select(x => new BlInvoice()
            {
                amount = x.amount,
                balance = x.balance,
                cid = x.cid,
                CreatedAt = x.CreatedAt,
                Id = x.Id,
                invid = x.invid,
                month = DateConfig.getMonthStingfromNumber(x.CreatedAt.Month),
                status = x.status,
                zone = x.zone
            });

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.invid != null && r.invid.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.cid != null && r.cid.ToUpper().Contains(searchBy.ToUpper()) ||
                                            r.month != null && r.month.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _invoicess.AsQueryable().Where(x => x.status != "Deleted" && x.cid.Equals(cus.cid));

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadCustomerPayment([FromBody] DtParameters dtParameters, string Id)
        {
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }
            var cus = _customers.AsQueryable().Where(x => x.Id == ObjectId.Parse(Id)).FirstOrDefault() == null ? new customer() : _customers.AsQueryable().Where(x => x.Id == ObjectId.Parse(Id)).FirstOrDefault();
            var result = _collections.AsQueryable().Where(x => x.status != "Deleted" && x.cid.Equals(cus.cid));

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r =>
                                           r.cid != null && r.cid.ToUpper().Contains(searchBy.ToUpper()) ||
                                            r.payid != null && r.payid.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.status != null && r.status.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.collectedby != null && r.collectedby.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _collections.AsQueryable().Where(x => x.status != "Deleted" && x.cid.Equals(cus.cid));

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> LoadLogs([FromBody] DtParameters dtParameters, string Id)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var rols = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            if (rols.FirstOrDefault().Value == "admin")
            {
                zone = _admins.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "Collection" || rols.FirstOrDefault().Value == "Technitian")
            {
                zone = _users.AsQueryable().Where(x => x.uname == HttpContext.User.Identity.Name).FirstOrDefault().zone;
            }
            else if (rols.FirstOrDefault().Value == "root")
            {
                zone = "root";
            }
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = zone == "root" ? _log.AsQueryable().Where(x => x.name == Id) : _log.AsQueryable().Where(x => x.name == Id && x.zone == zone);

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.name != null && r.name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.uid != null && r.uid.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = zone == "root" ? _log.AsQueryable().Where(x => x.name == Id) : _log.AsQueryable().Where(x => x.name == Id && x.zone == zone);

            var totalResultsCount = cntdb.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
    }
}
