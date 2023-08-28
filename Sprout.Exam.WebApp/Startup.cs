using AutoMapper;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Sprout.Exam.Business.Factories;
using Sprout.Exam.Business.Factories.Contracts;
using Sprout.Exam.Business.Mappings;
using Sprout.Exam.Business.Services;
using Sprout.Exam.Business.Services.Contracts;
using Sprout.Exam.Business.Strategies;
using Sprout.Exam.Business.Strategies.Contracts;
using Sprout.Exam.DataAccess.Data;
using Sprout.Exam.DataAccess.Repositories;
using Sprout.Exam.DataAccess.Repositories.Contracts;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Models;

using System;

namespace Sprout.Exam.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DatabaseContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));
            //services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ISalaryCalculationFactory, SalaryCalculationFactory>();
            services.AddSingleton<IContractualSalaryCalculationStrategy, ContractualEmployeeSalaryStrategy>();
            services.AddSingleton<IRegularSalaryCalculationStrategy, RegularEmployeeSalaryStrategy>();


            // Configure AutoMapper
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(),
                               AppDomain.CurrentDomain.GetAssemblies());

            //// Configure AutoMapper using MapperConfiguration
            //var mapperConfig = new MapperConfiguration(cfg => {
            //    cfg.AddProfile<MappingProfile>();
            //    // You can add more profiles or configuration here
            //});

            //// Create an IMapper instance using the configured MapperConfiguration
            //IMapper mapper = mapperConfig.CreateMapper();

            //// Register the IMapper instance with the DI container
            //services.AddSingleton(mapper);

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "customRoute",
                    pattern: "{controller}/{action}/{param1}/{param2}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
