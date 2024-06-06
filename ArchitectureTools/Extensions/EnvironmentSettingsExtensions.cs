using ArchitectureTools.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ArchitectureTools.Extensions
{
    /// <summary>
    /// Extensões para injeção de dependência das configurações do ambiente (design pattern "Options")
    /// </summary>
    public static class EnvironmentSettingsExtensions
    {
        /// <summary>
        /// Realiza a injeção de dependência do container de configurações de ambiente
        /// </summary>
        /// <param name="services">Interface da coleção de serviços</param>
        /// <param name="environmentVariableNames">Lista de nomes de variáveis de ambiente</param>
        /// <param name="additionalOptions">Função que retorna lista de chave-valor de variável de ambiente</param>
        public static void ConfigureEnvironmentSettings(this IServiceCollection services, 
            List<string>? environmentVariableNames = null,
            Func<List<KeyValuePair<string, string>>>? additionalOptions = null)
        {
            var settings = EnvironmentSettings.Instance;

            if (environmentVariableNames != null)
                settings.SetValueFromEnvironment(environmentVariableNames);

            if (additionalOptions != null)
                settings.SetValue(additionalOptions());

            services.AddSingleton(settings);
        }
    }
}
