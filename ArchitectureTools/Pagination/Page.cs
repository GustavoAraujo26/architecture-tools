using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArchitectureTools.Pagination
{
    /// <summary>
    /// Página (utilizada nas paginações de listas)
    /// </summary>
    public struct Page
    {
        /// <summary>
        /// Cria nova página
        /// </summary>
        /// <param name="selected">Página selecionada</param>
        /// <param name="size">Tamanho da página</param>
        /// <param name="lastPage">Última página</param>
        [JsonConstructor]
        public Page(int selected, int size, int? lastPage)
        {
            Selected = selected;
            Size = size;
            LastPage = lastPage;
        }

        /// <summary>
        /// Página selecionada
        /// </summary>
        [JsonInclude]
        public int Selected { get; private set; }

        /// <summary>
        /// Tamanho da página
        /// </summary>
        [JsonInclude]
        public int Size { get; private set; }

        /// <summary>
        /// Última página
        /// </summary>
        [JsonInclude]
        public int? LastPage { get; private set; }

        /// <summary>
        /// Quantidade de itens a serem ignorados no cálculo da lista
        /// </summary>
        public int Skip
        {
            get
            {
                if (Selected == 1)
                    return 0;

                return (Selected - 1) * Size;
            }
        }

        /// <summary>
        /// Página anterior
        /// </summary>
        public int? Previous
        {
            get
            {
                if (Selected == 0 || Selected == 1)
                    return null;

                return Selected - 1;
            }
        }

        /// <summary>
        /// Lista de páginas anteriores
        /// </summary>
        public List<int> PreviousPages
        {
            get
            {
                if (Selected == 0 || Selected == 1)
                    return new List<int>();

                if (Previous == null || Previous == 0)
                    return new List<int>();

                var result = new List<int>();

                for (int i = Previous.Value; i > 0; i--)
                {
                    if (result.Count == 5)
                        break;

                    result.Add(i);
                }

                return result;
            }
        }

        /// <summary>
        /// Próxima página
        /// </summary>
        public int? Next
        {
            get
            {
                if (LastPage is null)
                    return null;

                if (Selected == LastPage.Value)
                    return null;

                var next = Selected + 1;
                if (next > LastPage.Value)
                    return null;

                return next;
            }
        }

        /// <summary>
        /// Lista de próximas páginas
        /// </summary>
        public List<int> NextPages
        {
            get
            {
                if (LastPage == null)
                    return new List<int>();

                if (Selected == LastPage.Value)
                    return new List<int>();

                if (Next == null || Next == 0)
                    return new List<int>();

                var result = new List<int>();

                for (int i = Next.Value; i <= LastPage.Value; i++)
                {
                    if (result.Count == 5)
                        break;

                    result.Add(i);
                }

                return result;
            }
        }

        /// <summary>
        /// Converte página em JSON
        /// </summary>
        /// <returns>JSON</returns>
        public override string ToString() => 
            JsonSerializer.Serialize(this);

        /// <summary>
        /// Cria nova página, calculando os dados com base na quantidade total de itens
        /// </summary>
        /// <param name="selectedPage">Página selecionada</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <param name="totalItems">Quantidade de itens a serem paginados</param>
        /// <returns></returns>
        public static Page Create(int selectedPage, int pageSize, int totalItems) =>
            new Page(selectedPage, pageSize, CalculateLastPage(totalItems, pageSize));

        /// <summary>
        /// Deserializa o JSON nos dados da página
        /// </summary>
        /// <param name="json">JSON</param>
        /// <returns>Dados da página</returns>
        public static Page Deserialize(string json) => 
            JsonSerializer.Deserialize<Page>(json);

        private static int? CalculateLastPage(int listCount, int pageSize)
        {
            if (listCount == 0 || pageSize == 0)
                return null;

            int totalPages = listCount / pageSize;
            int remainder = listCount % pageSize;
            if (remainder > 0)
                totalPages += 1;

            return totalPages;
        }
    }
}
