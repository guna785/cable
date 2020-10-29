using BL.security;
using BL.service;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cable.Service
{
    public interface IAuthenTicationService
    {
        RegUser AuthenticateUser(string uname, string password);
    }
    public class AuthenTicationService : IAuthenTicationService
    {
        private readonly IGenericBL<admin> _bLservice;
        private readonly IGenericBL<user> _bLusrservice;
        private readonly IGenericBL<userlogic> _logicService;
        public AuthenTicationService(IGenericBL<admin> bLservice, IGenericBL<userlogic> logicService, IGenericBL<user> bLusrservice)
        {
            _bLservice = bLservice;
            _bLusrservice = bLusrservice;
            _logicService = logicService;
        }
        public RegUser AuthenticateUser(string uname, string password)
        {
            RegUser regUser = new RegUser();
            var usr = _bLservice.FilterBy(x => x.uname == uname).FirstOrDefault();
            if (usr != null)
            {


                var sal = _logicService.FilterBy(x => x.uid == usr.Id).FirstOrDefault();
                var pass = HasherService.HashPasswordWithSalt(Encoding.UTF8.GetBytes(password), sal.usrToken);
                if (usr.password == Convert.ToBase64String(pass))
                //if (usr.password == password)
                {
                    regUser.uname = usr.uname;
                    regUser.Name = usr.name;
                    regUser.role = usr.type;
                    regUser.status = true;
                    return regUser;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

    }
}
