using Application.Behaviours;
using Application.DomainServices;
using Application.DomainServices.AuthorServices;
using Application.DomainServices.UserServices;
using Domain.EntityServices;
using Domain.EntityServices.AuthorServices;
using Domain.EntityServices.UserServices;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;

namespace WEB
{
    public class Startup
    {
        public Startup(IConfiguration conf) {
            configuration=conf;
        }

        private IConfiguration configuration { get; set; }


        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews()
                    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            services.AddDbContext<ApplicationDbContext>(opts => {
                opts.UseNpgsql(configuration["ConnectionStrings:DatabaseConnection"]);
            });
            services.AddDistributedMemoryCache();
            services.AddRazorPages();
            services.AddMemoryCache();
            services.AddSession();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opts =>
                {
                    opts.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    opts.SlidingExpiration = true;
                    opts.LoginPath = "/user/login";
                    opts.AccessDeniedPath = "/user/login";
                    opts.Cookie.HttpOnly = true;
                });
            services.AddAuthorization();
            services.AddHsts(opt => {
                opt.MaxAge = TimeSpan.FromDays(60);
            });
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly,
                includeInternalTypes: true);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped<IEmailUniqueCheck, EmailUniqueCheck>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IAddClaimIdentity, AddClaimIdentity>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IProfileMaterialRepository, ProfileMaterialRepository>();
            services.AddScoped<IAddClaimIdentity, AddClaimIdentity>();
            services.AddScoped<ISaveProfileMaterial, SaveProfileMaterial>();
            services.AddScoped<IUrlPageUniqueCheck, UrlPageUniqueCheck>();
        }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=NewsFeed}/{action=Index}");
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapFallback(async context => {
                    await context.Response.WriteAsync("<h1>Not Found</h1>");
                });
            });
            new SeedData().fill(app);
        }
    }
}
