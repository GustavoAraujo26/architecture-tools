using System.Collections.Generic;

namespace ArchitectureTools.HttpLibrary
{
    public sealed class ApiHttpCollection
    {
        private static ApiHttpCollection _instance;
        private List<ApiLibrary> _apis;

        private ApiHttpCollection()
        {
            _apis = new List<ApiLibrary>();
        }

        /// <summary>
        /// Instância da biblioteca
        /// </summary>
        public ApiHttpCollection Instance
        {
            get
            {
                return _instance ?? (_instance = new ApiHttpCollection());
            }
        }

        /// <summary>
        /// Adiciona nova biblioteca de API na coleção
        /// </summary>
        /// <param name="api">API a ser inserida</param>
        public void AddApi(ApiLibrary api)
        {
            if (_apis == null)
                _apis = new List<ApiLibrary>();

            _apis.Add(api);
        }
    }
}
