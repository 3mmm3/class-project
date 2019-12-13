using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void MigrateDatabase<TContext>(this IApplicationBuilder app) where TContext : DbContext
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<TContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
