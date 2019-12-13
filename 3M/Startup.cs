using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3M.DataModels;
using _3M.DataModels.MapperProfiles;
using _3M.DbContexts;
using _3M.Extensions;
using _3M.Helpers;
using _3M.Repositories;
using _3M.Repositories.Base;
using _3M.Services;
using _3M.ViewModels;
using _3M.ViewModels.Validators;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;


namespace _3M
{
    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new DefaultProfile());
            },this.GetType().Assembly);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddRepositories<IRepository>(typeof(CategoryRepository).Assembly);



            services.AddMvc()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<ChangePasswordViewModelValidator>();
                }).AddSessionStateTempDataProvider()
                .AddRazorRuntimeCompilation();

            services.AddIdentity<AppUser, IdentityRole<Guid>>(options=>
            {

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric= false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 3;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail= false;
                options. SignIn.RequireConfirmedPhoneNumber= false;
            })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(Options =>
           {
               Options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
           }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
           {
               options.LoginPath = "/Account/Login";
               options.LogoutPath = "/Account/Logout";
               options.AccessDeniedPath = "/Account/AccessDenied";
               options.SlidingExpiration = true;
               options.ReturnUrlParameter = "ReturnUrl";
           }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
           {
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidIssuer = "http://localhost",
                   ValidAudience = "http://lacalhost",
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   RoleClaimType = "Roles",
                   NameClaimType = "UserName",
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes("My PAssword"))
               };
           });

            services.AddAuthorization();
            services.AddSession();

            services.AddTransient<PagingHelper>();
            services.AddTransient<NotificationManager>();
            services.AddTransient<CartManager>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.MigrateDatabase<AuthDbContext>();
            app.MigrateDatabase<AppDbContext>();
            DataSeeder.SeedRoles(app);
            DataSeeder.SeedUsers(app);

            IApplicationBuilder applicationBuilder = app.UseRouting();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default",
                    "{controller=Home}/{action-Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }

        
            
        }
    }

