// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();
// app.MapGet("/", () => "Hello World!");
// app.Run();

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AServer {
    public class Program {

        public static void Main(string[] args) {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())  // 设置当前目录的内容
            .UseIISIntegration()
            // .UseUrls("http:// *:5000") // 使 项目在 5000端口被访问: 【这里改成跟其它一样，127.0.0.1:10002】
            .UseUrls("http://127.0.0.1:8080") // 使 项目在 5000端口被访问。暂时先写成这样，测一下再作必要的修改【爱表哥，爱生活！！！】
            .UseStartup<Startup>()
            .Build();

        
    }
}