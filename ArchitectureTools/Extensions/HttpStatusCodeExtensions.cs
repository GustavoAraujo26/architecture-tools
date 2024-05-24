using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace ArchitectureTools.Extensions
{
    /// <summary>
    /// Extensões para o HttpStatusCode
    /// </summary>
    public static class HttpStatusCodeExtensions
    {
        /// <summary>
        /// Retorna descrição do status HTTP
        /// </summary>
        /// <param name="statusCode">Status HTTP</param>
        /// <returns>Descrição do status HTTP</returns>
        public static string GetDescription(this HttpStatusCode statusCode) => 
            ReasonPhrases.GetReasonPhrase((int)statusCode);
    }
}
