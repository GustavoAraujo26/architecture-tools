using System.Collections.Generic;
using System.Linq;
using ArchitectureTools.Exceptions;

namespace ArchitectureTools.HttpLibrary
{
    /// <summary>
    /// Biblioteca de API's com seus endpoints para consumo
    /// </summary>
    public sealed class ApiLibrary
    {
        private static ApiLibrary _instance;
        private List<ApiResource> _apiResources;

        private ApiLibrary()
        {
            _apiResources = new List<ApiResource>();
        }

        /// <summary>
        /// Instância da biblioteca
        /// </summary>
        public ApiLibrary Instance
        {
            get
            {
                return _instance ?? (_instance = new ApiLibrary());
            }
        }

        /// <summary>
        /// Adiciona novo recurso à biblioteca
        /// </summary>
        /// <param name="resource">Recurso a ser adicionado</param>
        /// <exception cref="ItemAlreadyRegisteredException">Exceção de item já registrado</exception>
        public void AddResource(ApiResource resource)
        {
            var current = _apiResources.FirstOrDefault(x => x.Key == resource.Key);
            if (current != null)
                throw new ItemAlreadyRegisteredException(resource.Key);

            _apiResources.Add(resource);
        }

        /// <summary>
        /// Retorna recurso com base em sua chave
        /// </summary>
        /// <param name="key">Chave do recurso</param>
        /// <returns>Recurso da API</returns>
        public ApiResource GetResource(string key) =>
            _apiResources.FirstOrDefault(x => x.Key == key);
    }
}
