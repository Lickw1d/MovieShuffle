using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieShuffle.Data;

[assembly: HostingStartup(typeof(MovieShuffle.Areas.Identity.IdentityHostingStartup))]
namespace MovieShuffle.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MovieShuffleContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MovieShuffleContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<MovieShuffleContext>();
            });
        }
    }
}