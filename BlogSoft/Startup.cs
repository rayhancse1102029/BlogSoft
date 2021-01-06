using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogSoft
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            #region ERP Database Settings

            services.AddDbContext<FBAIDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("FBAIDbConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<FBAIDbContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region Auth Related Settings
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;



                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);
                options.Lockout.MaxFailedAccessAttempts = 3;
                //options.Lockout.AllowedForNewUsers = true;

                //// User settings.
                //options.User.AllowedUserNameCharacters =
                //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                //options.User.RequireUniqueEmail = false;
            });

            #endregion

            #region PDF DI
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            #endregion

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromHours(24);
                //options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });

            #region Service

            services.AddScoped<IEmployeeCodeService, EmployeeCodeService>();
            services.AddScoped<IUserService, UserService>();

            #endregion

            #region MasterData

            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<ICostingTypeService, CostingTypeService>();
            services.AddScoped<IMonthService, MonthService>();
            services.AddScoped<IQtyMeasurementService, QtyMeasurementService>();
            services.AddScoped<IYearService, YearService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IStoreUserMoboleService, StoreUserMoboleService>();
            services.AddScoped<IDipositeMoneyForService, DipositeMoneyForService>();

            #endregion

            #region Employee Attachment

            services.AddScoped<IEmployeeAttachmentMasterService, EmployeeAttachmentMasterService>();
            services.AddScoped<IEmployeeAttachmentDetailsService, EmployeeAttachmentDetailsService>();

            #endregion

            #region Account

            services.AddScoped<IRegularCostingMasterService, RegularCostingMasterService>();
            services.AddScoped<IRegularCostingDetailsService, RegularCostingDetailsService>();
            services.AddScoped<ISalaryService, SalaryService>();
            services.AddScoped<IRegularCostingReportService, RegularCostingReportService>();
            services.AddScoped<ISalaryReportService, SalaryReportService>();
            services.AddScoped<IBoucherService, BoucherService>();
            services.AddScoped<IFileSaveService, FileSaveService>();

            #endregion

            #region Attachment

            services.AddScoped<IEmployeeCodeService, EmployeeCodeService>();
            services.AddScoped<IEmployeeAttachmentMasterService, EmployeeAttachmentMasterService>();
            services.AddScoped<IEmployeeAttachmentDetailsService, EmployeeAttachmentDetailsService>();

            #endregion

            #region Areas Config
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/areas/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });
            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "controllers",
                    template: "{controller=exists}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                    name: "default",
                    areaName: "ForeignBanglaWebsite",
                    template: "{controller=ForeignBanglaWebsite}/{action=Index}/{id?}");
            });
        }
    }
}
