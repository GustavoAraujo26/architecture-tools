using ArchitectureTools.Pagination;
using ArchitectureTools.Tests.Models;
using Bogus;

namespace ArchitectureTools.Tests.StructTests
{
    public class PaginationResponseTests
    {
        [Fact]
        public void ShouldReturnSuccess_WhenDeserializeFirstPage()
        {
            var fakeData = new Faker<ModelTest>().Generate(10);
            var page = new Page(1, 10, 5000);
            
            var response = new PaginationResponse<ModelTest>(page, fakeData);

            var json = response.ToString();

            var newResponse = PaginationResponse<ModelTest>.Deserialize(json);

            Assert.NotNull(newResponse.Page.Next);
            Assert.NotEmpty(newResponse.Page.NextPages);
            Assert.Null(newResponse.Page.Previous);
            Assert.Empty(newResponse.Page.PreviousPages);
        }

        [Fact]
        public void ShouldReturnSuccess_WhenDeserializeMiddlePage()
        {
            var fakeData = new Faker<ModelTest>().Generate(10);
            var page = new Page(10, 10, 5000);

            var response = new PaginationResponse<ModelTest>(page, fakeData);

            var json = response.ToString();

            var newResponse = PaginationResponse<ModelTest>.Deserialize(json);

            Assert.NotNull(newResponse.Page.Next);
            Assert.NotEmpty(newResponse.Page.NextPages);
            Assert.NotNull(newResponse.Page.Previous);
            Assert.NotEmpty(newResponse.Page.PreviousPages);
        }

        [Fact]
        public void ShouldReturnSuccess_WhenDeserializeLastPage()
        {
            var fakeData = new Faker<ModelTest>().Generate(10);
            var page = new Page(5000, 10, 5000);

            var response = new PaginationResponse<ModelTest>(page, fakeData);

            var json = response.ToString();

            var newResponse = PaginationResponse<ModelTest>.Deserialize(json);

            Assert.Null(newResponse.Page.Next);
            Assert.Empty(newResponse.Page.NextPages);
            Assert.NotNull(newResponse.Page.Previous);
            Assert.NotEmpty(newResponse.Page.PreviousPages);
        }
    }
}
