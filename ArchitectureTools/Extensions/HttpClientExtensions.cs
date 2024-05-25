using ArchitectureTools.Structs;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArchitectureTools.Extensions
{
    /// <summary>
    /// Extensões para clientes HTTP
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Converte mensagem de resposta de chamada HTTP para o container de resposta de aplicação
        /// </summary>
        /// <typeparam name="T">Tipo do conteúdo retornado</typeparam>
        /// <param name="httpResponseMessage">Mensagem de resposta de chamada HTTP</param>
        /// <returns>Container de resposta</returns>
        public static async Task<AppResponse<T>> ToAppResponse<T>(this HttpResponseMessage httpResponseMessage) 
            where T : class
        {
            try
            {
                var messageContent = await httpResponseMessage.Content.ReadAsStringAsync();

                if (!httpResponseMessage.IsSuccessStatusCode)
                    return AppResponse<T>.Custom(httpResponseMessage.StatusCode, messageContent);

                var responseContent = JsonSerializer.Deserialize<T>(messageContent);
                return AppResponse<T>.Ok(responseContent);
            }
            catch(Exception ex)
            {
                return AppResponse<T>.InternalError(ex);
            }
        }
    }
}
