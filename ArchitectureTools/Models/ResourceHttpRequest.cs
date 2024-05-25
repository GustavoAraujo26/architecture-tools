using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectureTools.Models
{
    /// <summary>
    /// Requisição de chamadas para recurso (HTTP)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ResourceHttpRequest<T> where T : class
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="endpointKey">Chave identificadora do endpoint</param>
        /// <param name="queryParameters">Parâmetros de pesquisa</param>
        /// <param name="requestContent">Conteúdo da requisição</param>
        /// <param name="authenticationToken">Token de autenticação</param>
        public ResourceHttpRequest(string endpointKey, string[]? queryParameters = null, 
            T? requestContent = null, KeyValuePair<string, string>? authenticationToken = null)
        {
            EndpointKey = endpointKey;
            QueryParameters = queryParameters;
            RequestContent = requestContent;
            AuthenticationToken = authenticationToken;
        }

        /// <summary>
        /// Chave identificadora do endpoint
        /// </summary>
        public string EndpointKey { get; private set; }

        /// <summary>
        /// Parâmetros de pesquisa
        /// </summary>
        public string[]? QueryParameters { get; private set; }

        /// <summary>
        /// Conteúdo da requisição
        /// </summary>
        public T? RequestContent { get; private set; }

        /// <summary>
        /// Token de autenticação
        /// </summary>
        public KeyValuePair<string, string>? AuthenticationToken { get; private set; }
    }
}
