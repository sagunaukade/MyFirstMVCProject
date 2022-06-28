using BusinessLayer.Interface;
using CommanLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{

    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserModel Register(UserModel user)
        {
            try
            {
                return this.userRL.Register(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserLoginModel Login(string Email, string Password)
        {
            try
            {
                return this.userRL.Login(Email, Password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
