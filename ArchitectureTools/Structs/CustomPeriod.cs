using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ArchitectureTools.Structs
{
    /// <summary>
    /// Período customizado para trabalhar com range de datas
    /// </summary>
    public struct CustomPeriod
    {
        [JsonConstructor]
        public CustomPeriod(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Data de início
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        /// Data de término
        /// </summary>
        public DateTime End { get; private set; }

        /// <summary>
        /// Realiza validação nas datas
        /// </summary>
        /// <returns>Container com retorno da validação</returns>
        public AppResponse<object> Validate()
        {
            List<string> errors = new List<string>();

            if (Start == DateTime.MinValue)
                errors.Add("Data de início invalida!");

            if (End == DateTime.MinValue)
                errors.Add("Data de termino invalida!");

            if (Start > End)
                errors.Add("Data de inicio não pode ser maior que data de termino");

            if (errors.Count > 0)
                return AppResponse<object>.Ok();
            else
                return AppResponse<object>.UnprocessableEntity(errors);
        }
    }
}
