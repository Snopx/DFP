using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Web.Host
{
    /// <summary>
    /// Program to setup
    /// </summary>
    public class Program
    {
        /// <summary>
        /// program entry
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CurrentDirectoryHelpers.SetCurrentDirectory();
            CreateWebHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// IWebHostBuilder builder method
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
