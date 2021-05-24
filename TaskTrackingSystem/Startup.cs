using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskTrackingSystem.DAL.Models;
using AutoMapper;
using TaskTrackingSystem.BLL;
using TaskTrackingSystem.DAL.Interfaces;
using TaskTrackingSystem.DAL.Repositories;
using TaskTrackingSystem.BLL.Services;
using TaskTrackingSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TaskTrackingSystem.BLL.Security;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace TaskTrackingSystem
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
            services.AddControllersWithViews();

            services.AddDbContext<DatabaseContext>(config =>
                config.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // In production, the Angular files will be served from this directory
            services.AddSwaggerGen();

            services.AddScoped<DbContext, DatabaseContext>();

            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.SignIn.RequireConfirmedAccount = false;
                opt.SignIn.RequireConfirmedEmail = false;
                opt.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddSignInManager<SignInManager<User>>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddUserManager<UserManager<User>>()
                .AddRoleStore<RoleStore<IdentityRole>>()
                .AddUserStore<UserStore<User>>();


            var mapperConfig = new MapperConfiguration(profile =>
            {
                profile.AddProfile(new AutomapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            // DAL
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //BLL
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(Configuration.GetSection("AppSettings:Token").Value));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });

            services.AddAuthorization(opt =>
            {
                opt.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            });

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddAuthorization();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            IdentityModelEventSource.ShowPII = false;

            app.UseSwagger();

            app.UseSwaggerUI(sw =>
            {
                sw.SwaggerEndpoint("/swagger/v1/swagger.json", "site");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseCors();
            
            app.UseAuthentication();
            app.UseAuthorization(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
