﻿using ArchitectureTools.Exceptions;
using ArchitectureTools.Extensions;
using ArchitectureTools.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ArchitectureTools.HttpLibrary
{
    /// <summary>
    /// Recurso para API's
    /// </summary>
    public sealed class ApiResource
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="key">Chave identificadora da API</param>
        /// <param name="baseUrl">URL-base da API</param>
        /// <param name="endpoints">Lista de endpoints disponíveis para a API</param>
        public ApiResource(string key, string baseUrl, List<ApiEndpoint> endpoints)
        {
            Key = key;
            BaseUrl = baseUrl;
            Endpoints = endpoints;
        }

        /// <summary>
        /// Chave identificadora da API
        /// </summary>
        [JsonInclude]
        public string Key { get; private set; }

        /// <summary>
        /// URL-base da API
        /// </summary>
        [JsonInclude]
        public string BaseUrl { get; private set; }

        /// <summary>
        /// Lista de endpoints disponíveis para a API
        /// </summary>
        [JsonInclude]
        public List<ApiEndpoint> Endpoints { get; private set; }

        /// <summary>
        /// Adiciona novo endpoint
        /// </summary>
        /// <param name="endpoint">Endpoint</param>
        /// <exception cref="ItemAlreadyRegisteredException">Exceção de item já registrado</exception>
        public void AddEndpoint(ApiEndpoint endpoint)
        {
            var current = Endpoints.FirstOrDefault(x => x.Key == endpoint.Key);
            if (current != null)
                throw new ItemAlreadyRegisteredException(endpoint.Key);

            Endpoints.Add(endpoint);
        }

        /// <summary>
        /// Realiza chamada HTTP no endpoint especificado
        /// </summary>
        /// <typeparam name="TRequest">Tipo do conteúdo de requisição</typeparam>
        /// <typeparam name="TResponse">Tipo do conteúdo de resposta</typeparam>
        /// <param name="resourceRequest">Container de requisição para realizar as chamadas</param>
        /// <returns>Container de resposta da chamada HTTP</returns>
        public async Task<ActionResponse<TResponse>> Call<TRequest, TResponse>(ApiEndpointRequest<TRequest> resourceRequest)
        {
            try
            {
                ApiEndpoint endpoint = Endpoints.FirstOrDefault(x => x.Key.Equals(resourceRequest.EndpointKey));
                if (endpoint == null)
                    return ActionResponse<TResponse>.BadRequest($"Endpoint {resourceRequest.EndpointKey} not found!");

                string requestUri = BuildRequestUri(endpoint.Resource, resourceRequest.QueryParameters);

                using (var client = new HttpClient())
                {
                    ConfigureBasicHttpData(client, resourceRequest.AuthenticationToken);

                    var requestMessage = BuildRequestMessage(endpoint.HttpMethod, requestUri, resourceRequest.RequestContent);

                    var httpResponse = await client.SendAsync(requestMessage);
                    return await httpResponse.ToAppResponse<TResponse>();
                }
            }
            catch (Exception ex)
            {
                return ActionResponse<TResponse>.InternalError(ex);
            }
        }

        private string BuildRequestUri(string resource, string[] parameters)
        {
            string requestUri = resource;
            if (parameters != null)
                requestUri = string.Format(resource, parameters);

            return requestUri;
        }

        private void ConfigureBasicHttpData(HttpClient client,
            KeyValuePair<string, string>? authenticationToken = null)
        {
            client.BaseAddress = new Uri(BaseUrl);

            if (authenticationToken != null)
                client.DefaultRequestHeaders.Add("Authentication",
                    $"{authenticationToken.Value.Key} {authenticationToken.Value.Value}");
        }

        private HttpRequestMessage BuildRequestMessage<TRequest>(HttpMethod httpMethod,
            string requestUri, TRequest request = default(TRequest))
        {
            var requestMessage = new HttpRequestMessage(httpMethod, requestUri);

            if (request != null)
            {
                if (!typeof(TRequest).Equals(typeof(Dictionary<string, string>)))
                {
                    requestMessage.Content = new StringContent(JsonSerializer.Serialize(request));
                }
                else
                {
                    var dictionaryContent = (Dictionary<string, string>)Convert.ChangeType(request,
                        typeof(Dictionary<string, string>));
                    requestMessage.Content = new FormUrlEncodedContent(dictionaryContent);
                }
            }

            return requestMessage;
        }
    }
}
