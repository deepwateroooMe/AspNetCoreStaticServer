using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace AServer {
    public class Startup {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https:// go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            // 在 ConfigureServices 函数中增加 目录访问服务
            services.AddDirectoryBrowser(); // 使目录可以被浏览 （浏览所有的文件以及文件夹）
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env) {

            // 设置 目录可浏览  start
            var dir = new DirectoryBrowserOptions();
            dir.FileProvider = new PhysicalFileProvider(@"F:\AServer\wwwroot\");
            app.UseDirectoryBrowser(dir);

            
            // // 这是一个基本的静态文件服务器， app.UseStaticFiles() 函数使当前内容目录下默认的 wwwroot中的文件可以被访问【 wwwroot 】
            // app.UseStaticFiles(); // 使用默认文件夹 wwwroot 仅仅shi wwwroot对外可见
            // 如果设置为指定目录的文件，可以被访问 start
            var staticfile = new StaticFileOptions();
            staticfile.FileProvider = new PhysicalFileProvider(@"F:\AServer\wwwroot\"); // 指定目录，这里指的是F盘，也可以指定其他目录

            // c盘中有个 叫 456.log 的文件，我们访问不了，原因是：服务器不能识别，怎么办？如何让服务器识别 所有类型的文件呢？
            // 设置 对应的文件类型（防止Mime type没事别出来，打不开或出现404错误）
            staticfile.ServeUnknownFileTypes = true;
            staticfile.DefaultContentType = "application/x-msdownload";// 设置默认 MIME TYPE
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings.Add(".log", "text/plain"); // 手动设置对应的 MIME TYPE
            staticfile.ContentTypeProvider = provider;


            app.UseStaticFiles(staticfile); // 使用默认文件夹 wwwroot 仅仅是 wwwroot对外可见
            
            app.Run(async (context) =>  {
                await context.Response.WriteAsync("Hello, you are downloading this specific file.");
            });
        }
    }
}