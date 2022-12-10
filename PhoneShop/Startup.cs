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
            //��������� �� � ������� ������� ������
            services.AddDbContextPool<PhoneDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyBDConnection")));
            //��������� �� � ������� ���������� ����, �� � ������� ������������� �� IdentityUser
            services.AddDbContextPool<AuthDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyBDConnection")));
            //��������� �� � ������� ���������� ����, �� � ������� ������������� �� CartApp
            services.AddDbContextPool<CartAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyBDConnection")));
            //
            services.AddDbContextPool<ConfOrdersDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyBDConnection")));
            services.AddDbContextPool<HelpDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyBDConnection")));
            

            //�������� ����������� ��� ������ 
            services.AddScoped<IPhoneRepository, SQLPhoneRepository>();
            services.AddScoped<ICartRepository, SQLCartRepository>();
            services.AddScoped<IConfirmOrdersRepository, SQLConfirmRepository>();

            //������ ��������� �������� 
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


            //��������� ������ (�������� ����) �� ������������ � �������
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
