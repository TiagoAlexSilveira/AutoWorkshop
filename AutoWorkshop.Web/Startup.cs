using AutoWorkshop.Web.Data;
using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoWorkshop.Web
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
            services.AddIdentity<User, IdentityRole>(cfg =>
             {
                 cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                 cfg.SignIn.RequireConfirmedEmail = true;
                 cfg.User.RequireUniqueEmail = true;
                 cfg.Password.RequireDigit = false;
                 cfg.Password.RequiredUniqueChars = 0;
                 cfg.Password.RequireLowercase = false;
                 cfg.Password.RequireNonAlphanumeric = false;
                 cfg.Password.RequireUppercase = false;
                 cfg.Password.RequiredLength = 6;
             })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<DataContext>();

            //services.AddAuthentication()  era para api
            //    .AddCookie()
            //    .AddJwtBearer(cfg =>
            //    {
            //        cfg.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidIssuer = Configuration["Tokens:Issuer"],
            //            ValidAudience = Configuration["Toekns:Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(
            //                Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
            //        };
            //    });

            services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddTransient<SeedDb>();

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IConverterHelper, ConverterHelper>();
            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IMechanicRepository, MechanicRepository>();
            services.AddScoped<ISecretaryRepository, SecretaryRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAppointmentTypeRepository, AppointmentTypeRepository>();
            services.AddScoped<IRepairRepository, RepairRepository>();
            services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();




            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/NotAuthorized";
                options.AccessDeniedPath = "/Account/NotAuthorized";
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzA5MDExQDMxMzgyZTMyMmUzME9JVklRSWE0Z3FtUHpQOUZ0SkxQQTdqQWd5cnVaQzdpU1A3WERnaktkN0U9");

            //
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}"); //adicionar por causa dos erros

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
