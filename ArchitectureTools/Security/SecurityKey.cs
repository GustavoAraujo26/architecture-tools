using System;
using System.Text.Json.Serialization;

namespace ArchitectureTools.Security
{
    /// <summary>
    /// Chave de segurança (podendo ser utilizado em autenticações de multiplos fatores)
    /// </summary>
    public struct SecurityKey
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="value"></param>
        [JsonConstructor]
        public SecurityKey(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Valor
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Verifica se a chave está correta
        /// </summary>
        /// <param name="key">Chave a ser comparada</param>
        /// <returns></returns>
        public bool Compare(string key) => 
            Value.Equals(key);

        /// <summary>
        /// Altera a chave, gerando uma nova
        /// </summary>
        /// <returns>Nova chave</returns>
        public SecurityKey Change() =>
            SecurityKey.BuildNew();

        /// <summary>
        /// Constrói nova chave
        /// </summary>
        /// <returns>Nova chave</returns>
        public static SecurityKey BuildNew() =>
            new SecurityKey(Generate());

        private static string Generate()
        {
            var random = new Random();
            return random.Next(1, 999999).ToString("D6");
        }
    }
}
