namespace ArchitectureTools.Extensions
{
    /// <summary>
    /// Extensões para strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Corta string com base no máximo de caracteres permitidos
        /// </summary>
        /// <param name="value">Valor a ser tratado</param>
        /// <param name="maxLength">Tamanho máximo da string</param>
        /// <returns>String tratada</returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.Length <= maxLength)
                return value;
            else
                return value.Substring(0, maxLength);
        }
    }
}
