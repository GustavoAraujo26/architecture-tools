using ArchitectureTools.Period;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArchitectureTools.Pagination
{
    /// <summary>
    /// Container de resposta de paginação
    /// </summary>
    /// <typeparam name="T">Tipo do dado a ser retornado</typeparam>
    public sealed class PaginationResponse<T>
    {
        /// <summary>
        /// Cria nova resposta de paginação
        /// </summary>
        /// <param name="page">Dados da página selecionada</param>
        /// <param name="content">Conteúdo da página (lista de itens)</param>
        [JsonConstructor]
        public PaginationResponse(Page page, List<T> content)
        {
            Page = page;
            Content = content;
        }

        /// <summary>
        /// Dados da página selecionada
        /// </summary>
        [JsonInclude]
        public Page Page { get; private set; }

        /// <summary>
        /// Conteúdo da página (lista de itens)
        /// </summary>
        [JsonInclude]
        public List<T> Content { get; private set; }

        /// <summary>
        /// Converte container para JSON
        /// </summary>
        /// <returns>JSON</returns>
        public override string ToString() => JsonSerializer.Serialize(this);

        /// <summary>
        /// Deserializa JSON
        /// </summary>
        /// <param name="json">JSON a ser deserializado</param>
        /// <returns>Container-resposta de paginação</returns>
        public static PaginationResponse<T> Deserialize(string json) =>
            JsonSerializer.Deserialize<PaginationResponse<T>>(json);
    }
}
