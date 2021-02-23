using System;
using caintDashboard.Areas.Identity.Data;
using caint.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(caintDashboard.Areas.Identity.IdentityHostingStartup))]
namespace caintDashboard.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<caintDashboardIdentityDbContext>(options => options.UseSqlite(context.Configuration.GetConnectionString("caintDashboardIdentityDbContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<caintDashboardIdentityDbContext>();
            });
        }
    }
}