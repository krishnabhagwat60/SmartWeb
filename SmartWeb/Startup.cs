using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartWeb.Data;
using SmartWeb.Models;
using SmartWeb.Repository;
using SmartWeb.Repository.IRepository;
using System;
using SmartWeb.Hubs;
using DinkToPdf.Contracts;
using DinkToPdf;
using tusdotnet.Models;
using System.IO;
using tusdotnet.Stores;
using tusdotnet.Models.Expiration;
using tusdotnet.Models.Configuration;
using System.Text;
using tusdotnet;
using System.Threading.Tasks;

namespace SmartWeb
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
            services.AddDbContext<SmartTeacherDBContext>(options =>
                   options.UseSqlServer(
                   Configuration.GetConnectionString("SmartTeacherConnection")));

            services.AddIdentity<TblApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SmartTeacherDBContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 4;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;

                opt.SignIn.RequireConfirmedEmail = true;

                // Lockout settings.
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.AllowedForNewUsers = true;

                // User settings.
                opt.User.AllowedUserNameCharacters = string.Empty;
                opt.User.RequireUniqueEmail = false;
            });

            services.AddDistributedMemoryCache();
            
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(400);

                options.LoginPath = $"/Home/Login";
                options.AccessDeniedPath = $"/Home/Authorized";
                options.SlidingExpiration = true;
            });
       
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(30);
                //options.Cookie.HttpOnly = true;
                //options.Cookie.IsEssential = true;
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //////////var context = new CustomAssemblyLoadContext();
            //////////context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddControllersWithViews();
            services.AddSingleton(CreateTusConfiguration);
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "MyArea",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chathub");
            });
            app.UseTus(httpContext => Task.FromResult(httpContext.RequestServices.GetService<DefaultTusConfiguration>()));
        }
        private DefaultTusConfiguration CreateTusConfiguration(IServiceProvider serviceProvider)
        {
            var env = (IWebHostEnvironment)serviceProvider.GetRequiredService(typeof(IWebHostEnvironment));

            //File upload path
            var tusFiles = Path.Combine(env.WebRootPath, "PAlert");

            return new DefaultTusConfiguration
            {
                UrlPath = "/files",
                //File storage path
                Store = new TusDiskStore(tusFiles),
                //Does metadata allow null values
                MetadataParsingStrategy = MetadataParsingStrategy.AllowEmptyValues,
                //The file will not be updated after expiration
                Expiration = new AbsoluteExpiration(TimeSpan.FromMinutes(5)),
                //Event handling (various events, meet your needs)
                Events = new Events
                {
                    //Upload completion event callback
                    OnFileCompleteAsync = async ctx =>
                    {
                        //Get upload file
                        var file = await ctx.GetFileAsync();

                        //Get upload file=
                        var metadatas = await file.GetMetadataAsync(ctx.CancellationToken);

                        //Get the target file name in the above file metadata
                        var fileNameMetadata = metadatas["name"];

                        //The target file name is encoded in Base64, so it needs to be decoded here
                        var fileName = fileNameMetadata.GetString(Encoding.UTF8);

                        var extensionName = Path.GetExtension(fileName);

                        //Convert the uploaded file to the actual target file
                        File.Move(Path.Combine(tusFiles, ctx.FileId), Path.Combine(tusFiles, $"{ctx.FileId}{extensionName}"));
                    }
                }
            };
        }

    }
}
