using Demo.Busiess.Interfaces;
using Demo.Busiess.Repositories;
using Demo.DataAccess.Contexts;
using Demo.DataAccess.Models;
using Demo.Presentaion.Mappers;
using Demo.Presentaion.Services.Classes;
using Demo.Presentaion.Services.interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Demo
{
    public class Program
    {
        public static object env { get; private set; }

        public static void Main(string[] args)
        {

            #region Configure Services => allow dependacy injections
            var Builder = WebApplication.CreateBuilder(args);
            Builder.Services.AddControllersWithViews();
            Builder.Services.AddDbContext<MainContext>(options =>
            { options.UseSqlServer(Builder.Configuration.GetConnectionString(name: "DefaultConnection")); }
            );
            Builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            Builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            Builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            Builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<MainContext>().AddDefaultTokenProviders();
            Builder.Services.ConfigureApplicationCookie(conf =>
            {
                conf.LoginPath = "/Account/SignIn";
                conf.AccessDeniedPath = "/Account/AccessDenied";
            });


            Builder.Services.AddAutoMapper(m => m.AddProfile(new MainMapper()));



            Builder.Services.AddTransient<ITranseantServices, ITransiantServies>(); 
            Builder.Services.AddScoped<IScopedServices, ScopedServices>();
            Builder.Services.AddSingleton<ISingletonServices, SingletonServices>();  
            #endregion
            var app = Builder.Build();


            #region Http Requests => allow Pipline  or Midelwares
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
               
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            }
            );
            #endregion

            app.Run();
        }

    }
}
