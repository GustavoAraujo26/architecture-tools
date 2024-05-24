using System;
using System.Collections.Generic;
using System.Linq;

namespace ArchitectureTools.Models
{
    /// <summary>
    /// Configuração do sistema. Implementa o design pattern "Options"
    /// </summary>
    public sealed class EnvironmentSettings
    {
        private static EnvironmentSettings _instance;
        private List<KeyValuePair<string, string>> _variables;

        private EnvironmentSettings()
        {
            _variables = new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// Instância das configurações do ambiente
        /// </summary>
        public static EnvironmentSettings Instance
        {
            get
            {
                return _instance ?? (_instance = new EnvironmentSettings());
            }
        }

        /// <summary>
        /// Adiciona nova variável na lista
        /// </summary>
        /// <param name="key">Chave da variável</param>
        /// <param name="value">Valor da variável</param>
        public void SetValue(string key, string value) => 
            _variables.Add(new KeyValuePair<string, string>(key, value));

        /// <summary>
        /// Adiciona lista de variáveis
        /// </summary>
        /// <param name="options">Lista de novas variáveis</param>
        public void SetValue(List<KeyValuePair<string, string>> options) => 
            _variables.AddRange(options);

        /// <summary>
        /// Adiciona variável de ambiente na lista
        /// </summary>
        /// <param name="key">Chave da variável de ambienbte</param>
        public void SetValueFromEnvironment(string key) => 
            _variables.Add(new KeyValuePair<string, string>(key, Environment.GetEnvironmentVariable(key)));

        /// <summary>
        /// Adiciona lista de variáveis de ambiente na lista
        /// </summary>
        /// <param name="keys">Lista de chaves de variáveis de ambiente</param>
        public void SetValueFromEnvironment(List<string> keys)
        {
            foreach(var key in keys)
                SetValueFromEnvironment(key);
        }

        /// <summary>
        /// Retorna valor da variável salva
        /// </summary>
        /// <param name="key">Chave</param>
        /// <returns>Valor da variável</returns>
        public string GetValue(string key)
        {
            var kvp = _variables.FirstOrDefault(x => x.Key == key);

            if (kvp.Equals(default(KeyValuePair<string, string>)))
                return null;
            else
                return kvp.Value;
        }
    }
}
