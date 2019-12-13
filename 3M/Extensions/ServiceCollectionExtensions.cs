using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace _3M.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories<TRepository>(this IServiceCollection services, Assembly assembly)
        {
            assembly.GetTypes()
                .Where(i => !i.IsInterface && !i.IsAbstract && typeof(TRepository).IsAssignableFrom(i))
                .ToList()
                .ForEach(repo => services.AddTransient(repo));
        }
    }
}
