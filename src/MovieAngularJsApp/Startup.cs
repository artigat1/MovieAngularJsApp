using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieAngularJsApp.Models;

namespace MovieAngularJsApp
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Register Entity Framework
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<MoviesAppContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            // Add ASP.Net Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MoviesAppContext>()
                .AddDefaultTokenProviders();

            services.Configure<CookieAuthenticationOptions>(o =>
            {
                o.LoginPath = PathString.Empty;
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseIISPlatformHandler();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseIdentity();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateSampleData(app.ApplicationServices).Wait();
        }

        private static async Task CreateSampleData(IServiceProvider applicationServices)
        {
            using (var dbContext = applicationServices.GetService<MoviesAppContext>())
            {
                var sqlServerDatabase = dbContext.Database;
                if (sqlServerDatabase != null)
                {
                    // Create database in user root (c:\users\your name)
                    if (await sqlServerDatabase.EnsureCreatedAsync())
                    {
                        // add some movies
                        var movies = new List<Movie>
                            {
                                new Movie {Title="Star Wars: The Force Awakens", Director="J.J. Abrams", ReleaseDate=new DateTime(2015, 12, 17)},
                                new Movie {Title="The Hateful Eight", Director="Quentin Tarantino", ReleaseDate=new DateTime(2016, 01, 08)},
                                new Movie {Title="The Revenant", Director="Alejandro González Iñárritu", ReleaseDate=new DateTime(2016, 01, 15)},
                            };
                        movies.ForEach(m => dbContext.Movies.Add(m));

                        // add some users
                        var userManager = applicationServices.GetService<UserManager<ApplicationUser>>();

                        // add editor user
                        var stephen = new ApplicationUser
                        {
                            UserName = "Stephen"
                        };
                        var result = await userManager.CreateAsync(stephen, "P@ssw0rd");
                        await userManager.AddClaimAsync(stephen, new Claim("CanEdit", "true"));

                        // add normal user
                        var bob = new ApplicationUser
                        {
                            UserName = "Bob"
                        };
                        await userManager.CreateAsync(bob, "P@ssw0rd");
                    }

                }
            }
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
