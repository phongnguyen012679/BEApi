using BEApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BEApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services,
                                                  IConfiguration config,
                                                  bool isProd)
        {
            //if (isProd)
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("SysConnection")));

            // else
            //  services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
        }
    }
}