using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ArchitectureTools.HttpLibrary
{
    /// <summary>
    /// Requisição de chamadas para recurso (HTTP)
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public sealed class ApiEndpointRequest<TResult>
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="endpointKey">Chave identificadora do endpoint</param>
        /// <param name="queryParameters">Parâmetros de pesquisa</param>
        /// <param name="requestContent">Conteúdo da requisição</param>
        /// <param name="authenticationToken">Token de autenticação</param>
        public ApiEndpointRequest(string endpointKey, string[]? queryParameters = null,
            TResult requestContent = default(TResult), KeyValuePair<string, string>? authenticationToken = null)
        {
            EndpointKey = endpointKey;
            QueryParameters = queryParameters;
            RequestContent = requestContent;
            AuthenticationToken = authenticationToken;
        }

        /// <summary>
        /// Chave identificadora do endpoint
        /// </summary>
        [JsonInclude]
        public string EndpointKey { get; private set; }

        /// <summary>
        /// Parâmetros de pesquisa
        /// </summary>
        [JsonInclude]
        public string[]? QueryParameters { get; private set; }

        /// <summary>
        /// Conteúdo da requisição
        /// </summary>
        [JsonInclude]
        public TResult RequestContent { get; private set; }

        /// <summary>
        /// Token de autenticação
        /// </summary>
        [JsonInclude]
        public KeyValuePair<string, string>? AuthenticationToken { get; private set; }
    }
}
