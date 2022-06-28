using CommanLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{

    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        public UserModel Register(UserModel user)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Assignment"]);
                SqlCommand com = new SqlCommand("UserRegister", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", user.Email);
                com.Parameters.AddWithValue("@FirstName", user.FirstName);
                com.Parameters.AddWithValue("@LastName", user.LastName);
                com.Parameters.AddWithValue("@DOB", user.DOB);
                com.Parameters.AddWithValue("@Gender", user.Gender);
                com.Parameters.AddWithValue("@Education", user.Education);
                com.Parameters.AddWithValue("@Address", user.Address);
                com.Parameters.AddWithValue("@Password", user.Password);
                com.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword);
                this.sqlConnection.Open();
                int i = com.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public UserLoginModel Login(string Email, string Password)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Assignment"]);
                SqlCommand com = new SqlCommand("UserLogin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", Email);
                com.Parameters.AddWithValue("@Password", Password);
                this.sqlConnection.Open();
                SqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    int UserId = 0;
                    UserLoginModel user = new UserLoginModel();
                    while (rd.Read())
                    {
                        user.Email = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                        user.Password = Convert.ToString(rd["Password"] == DBNull.Value ? default : rd["Password"]);
                        UserId = Convert.ToInt32(rd["UserId"] == DBNull.Value ? default : rd["UserId"]);


                    }

                    this.sqlConnection.Close();
                    user.Token = this.GenerateJWTToken(Email, UserId);
                    return user;
                }
                else
                {
                    this.sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public string GenerateJWTToken(string Email, int userId)
        {
            // header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // payload
            var claims = new[]
            {
                //new Claim(ClaimTypes.Role, "User"),
                new Claim("Email", Email),
                new Claim("Id", userId.ToString()),
            };

            // signature
            var token = new JwtSecurityToken(
            this.Configuration["Jwt:Issuer"],
            this.Configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

       
