using CommanLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserModel Register(UserModel user);
        public UserLoginModel Login(string Email, string Password);
    }
}
