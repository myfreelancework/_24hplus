using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _24hplusdotnetcore.Common;
using _24hplusdotnetcore.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace _24hplusdotnetcore.Services
{
    public class AuthServices
    {
        private readonly IMongoCollection<User> _user;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IConfiguration _config;
        public AuthServices(IMongoDbConnection connection, IDataProtectionProvider dataProtectionProvider, IConfiguration config)
        {
            var client = new MongoClient(connection.ConnectionString);
            var database = client.GetDatabase(connection.DataBase);
            _user = database.GetCollection<User>(MongoCollection.UsersCollection);
            _dataProtectionProvider = dataProtectionProvider;
            _config = config;
        }

        public string Login(User user)
        {
            var loggedUser = AuthenticateUser(user);
            string token = string.Empty;
            if (loggedUser != null)
            {
                token = GenerateJSONWebToken(loggedUser);
            }
            return token;

        }
        private User AuthenticateUser(User user)
        {
            var loggedUser = new User();
            CipherServices cipher = new CipherServices(_dataProtectionProvider);
            loggedUser = null;
            try
            {
                var loggedProcessUser = _user.Find(u => u.UserEmail == user.UserEmail).FirstOrDefault();
                if (loggedProcessUser != null)
                {
                    string descriptPassword = cipher.Decrypt(loggedProcessUser.UserPassword);
                    if (user.UserPassword == descriptPassword)
                    {
                        loggedUser = loggedProcessUser;
                    }

                }
            }
            catch (System.Exception ex)
            {
                
                throw;
                
            }
            return loggedUser;
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userInfo.UserEmail),
                new Claim(JwtRegisteredClaimNames.GivenName, userInfo.UserFirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.UserLastName),
                new Claim("Role", userInfo.RoleId.ToString()),
                new Claim(ClaimTypes.Role, userInfo.RoleId.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}