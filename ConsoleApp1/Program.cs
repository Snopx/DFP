using System;
using Infrastructure.Util;

namespace ConsoleApp1
{
    class Program
    {
        //
        static void Main(string[] args)
        {
            var key = Guid.NewGuid().ToString("n");
            Console.WriteLine(key);
            //var key = "2db86356-4590-4909-2a09-9788d7768249";
            var name = "darrenFang";
            Console.WriteLine($"加密字符串为:{name}");
            var encryptStr = SecurityOfCrypt.Encode(name, key);
            Console.WriteLine($"加密后结果为：{encryptStr}");
            var decryptStr = SecurityOfCrypt.Decode(encryptStr, key);
            Console.WriteLine($"解密后字符串为{decryptStr}");
        }
    }
}
