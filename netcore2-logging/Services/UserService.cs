using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using netcore2_logging.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace netcore2_logging.Services
{
    public interface IUserService
    {
        UserModel Authenticate(LoginModel loginModel);
    }

    public class UserService : IUserService
    {
        /// <summary>
        /// DB連線
        /// </summary>
        private MySqlConnection _connection;

        private readonly JWTSettings _JWTSettings;

        public UserService(IOptions<JWTSettings> appSettings,IConnectionFactory connectionFactory)
        {
            _JWTSettings = appSettings.Value;
            _connection = (MySqlConnection)connectionFactory.CreateConnection();
        }

        ~UserService()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }

        public UserModel Authenticate(LoginModel loginModel)
        {
            //執行登入動作
            var sqlStr = $"SELECT * FROM users where Username=@username AND Password=@password";
            UserModel user = _connection.QueryFirstOrDefault<UserModel>(sqlStr,
                new {
                    username = loginModel.Username,
                    password = loginModel.Password
                });

            // return null if user not found
            if (user == null)
                return null;

            // 授權成功時，產生JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_JWTSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //儲存相關Claim以便日後調用
                //IMPORTANT: 放在這裡的資訊會被公開，避免放置敏感訊息
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.DateOfBirth,user.Birthdate.ToString("yyyy-MM-dd"))
                }),
                //有效期限
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                //簽署者
                Issuer = _JWTSettings.Issuer,
                //接收方
                Audience = _JWTSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // 返回使用者資訊時，隱藏密碼
            user.Password = null;

            return user;
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Birthdate { set; get; }
        public string Token { get; set; }
    }
}
