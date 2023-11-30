//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(3);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});
//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    options.CheckConsentNeeded = context => true;
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//});
//builder.Services.ConfigureApplicationCookie(o => {
//    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
//    o.SlidingExpiration = true;
//});
//// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();
//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
//app.UseSession();

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseCookiePolicy();
//app.UseAuthentication();
//app.UseRouting();
//app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");


//});

//app.Run();
using Microsoft.Extensions.FileProviders;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Rewrite;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zencareservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);
            //var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            //app.Run();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Shift")),
            //    RequestPath = "/Shift"
            //});

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Login}/{id?}");


            //app.Map("/Shift", webFormsApp =>
            //{
            //    webFormsApp.Run(async context =>
            //    {
            //        // Construct the URL to the Web Forms page
            //        string webFormsPageUrl = "/Shift/WebForm1.aspx"; // Adjust the path as needed

            //        // Redirect to the Web Forms page
            //        context.Response.Redirect(webFormsPageUrl);
            //    });
            //});

            //app.Use(async (context, next) =>
            //{
            //    var requestPath = context.Request.Path.Value;

            //    if (requestPath.EndsWith(".aspx"))
            //    {
            //        // Handle .aspx requests as needed or simply return a 404 response
            //        context.Response.StatusCode = 404;
            //        return;
            //    }

            //    await next();
            //});



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=ForgotPassword}/{id?}");

                //endpoints.MapControllerRoute(
                //    name: "subProject1",
                //    pattern: "IMEmployee_Prj/{controller=Home}/{action=Index}/{id?}",
                //    defaults: new { area = "IMEmployee_Prj", controller = "Home" });

                //endpoints.MapRazorPages();
                //endpoints.MapBlazorHub();
                //endpoints.MapFallbackToPage("/_Host");

            });

            //app.Map("/WebForm1.aspx", webFormsApp =>
            //{
            //    webFormsApp.Run(async context =>
            //    {
            //        // Specify the path to your ASPX file
            //        var aspxFilePath = "./WebForm1.aspx"; // Update with the actual path

            //        // Read the content of the ASPX file
            //        string aspxContent = File.ReadAllText(aspxFilePath);

            //        // Set the content type for the response
            //        context.Response.ContentType = "text/html";

            //        // Write the ASPX content to the response
            //        await context.Response.WriteAsync(aspxContent);
            //    });
            //});



            //var options = new RewriteOptions()
            //    .AddRewrite("(.+).aspx", "./WebForm1.aspx", skipRemainingRules: true);

            //app.UseRewriter(options);

            app.Run();

        }



    }
}