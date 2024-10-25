using ArchitectureTools.Enums;
using System.Collections.Generic;

namespace ArchitectureTools.Security
{
    /// <summary>
    /// Resposta da validação de integridade da senha
    /// </summary>
    public class PasswordStrengthResponse
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public PasswordStrengthResponse() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="valid">Senha válida?</param>
        /// <param name="rules">Regras infringidas</param>
        /// <param name="score">Pontuação da integridade da senha</param>
        public PasswordStrengthResponse(bool valid, 
            Dictionary<PasswordStrengthRule, string> rules, PasswordStrengthScore score)
        {
            Valid = valid;
            Rules = rules;
        }

        /// <summary>
        /// Senha válida?
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// Regras infringidas
        /// </summary>
        public Dictionary<PasswordStrengthRule, string> Rules { get; set; }

        /// <summary>
        /// Pontuação da integridade da senha
        /// </summary>
        public PasswordStrengthScore Score { get; set; }

        /// <summary>
        /// Constrói resposta válida
        /// </summary>
        /// <returns>Resposta</returns>
        public static PasswordStrengthResponse BuildValid() => 
            new PasswordStrengthResponse(true, new Dictionary<PasswordStrengthRule, string>(), 
                PasswordStrengthScore.Strong);

        /// <summary>
        /// Constrói resposta inválida
        /// </summary>
        /// <param name="rules">Dicionário de regras infringidas</param>
        /// <param name="score">Pontuação da integridade da senha</param>
        /// <returns>Resposta</returns>
        public static PasswordStrengthResponse BuildInvalid(Dictionary<PasswordStrengthRule, string> rules,
            PasswordStrengthScore score) => 
            new PasswordStrengthResponse(false, rules, score);
    }
}
