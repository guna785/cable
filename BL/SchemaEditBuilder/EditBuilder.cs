using BL.SchemaModel;
using BL.service;
using DAL.DALService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace BL.SchemaEditBuilder
{
    public class EditBuilder
    {
        private IGenericBL<zone> _zones;
        private IGenericBL<admin> _admins;
        private IGenericBL<area> _areas;
        private IGenericBL<provider> _providers;
        private IGenericBL<package> _packages;
        private IGenericBL<user> _users;
        private IGenericBL<customer> _customers;
        private IGenericBL<stb> _stbs;
        private IGenericBL<email> _emails;

        public EditBuilder(IGenericBL<zone> zones, IGenericBL<admin> admins, IGenericBL<area> areas, IGenericBL<provider> providers,
            IGenericBL<package> packages, IGenericBL<user> users, IGenericBL<customer> customers, IGenericBL<stb> stbs,
            IGenericBL<email> emails)
        {
            _zones = zones;
            _admins = admins;
            _areas = areas;
            _providers = providers;
            _packages = packages;
            _users = users;
            _customers = customers;
            _stbs = stbs;
            _emails = emails;
        }
        public async Task<T> ReturnObjectData<T>(string id)
        {
            var obj = typeof(T).Name;
            if (obj.Equals("EditZoneSchema"))
            {
                var obdata = _zones.FindById(id);
                return (T)Convert.ChangeType(new EditZoneSchema()
                {
                    name = obdata.name,
                    Id = obdata.Id.ToString(),
                    status = obdata.status,
                    remarks = obdata.remarks

                }, typeof(T));
            }
            if (obj.Equals("EditAdminSchema"))
            {
                var obdata = _admins.FindById(id);
                return (T)Convert.ChangeType(new EditAdminSchema()
                {
                    name = obdata.name,
                    Id = obdata.Id.ToString(),
                    uname = obdata.uname,
                    address = obdata.address,
                    email = obdata.email,
                    gst = obdata.gst,
                    password = "",
                    phone = obdata.phone,
                    zone = obdata.zone,
                    status = obdata.status,
                    remarks = obdata.remarks

                }, typeof(T));
            }
            if (obj.Equals("EditAreaSchema"))
            {
                var obdata = _areas.FindById(id);
                return (T)Convert.ChangeType(new EditAreaSchema()
                {
                    name = obdata.name,
                    Id = obdata.Id.ToString(),
                    zone = obdata.zone,
                    status = obdata.status,
                    remarks = obdata.remarks

                }, typeof(T));
            }
            if (obj.Equals("EditProviderSchema"))
            {
                var obdata = _providers.FindById(id);
                return (T)Convert.ChangeType(new EditProviderSchema()
                {
                    name = obdata.name,
                    Id = obdata.Id.ToString(),
                    zone = obdata.zone,
                    status = obdata.status,
                    remarks = obdata.remarks

                }, typeof(T));
            }
            if (obj.Equals("EditPackageSchema"))
            {
                var obdata = _packages.FindById(id);
                return (T)Convert.ChangeType(new EditPackageSchema()
                {
                    name = obdata.name,
                    Id = obdata.Id.ToString(),
                    zone = obdata.zone,
                    status = obdata.status,
                    remarks = obdata.remarks,
                    amount = obdata.amount,
                    type = obdata.type

                }, typeof(T));
            }
            if (obj.Equals("EditTechnitianSchema"))
            {
                var obdata = _users.FindById(id);
                return (T)Convert.ChangeType(new EditTechnitianSchema()
                {
                    name = obdata.name,
                    Id = obdata.Id.ToString(),
                    zone = obdata.zone,
                    status = obdata.status,
                    remarks = obdata.remarks,
                    type = obdata.type,
                    address = obdata.address,
                    area = obdata.area,
                    email = obdata.email,
                    password = "",
                    phone = obdata.phone,
                    uname = obdata.uname

                }, typeof(T));
            }
            if (obj.Equals("EditCustomerSchema"))
            {
                var obdata = _customers.FindById(id);
                return (T)Convert.ChangeType(new EditCustomerSchema()
                {
                    name = obdata.name,
                    Id = obdata.Id.ToString(),
                    zone = obdata.zone,
                    status = obdata.status,
                    remarks = obdata.remarks,
                    cid = obdata.cid,
                    address = obdata.address,
                    area = obdata.area,
                    email = obdata.email,
                    city = obdata.city,
                    phone = obdata.phone,
                    country = obdata.country,
                    discount = obdata.discount,
                    pincode = obdata.pincode,
                    provider = obdata.provider,
                    state = obdata.state

                }, typeof(T));
            }
            if (obj.Equals("cusSTBSchema"))
            {
                var obdata = _stbs.FindById(id);
                var addonPack = "[]";
                try
                {
                    var ob = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(obdata.addonpack);
                    addonPack = ob.Any(x => x.Trim().Equals("")) ? "[]" : Newtonsoft.Json.JsonConvert.SerializeObject(ob);
                }
                catch
                {
                    addonPack = "[]";
                }
                return (T)Convert.ChangeType(new cusSTBSchema()
                {
                    zone = obdata.zone,
                    type = obdata.type,
                    stbno = obdata.stbno,
                    planname = obdata.planname,
                    Id = obdata.Id.ToString(),
                    addonpack = addonPack,
                    cusID = obdata.cid
                }, typeof(T));
            }
            if (obj.Equals("EmailSchema"))
            {
                var obdata = _emails.AsQueryable().Where(x => x.zone == id).FirstOrDefault()==null?new email() : _emails.AsQueryable().Where(x => x.zone == id).FirstOrDefault();
                return (T)Convert.ChangeType(new EmailSchema()
                {
                    zone = obdata.zone,
                    emailaddress = obdata.emailaddress,
                    name = obdata.name,
                    password = "",
                    smtpport = obdata.smtpport,
                    smtpserver = obdata.smtpserver,
                    Id = obdata.Id.ToString()

                }, typeof(T));
            }

            return (T)Convert.ChangeType(null, typeof(T));
        }
    }
}
