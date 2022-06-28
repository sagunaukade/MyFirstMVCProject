using CommanLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserModel Register(UserModel user);
        public UserLoginModel Login(string Email, string Password);
    }
}
