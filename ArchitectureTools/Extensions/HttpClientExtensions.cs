using ArchitectureTools.Responses;
using System.Net.Http;
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
        /// <typeparam name="TResult">Tipo do conteúdo retornado</typeparam>
        /// <param name="httpResponseMessage">Mensagem de resposta de chamada HTTP</param>
        /// <returns>Container de resposta</returns>
        public static async Task<ActionResponse<TResult>> ToAppResponse<TResult>(this HttpResponseMessage httpResponseMessage) =>
            await ActionResponse<TResult>.FromHttpResponse(httpResponseMessage);
    }
}
