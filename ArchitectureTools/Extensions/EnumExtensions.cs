using ArchitectureTools.Structs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ArchitectureTools.Extensions
{
    /// <summary>
    /// Extensões para enumeradores
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Retorna o valor do atributo de descrição do enumerador
        /// </summary>
        /// <param name="enumValue">Valor do enumerador</param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(description);

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    description = ((DescriptionAttribute)attrs[0]).Description;
            }

            return description;
        }

        /// <summary>
        /// Obtém os dados da opção do enumerador
        /// </summary>
        /// <param name="enumValue">Opção do enumerador</param>
        /// <returns>Container de dados do enumerador</returns>
        public static EnumData GetData(this Enum enumValue) => 
            new EnumData((int)(object)enumValue, enumValue.ToString(), enumValue.GetDescription());

        /// <summary>
        /// Lista todos os valores do enumerador
        /// </summary>
        /// <typeparam name="T">Tipo do enumerador</typeparam>
        /// <param name="enumValue">Valor do enumerador</param>
        /// <returns></returns>
        public static List<EnumData> List<T>(this Enum enumValue) where T : Enum =>
            Enum.GetValues(typeof(T)).Cast<T>().Select(x => x.GetData()).ToList();
    }
}
