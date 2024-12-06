using ArchitectureTools.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ArchitectureTools.Security
{
    /// <summary>
    /// Regras para senhas
    /// </summary>
    public class PasswordRules
    {
        /// <summary>
        /// Construtor com lista de regras vazias
        /// </summary>
        public PasswordRules()
        {
            Rules = new Dictionary<PasswordStrengthRule, string>();
        }

        /// <summary>
        /// Construtor para inicializar as regras
        /// </summary>
        /// <param name="rules"></param>
        [JsonConstructor]
        public PasswordRules(Dictionary<PasswordStrengthRule, string> rules)
        {
            Rules = rules;
        }

        /// <summary>
        /// Lista de regras
        /// </summary>
        [JsonInclude]
        public Dictionary<PasswordStrengthRule, string> Rules { get; private set; }

        #region [Métodos de manipulação]
        /// <summary>
        /// Altera uma regra
        /// </summary>
        /// <param name="rule">Regra</param>
        /// <param name="value">Valor (parâmetro numeral) da regra</param>
        public void Change(PasswordStrengthRule rule, int value)
        {
            if (rule != PasswordStrengthRule.MinimumLength &&
                rule != PasswordStrengthRule.MaximumLength)
                return;

            var valueString = value.ToString();

            if (Rules.ContainsKey(rule))
            {
                Rules[rule] = valueString;
                return;
            }

            Rules.Add(rule, valueString);
        }

        /// <summary>
        /// Altera uma regra
        /// </summary>
        /// <param name="rule">Regra</param>
        /// <param name="value">Valor (parâmetro booleano) da regra</param>
        public void Change(PasswordStrengthRule rule, bool value)
        {
            if (rule == PasswordStrengthRule.MinimumLength ||
                rule == PasswordStrengthRule.MaximumLength)
                return;

            var valueString = value.ToString();

            if (Rules.ContainsKey(rule))
            {
                Rules[rule] = valueString;
                return;
            }

            Rules.Add(rule, valueString);
        }

        /// <summary>
        /// Remove regra
        /// </summary>
        /// <param name="rule">Regra</param>
        public void Remove(PasswordStrengthRule rule)
        {
            if (!Rules.ContainsKey(rule))
                return;

            Rules.Remove(rule);
        }

        /// <summary>
        /// Obtem valor da regra para tamanho da senha
        /// </summary>
        /// <param name="rule">Regra</param>
        /// <returns>Tamanho da regra</returns>
        public int GetLengthValue(PasswordStrengthRule rule)
        {
            if (rule != PasswordStrengthRule.MinimumLength &&
                rule != PasswordStrengthRule.MaximumLength)
                return 0;

            int value;
            int.TryParse(Rules[rule], out value);

            return value;
        }

        /// <summary>
        /// Obtem valor da regra para booleano
        /// </summary>
        /// <param name="rule">Regra</param>
        /// <returns>Valor booleano</returns>
        public bool GetBooleanValue(PasswordStrengthRule rule)
        {
            if (rule == PasswordStrengthRule.MinimumLength ||
                rule == PasswordStrengthRule.MaximumLength)
                return false;

            bool value;
            bool.TryParse(Rules[rule], out value);

            return value;
        }
        #endregion

        #region [Métodos de validação]
        /// <summary>
        /// Valida a senha informada com base nas regras estabelecidas
        /// </summary>
        /// <param name="password">Senha informada</param>
        /// <param name="privateKey">Chave privada da aplicação</param>
        /// <returns>Resposta da validação da senha</returns>
        public PasswordStrengthResponse Validate(Password password, string privateKey)
        {
            string passwordDecrypted = password.GetDecryptedValue(privateKey);
            var score = PasswordStrengthScore.Strong;
            var brokenRules = new Dictionary<PasswordStrengthRule, string>();
            
            foreach(var rule in Rules)
            {
                bool broken = false;

                switch (rule.Key)
                {
                    case PasswordStrengthRule.MinimumLength:
                        int minLength = GetLengthValue(rule.Key);
                        broken = passwordDecrypted.Length < minLength;
                        break;
                    case PasswordStrengthRule.MaximumLength:
                        int maxLength = GetLengthValue(rule.Key);
                        broken = passwordDecrypted.Length > maxLength;
                        break;
                    case PasswordStrengthRule.Numbers:
                        var numbersResult = new Regex("(?=.*\\d)").Match(passwordDecrypted);
                        broken = numbersResult != null && !numbersResult.Success;
                        break;
                    case PasswordStrengthRule.UppercaseLetters:
                        var upperCaseResult = new Regex("(?=.*[A-Z])").Match(passwordDecrypted);
                        broken = upperCaseResult != null && !upperCaseResult.Success;
                        break;
                    case PasswordStrengthRule.LowercaseLetters:
                        var lowerCaseResult = new Regex("(?=.*[a-z])").Match(passwordDecrypted);
                        broken = lowerCaseResult != null && !lowerCaseResult.Success;
                        break;
                    case PasswordStrengthRule.SpecialCharacters:
                        var specialCharactersResult = new Regex("(?=.*[-+_!@#$%^&*., ?])").Match(passwordDecrypted);
                        broken = specialCharactersResult != null && !specialCharactersResult.Success;
                        break;
                    default:
                        break;
                }

                if (broken)
                    brokenRules.Add(rule.Key, rule.Value);
            }

            if (brokenRules.Count == Rules.Count)
                score = PasswordStrengthScore.Weak;
            else if (brokenRules.Count > 0)
                score = PasswordStrengthScore.Medium;

            if (brokenRules.Count == 0)
                return PasswordStrengthResponse.BuildValid();
            else
                return PasswordStrengthResponse.BuildInvalid(brokenRules, score);
        }
        #endregion

        #region [Construção de regras com pesos]
        /// <summary>
        /// Constrói regras de nível "muito-fraco"
        /// </summary>
        /// <returns>Regras de senha</returns>
        public static PasswordRules BuildVeryWeak()
        {
            var rules = new Dictionary<PasswordStrengthRule, string>
            {
                { PasswordStrengthRule.LowercaseLetters, true.ToString() }
            };

            return new PasswordRules(rules);
        }

        /// <summary>
        /// Constrói regras de nível "fraco"
        /// </summary>
        /// <returns>Regras de senha</returns>
        public static PasswordRules BuildWeak()
        {
            var rules = new Dictionary<PasswordStrengthRule, string>
            {
                { PasswordStrengthRule.Numbers, true.ToString() },
                { PasswordStrengthRule.LowercaseLetters, true.ToString() }
            };

            return new PasswordRules(rules);
        }

        /// <summary>
        /// Constrói regras de nível "médio"
        /// </summary>
        /// <returns>Regras de senha</returns>
        public static PasswordRules BuildMedium()
        {
            var rules = new Dictionary<PasswordStrengthRule, string>
            {
                { PasswordStrengthRule.MinimumLength, "4" },
                { PasswordStrengthRule.Numbers, true.ToString() },
                { PasswordStrengthRule.LowercaseLetters, true.ToString() }
            };

            return new PasswordRules(rules);
        }

        /// <summary>
        /// Constrói regras de nível "forte"
        /// </summary>
        /// <returns>Regras de senha</returns>
        public static PasswordRules BuildStrong()
        {
            var rules = new Dictionary<PasswordStrengthRule, string>
            {
                { PasswordStrengthRule.MinimumLength, "8" },
                { PasswordStrengthRule.MaximumLength, "12" },
                { PasswordStrengthRule.Numbers, true.ToString() },
                { PasswordStrengthRule.UppercaseLetters, true.ToString() },
                { PasswordStrengthRule.LowercaseLetters, true.ToString() }
            };

            return new PasswordRules(rules);
        }

        /// <summary>
        /// Constrói regras de nível "muito-forte"
        /// </summary>
        /// <returns>Regras de senha</returns>
        public static PasswordRules BuildVeryStrong()
        {
            var rules = new Dictionary<PasswordStrengthRule, string>
            {
                { PasswordStrengthRule.MinimumLength, "8" },
                { PasswordStrengthRule.MaximumLength, "12" },
                { PasswordStrengthRule.Numbers, true.ToString() },
                { PasswordStrengthRule.UppercaseLetters, true.ToString() },
                { PasswordStrengthRule.LowercaseLetters, true.ToString() },
                { PasswordStrengthRule.SpecialCharacters, true.ToString() }
            };

            return new PasswordRules(rules);
        }
        #endregion
    }
}
