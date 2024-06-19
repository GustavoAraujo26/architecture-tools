using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using ArchitectureTools.Responses;

namespace ArchitectureTools.Period
{
    /// <summary>
    /// Período customizado para trabalhar com range de datas
    /// </summary>
    public struct PeriodRange
    {
        [JsonConstructor]
        public PeriodRange(DateTime start, DateTime end)
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
        public ActionResponse<object> Validate()
        {
            List<string> errors = new List<string>();

            if (Start == DateTime.MinValue)
                errors.Add("Data de início invalida!");

            if (End == DateTime.MinValue)
                errors.Add("Data de termino invalida!");

            if (Start > End)
                errors.Add("Data de inicio não pode ser maior que data de termino");

            if (errors.Count == 0)
                return ActionResponse<object>.Ok();
            else
                return ActionResponse<object>.UnprocessableEntity(errors);
        }

        /// <summary>
        /// Cria novo intervalo de período
        /// </summary>
        /// <param name="start">Data de início</param>
        /// <param name="end">Data de término</param>
        /// <returns>Container com o intervalo</returns>
        public static PeriodRange Create(DateTime start, DateTime end) =>
            new PeriodRange(start, end);

        /// <summary>
        /// Retorna JSON
        /// </summary>
        /// <returns>string de JSON</returns>
        public override string ToString() => JsonSerializer.Serialize(this);

        /// <summary>
        /// Deserializa JSON
        /// </summary>
        /// <param name="json">JSON a ser deserializado</param>
        /// <returns>Intervalo de período</returns>
        public static PeriodRange Deserialize(string json) => JsonSerializer.Deserialize<PeriodRange>(json);
    }
}
