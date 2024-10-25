using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ArchitectureTools.Security
{
    /// <summary>
    /// Gerenciador de criptografia de texto
    /// </summary>
    public class CryptographyFactory
    {
        /// <summary>
        /// Cria um hash de um texto (utilizando SHA256)
        /// </summary>
        /// <param name="text">Texto a ser tratado</param>
        /// <returns>Hash do texto</returns>
        public static byte[] HashValue(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            var sha256 = SHA256.Create();
            return sha256.ComputeHash(textBytes);
        }

        /// <summary>
        /// Criptografa um valor específico (utilizando o AES)
        /// </summary>
        /// <param name="value">Valor a ser criptografado</param>
        /// <param name="privateKey">Chave privada da aplicação</param>
        /// <returns>Valor criptografado</returns>
        public static string EncryptData(string value, string privateKey)
        {
            byte[] encryptedData;

            using (var aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = HashValue(privateKey);
                aes.IV = CreateIVKey(privateKey);

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(value);
                        }
                    }

                    encryptedData = memoryStream.ToArray();
                }

                return Convert.ToBase64String(encryptedData);
            }
        }

        /// <summary>
        /// Descriptografa um valor (utilizando o AES)
        /// </summary>
        /// <param name="value">Valor a ser descriptografado</param>
        /// <param name="key">Chave privada da aplicação</param>
        /// <returns>Valor descriptografado</returns>
        public static string DecryptData(string value, string key)
        {
            var valueBytes = Convert.FromBase64String(value);
            string decryptedData;

            using (var aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = HashValue(key);
                aes.IV = CreateIVKey(key);

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var memorStream = new MemoryStream(valueBytes))
                {
                    using (var cryptoStream = new CryptoStream(memorStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream))
                        {
                            decryptedData = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedData;
        }

        private static byte[] CreateIVKey(string key)
        {
            var keyBytes = HashValue(key);
            var keyBase64 = Convert.ToBase64String(keyBytes);
            var reducedKey = keyBase64.Substring(0, 24);

            byte[] reducedBytes = Convert.FromBase64String(reducedKey);
            byte[] iv = new byte[16];

            Array.Copy(reducedBytes, 0, iv, 0, 16);

            return iv;
        }
    }
}
