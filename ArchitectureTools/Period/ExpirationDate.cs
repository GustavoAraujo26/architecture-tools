using ArchitectureTools.Enums;
using System;
using System.Text.Json.Serialization;

namespace ArchitectureTools.Period
{
    /// <summary>
    /// Data de expiração
    /// </summary>
    public struct ExpirationDate
    {
        /// <summary>
        /// Construtor para inicializar a propriedade
        /// </summary>
        /// <param name="date">Data</param>
        [JsonConstructor]
        public ExpirationDate(DateTime date)
        {
            Value = date;
        }

        /// <summary>
        /// Construtor para inicializar a propriedade
        /// </summary>
        /// <param name="timeType">Tipo do tempo a ser adicionado</param>
        /// <param name="valueToAdd">Valor a ser adicionado</param>
        /// <param name="useUTC">Utiliza UTC?</param>
        public ExpirationDate(ExpirationTime timeType, long valueToAdd, bool useUTC = false)
        {
            if (useUTC)
                Value = AddToDate(DateTime.UtcNow, timeType, valueToAdd);
            else
                Value = AddToDate(DateTime.Now, timeType, valueToAdd);
        }

        /// <summary>
        /// Construtor para inicializar a propriedade
        /// </summary>
        /// <param name="timeType">Tipo do tempo a ser adicionado</param>
        /// <param name="valueToAdd">Valor a ser adicionado</param>
        /// <param name="useUTC">Utiliza UTC?</param>
        public ExpirationDate(ExpirationTime timeType, int valueToAdd, bool useUTC = false)
        {
            if (useUTC)
                Value = AddToDate(DateTime.UtcNow, timeType, valueToAdd);
            else
                Value = AddToDate(DateTime.Now, timeType, valueToAdd);
        }

        /// <summary>
        /// Valor
        /// </summary>
        public DateTime Value { get; private set; }

        /// <summary>
        /// Verifica se a data não se encontra expirada
        /// </summary>
        /// <param name="date">Data para comparação</param>
        /// <param name="useUTC">Utiliza UTC?</param>
        /// <returns></returns>
        public bool Check(DateTime? date = null, bool useUTC = false)
        {
            var now = DateTime.Now;
            if (useUTC)
                now = DateTime.UtcNow;

            if (date.HasValue)
                now = date.Value;

            return now <= Value;
        }

        /// <summary>
        /// Retorna a diferença entre as datas
        /// </summary>
        /// <param name="timeType">Tipo de tempo a ser devolvido</param>
        /// <param name="date">Data para comparação</param>
        /// <returns>Valor da diferença</returns>
        public double GetDifference(ExpirationTime timeType, DateTime date)
        {
            var timespan = Value.Subtract(date);

            switch (timeType)
            {
                case ExpirationTime.Miliseconds:
                    return timespan.TotalMilliseconds;
                case ExpirationTime.Seconds:
                    return timespan.TotalSeconds;
                case ExpirationTime.Minutes:
                    return timespan.TotalMinutes;
                case ExpirationTime.Hours:
                    return timespan.TotalHours;
                case ExpirationTime.Days:
                    return timespan.TotalDays;
                case ExpirationTime.Months:
                    return Math.Round(timespan.TotalDays / 30, 2);
                case ExpirationTime.Years:
                    return Math.Round(timespan.TotalDays / 365, 2);
                default:
                    return 0;
            }
        }

        private static DateTime AddToDate(DateTime date, ExpirationTime addition, long valueToAdd)
        {
            int valueLimit = int.MaxValue;
            if (valueToAdd < valueLimit)
                valueLimit = int.Parse(valueToAdd.ToString());
            
            switch (addition)
            {
                case ExpirationTime.Miliseconds:
                    return date.AddMilliseconds(valueToAdd);
                case ExpirationTime.Seconds:
                    return date.AddSeconds(valueToAdd);
                case ExpirationTime.Minutes:
                    return date.AddMinutes(valueToAdd);
                case ExpirationTime.Hours:
                    return date.AddHours(valueToAdd);
                case ExpirationTime.Days:
                    return date.AddDays(valueToAdd);
                case ExpirationTime.Months:
                    return date.AddMonths(valueLimit);
                case ExpirationTime.Years:
                    return date.AddYears(valueLimit);
                default:
                    return date;
            }
        }

        private static DateTime AddToDate(DateTime date, ExpirationTime addition, int valueToAdd)
        {
            switch (addition)
            {
                case ExpirationTime.Miliseconds:
                    return date.AddMilliseconds(valueToAdd);
                case ExpirationTime.Seconds:
                    return date.AddSeconds(valueToAdd);
                case ExpirationTime.Minutes:
                    return date.AddMinutes(valueToAdd);
                case ExpirationTime.Hours:
                    return date.AddHours(valueToAdd);
                case ExpirationTime.Days:
                    return date.AddDays(valueToAdd);
                case ExpirationTime.Months:
                    return date.AddMonths(valueToAdd);
                case ExpirationTime.Years:
                    return date.AddYears(valueToAdd);
                default:
                    return date;
            }
        }
    }
}
