using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Infrastructure.Util
{
    public static class JwtTokenHelper
    {
        private static IConfiguration _configuration;
        private static string securityKey => _configuration["Authentication:JwtBearer:SecurityKey"].ToString();
        public static void SetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true);
            _configuration = builder.Build();
        }

        /// <summary>
        /// 创建jwttoken,源码自定义
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static string CreateToken(string name, string role, int expiresMinute = 20, Dictionary<string, object> header = null)
        {
            if (header == null)
            {
                header = new Dictionary<string, object>(new List<KeyValuePair<string, object>>() {
                    new KeyValuePair<string, object>("alg", "HS256"),
                    new KeyValuePair<string, object>("typ", "JWT")
                });
            }
            var now = DateTime.UtcNow;
            Dictionary<string, object> payLoad = new Dictionary<string, object>
            {
                { ClaimTypes.Name,name},
                { ClaimTypes.Role,role},
                { "nbf",ToUnixEpochDate(now.AddMinutes(-5))},
                { "exp",ToUnixEpochDate(now.Add(TimeSpan.FromMinutes(expiresMinute)))},
                { "iss", _configuration["Authentication:JwtBearer:Issuer"] },
                { "aud", _configuration["Authentication:JwtBearer:Audience"] },
            };
            var encodedHeader = Base64UrlEncoder.Encode(JsonConvert.SerializeObject(header));
            var encodedPayload = Base64UrlEncoder.Encode(JsonConvert.SerializeObject(payLoad));

            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(securityKey));
            var encodedSignature = Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(encodedHeader, ".", encodedPayload))));

            return string.Concat(encodedHeader, ".", encodedPayload, ".", encodedSignature);
        }


        /// <param name="expiresMinute">有效分钟</param>
        /// <returns></returns>
        public static string CreateTokenByHandler(string name, string role, int expiresMinute = 20)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,name),
                new Claim(ClaimTypes.Role,role)
            };

            var jwt = new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtBearer:Issuer"],
                audience: _configuration["Authentication:JwtBearer:Audience"],
                claims: claims,
                notBefore: now.AddMinutes(-5),
                expires: now.Add(TimeSpan.FromMinutes(expiresMinute)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey)), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// 验证身份 验证签名的有效性,
        /// </summary>
        /// <param name="encodeJwt"></param>
        /// <param name="validatePayLoad">自定义各类验证； 是否包含那种申明，或者申明的值， </param>
        /// 例如：payLoad["aud"]?.ToString() == "roberAuddience";
        /// 例如：验证是否过期 等
        /// <returns></returns>
        public static bool Validate(string encodeJwt, Func<Dictionary<string, object>, bool> validatePayLoad)
        {
            var success = true;
            var jwtArr = encodeJwt.Split('.');
            var header = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[0]));
            var payLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[1]));

            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(securityKey));
            //首先验证签名是否正确（必须的）
            success = success && string.Equals(jwtArr[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1])))));
            if (!success)
            {
                return success;//签名不正确直接返回
            }
            //其次验证是否在有效期内（也应该必须）
            var now = ToUnixEpochDate(DateTime.UtcNow);
            success = success && (now >= long.Parse(payLoad["nbf"].ToString()) && now < long.Parse(payLoad["exp"].ToString()));

            //再其次 进行自定义的验证
            success = success && validatePayLoad(payLoad);

            return success;
        }
        /// <summary>
        /// 获取jwt中的payLoad
        /// </summary>
        /// <param name="encodeJwt"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetPayLoad(string encodeJwt)
        {
            var jwtArr = encodeJwt.Split('.');
            var payLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[1]));
            return payLoad;
        }
        public static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }


    public class JwtBearSetting
    {
        public bool IsEnabled { get; set; }
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
