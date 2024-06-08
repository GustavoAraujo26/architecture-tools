using System.Collections.Generic;

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
        /// <param name="itemCount">Quantidade de itens a serem paginados</param>
        public Page(int selected, int size, int? itemCount = null)
        {
            Selected = selected;
            Size = size;
            LastPage = null;

            if (itemCount.HasValue)
                LastPage = CalculateLastPage(itemCount.Value, size);
        }

        /// <summary>
        /// Página selecionada
        /// </summary>
        public int Selected { get; private set; }

        /// <summary>
        /// Tamanho da página
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Última página
        /// </summary>
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

        private int? CalculateLastPage(int listCount, int pageSize)
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
