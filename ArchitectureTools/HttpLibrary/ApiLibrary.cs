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
        private List<ApiResource> _apiResources;

        /// <summary>
        /// Cria nova instância de biblioteca de recursos de uma API específica
        /// </summary>
        public ApiLibrary()
        {
            _apiResources = new List<ApiResource>();
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
