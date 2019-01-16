using System;
using System.Collections.Generic;
using Infrastructure.Util;

namespace ConsoleApp1
{
    class Program
    {
        //
        static void Main(string[] args)
        {
            //var key = Guid.NewGuid().ToString("n");
            //Console.WriteLine(key);
            ////var key = "2db86356-4590-4909-2a09-9788d7768249";
            //var name = "darrenFang";
            //Console.WriteLine($"加密字符串为:{name}");
            //var encryptStr = SecurityOfCrypt.Encode(name, key);
            //Console.WriteLine($"加密后结果为：{encryptStr}");
            //var decryptStr = SecurityOfCrypt.Decode(encryptStr, key);
            //Console.WriteLine($"解密后字符串为{decryptStr}");

            Dictionary<string, object> payLoad = new Dictionary<string, object>();
            payLoad.Add("sub", "rober");
            payLoad.Add("jti", "09e572c7-62d0-4198-9cce-0915d7493806");
            payLoad.Add("nbf", null);
            payLoad.Add("exp", null);
            payLoad.Add("iss", "roberIssuer");
            payLoad.Add("aud", "roberAudience");
            payLoad.Add("age", 30);
            var encodeJwt = JwtTokenHelper.CreateToken(payLoad, 30);
            var encodeJwt2 = JwtTokenHelper.CreateTokenByHandler(payLoad, 30);

            var t =  JwtTokenHelper.Validate(encodeJwt, (load) => { return true; });
            var t2 = JwtTokenHelper.Validate(encodeJwt, (load) => { return true; });

            Console.WriteLine(t);
            Console.WriteLine(t2);

            Console.WriteLine(encodeJwt);
            Console.WriteLine(encodeJwt2);
            //Console.WriteLine(encodeJwt2);
            //var result = JwtTokenHelper.Validate(encodeJwt, (load) => { return true; });
        }


    }
}
