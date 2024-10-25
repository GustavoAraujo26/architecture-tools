using System.ComponentModel;

namespace ArchitectureTools.Enums
{
    /// <summary>
    /// Regra de integridade de senha
    /// </summary>
    public enum PasswordStrengthRule
    {
        /// <summary>
        /// Tamanho mínimo
        /// </summary>
        [Description("Has minimum length")]
        MinimumLength = 1,
        /// <summary>
        /// Tamanho máximo
        /// </summary>
        [Description("Has maximum length")]
        MaximumLength = 2,
        /// <summary>
        /// Possui números
        /// </summary>
        [Description("Has numbers")]
        Numbers = 3,
        /// <summary>
        /// Possui letras maiusculas
        /// </summary>
        [Description("Has upper case letters")]
        UppercaseLetters = 4,
        /// <summary>
        /// Possui letras minusculas
        /// </summary>
        [Description("Has lower case letters")]
        LowercaseLetters = 5,
        /// <summary>
        /// Possui caracteres especiais
        /// </summary>
        [Description("Has special characters")]
        SpecialCharacters = 6
    }
}
