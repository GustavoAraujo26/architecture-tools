using ArchitectureTools.Structs;
using ArchitectureTools.Tests.Models;

namespace ArchitectureTools.Tests.StructTests
{
    public class AppResponseTests
    {
        [Fact]
        public void ShouldReturnSuccess_WhenDeserializeSuccessfully()
        {
            var fakeData = new ModelTest("test");
            var appResponse = AppResponse<ModelTest>.Ok(fakeData);

            var json = appResponse.ToString();

            var responseDeserialized = AppResponse<ModelTest>.Deserialize(json);

            Assert.NotNull(json);
            Assert.NotNull(responseDeserialized);
            Assert.True(responseDeserialized.IsSuccess);
        }

        [Fact]
        public void ShouldReturnSuccess_WhenOkContainer()
        {
            var fakeData = new ModelTest("test");
            var appResponse = AppResponse<ModelTest>.Ok(fakeData);

            Assert.NotNull(appResponse);
            Assert.True(appResponse.IsSuccess);
        }

        [Fact]
        public void ShouldReturnFailure_WhenNotOkContainer()
        {
            var appResponse = AppResponse<ModelTest>.BadRequest();

            Assert.NotNull(appResponse);
            Assert.False(appResponse.IsSuccess);
        }

        [Fact]
        public void ShouldReturnFailure_ExceptionContainer()
        {
            var appResponse = AppResponse<ModelTest>.InternalError(new Exception("Error"));

            Assert.NotNull(appResponse);
            Assert.False(appResponse.IsSuccess);
        }
    }
}
