using ArchitectureTools.HttpLibrary;
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

        /// <summary>
        /// Configura a injeção de dependência de uma coleção de chamadas HTTP para API's
        /// </summary>
        /// <param name="services">Interface da coleção de serviços</param>
        /// <param name="apiCollection">Coleção de API's para realizar chamadas HTTP</param>
        public static void ConfigureApiCollection(this IServiceCollection services, ApiHttpCollection apiCollection) =>
            services.AddSingleton(apiCollection);
    }
}
