using System.Net.Http;

namespace ArchitectureTools.HttpLibrary
{
    /// <summary>
    /// Endpoint de uma API
    /// </summary>
    public sealed class ApiEndpoint
    {
        private ApiEndpoint(string key, string resource, HttpMethod httpMethod)
        {
            Key = key;
            Resource = resource;
            HttpMethod = httpMethod;
        }

        /// <summary>
        /// Chave identificadora
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Recurso (parte final da URL)
        /// </summary>
        public string Resource { get; private set; }

        /// <summary>
        /// Método HTTP
        /// </summary>
        public HttpMethod HttpMethod { get; private set; }

        /// <summary>
        /// Cria novo endpoint
        /// </summary>
        /// <param name="key">Chave identificadora</param>
        /// <param name="resource">Recurso (parte final da URL)</param>
        /// <param name="method">Método HTTP</param>
        /// <returns></returns>
        public static ApiEndpoint Create(string key, string resource, HttpMethod method) =>
            new ApiEndpoint(key, resource, method);
    }
}
