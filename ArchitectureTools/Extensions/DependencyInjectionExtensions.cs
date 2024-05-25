using Microsoft.Extensions.DependencyInjection;
using System;

namespace ArchitectureTools.Extensions
{
    /// <summary>
    /// Extensões para injeção de dependência nativa do .NET
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Obtém um serviço dentro do provedor de serviços
        /// </summary>
        /// <typeparam name="T">Tipo da classe do serviço</typeparam>
        /// <param name="serviceProvider">Provedor de serviço</param>
        /// <returns></returns>
        public static T GetService<T>(this IServiceProvider serviceProvider) where T : class => 
            serviceProvider.GetRequiredService<T>();
    }
}
