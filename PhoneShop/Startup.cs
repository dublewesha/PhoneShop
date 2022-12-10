using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneShop.Models;
using PhoneShop.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            //ƒобавл€ем бд к проекту коннект стринг
            services.AddDbContextPool<PhoneDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyBDConnection")));
            //добавл€ем бд к проекту аналогично выше, но с моделью использовани€ из IdentityUser
            services.AddDbContextPool<AuthDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyBDConnection")));
            //добавл€ем бд к проекту аналогично выше, но с моделью использовани€ из CartApp
            services.AddDbContextPool<CartAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyBDConnection")));
            //
            services.AddDbContextPool<ConfOrdersDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyBDConnection")));
            services.AddDbContextPool<HelpDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyBDConnection")));
            

            //¬недр€ем зависимости баз дынных 
            services.AddScoped<IPhoneRepository, SQLPhoneRepository>();
            services.AddScoped<ICartRepository, SQLCartRepository>();
            services.AddScoped<IConfirmOrdersRepository, SQLConfirmRepository>();

            //«адаем стартовую страницу 
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Shared/Index", "");
            });
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<AuthDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/LoginForm";
            });

            services.AddRazorPages().AddRazorRuntimeCompilation();


            //–еализуем сессии (контроль куки) не используетс€ в проекте
            services.AddSession();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
