using ArchitectureTools.Pagination;

namespace ArchitectureTools.Tests.StructTests
{
    public class PageTests
    {
        [Fact]
        public void ShouldReturnSuccess_WhenSimplePagination()
        {
            int totalItems = 20;

            var page = Page.Create(1, 10, totalItems);

            Assert.Equal(2, page.LastPage);
            Assert.Equal(1, page.NextPages.Count);
            Assert.Empty(page.PreviousPages);
            Assert.Null(page.Previous);
        }

        [Fact]
        public void ShouldReturnSuccess_WhenComplexPagination()
        {
            int totalItems = 28;

            var page = Page.Create(1, 10, totalItems);

            Assert.Equal(3, page.LastPage);
            Assert.Equal(2, page.NextPages.Count);
            Assert.Empty(page.PreviousPages);
            Assert.Null(page.Previous);
        }

        [Fact]
        public void ShouldReturnSuccess_WhenNextPage()
        {
            int totalItems = 28;

            var page = Page.Create(2, 10, totalItems);

            Assert.Equal(3, page.LastPage);
            Assert.Equal(1, page.NextPages.Count);
            Assert.NotEmpty(page.PreviousPages);
            Assert.NotNull(page.Previous);
        }

        [Fact]
        public void ShouldReturnSuccess_WhenLastPage()
        {
            int totalItems = 28;

            var page = Page.Create(3, 10, totalItems);

            Assert.NotNull(page.LastPage);
            Assert.Equal(2, page.PreviousPages.Count);
            Assert.NotEmpty(page.PreviousPages);
            Assert.NotNull(page.Previous);
        }

        [Theory]
        [InlineData(1, 10, 105678)]
        [InlineData(50, 10, 105678)]
        public void ShouldReturnSuccess_WhenLargeItems(int pageSelected, int pageSize, int totalItems)
        {
            var page = Page.Create(pageSelected, pageSize, totalItems);

            Assert.NotNull(page.LastPage);
            Assert.NotEmpty(page.NextPages);
        }

        [Fact]
        public void ShouldReturnSuccess_WhenEmptyItemCount()
        {
            var page = new Page(1, 10, null);

            Assert.Null(page.LastPage);
            Assert.Null(page.Next);
            Assert.Null(page.Previous);
            Assert.Empty(page.PreviousPages);
            Assert.Empty(page.NextPages);
        }
    }
}
