using Microsoft.EntityFrameworkCore;
using TerryPratchettSite.Data;
using TerryPratchettSite.Data.Repos;
using TerryPratchettSite.Interfaces;
using TerryPratchettSite.Services;

namespace TerryPratchettSite.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
     IConfiguration config)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors();
            services.AddScoped<UserRepo>();
            services.AddScoped<IReviewService, ReviewService>();
            return services;
        }
    }
}
