using System;
using System.Text.Json.Serialization;

namespace ArchitectureTools.Security
{
    /// <summary>
    /// Senha a ser utilizada para um usuário, por exemplo
    /// </summary>
    public struct Password
    {
        /// <summary>
        /// Inicializa a propriedade
        /// </summary>
        /// <param name="value">Senha a ser utilizada</param>
        [JsonConstructor]
        public Password(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Valor da senha
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Compara se a senha informada é igual
        /// </summary>
        /// <param name="password">Senha informada</param>
        /// <param name="privateKey">Chave privada da aplicação</param>
        /// <returns>Booleano</returns>
        public bool Compare(string password, string privateKey)
        {
            var encryptedPassword = CryptographyFactory.EncryptData(password, privateKey);

            return string.Equals(Value, encryptedPassword, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Descriptografa da senha
        /// </summary>
        /// <param name="privateKey">Chave privada da aplicação</param>
        /// <returns>Senha descriptografada</returns>
        public string GetDecryptedValue(string privateKey) =>
            CryptographyFactory.DecryptData(Value, privateKey);

        /// <summary>
        /// Constrói nova senha criptografada
        /// </summary>
        /// <param name="password">Senha informada</param>
        /// <param name="privateKey">Chave privada</param>
        /// <returns>Senha</returns>
        public static Password Build(string password, string privateKey)
        {
            var encryptedPassword = CryptographyFactory.EncryptData(password, privateKey);

            return new Password(encryptedPassword);
        }
    }
}
