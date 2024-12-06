using ArchitectureTools.Responses;
using ArchitectureTools.Tests.Models;

namespace ArchitectureTools.Tests.StructTests
{
    public class AppResponseTests
    {
        [Fact]
        public void ShouldReturnSuccess_WhenDeserializeSuccessfully()
        {
            var response = new ArchitectureTools.Responses.ActionResponse<ModelTest>();
            var response2 = new ArchitectureTools.Responses.ActionResponse<Guid>();

            var fakeData = new ModelTest("test");
            var appResponse = ActionResponse<ModelTest>.Ok(fakeData);

            var json = appResponse.ToString();

            var responseDeserialized = ActionResponse<ModelTest>.Deserialize(json);

            Assert.NotNull(json);
            Assert.NotNull(responseDeserialized);
            Assert.True(responseDeserialized.IsSuccess);
        }

        [Fact]
        public void ShouldReturnSuccess_WhenDeserializeSuccessfullyForStructs()
        {
            Guid id = Guid.NewGuid();
            var response1 = ActionResponse<Guid>.Ok(id);
            var json1 = response1.ToString();
            var response1Deserialized = ActionResponse<Guid>.Deserialize(json1);

            int number = new Random().Next();
            var response2 = ActionResponse<int>.Ok(number);
            var json2 = response2.ToString();
            var response2Deserialized = ActionResponse<int>.Deserialize(json2);

            var date = DateTime.Now;
            var response3 = ActionResponse<DateTime>.Ok(date);
            var json3 = response3.ToString();
            var response3Deserialized = ActionResponse<DateTime>.Deserialize(json3);

            decimal value = (decimal)new Random().NextDouble();
            var response4 = ActionResponse<decimal>.Ok(value);
            var json4 = response4.ToString();
            var response4Deserialized = ActionResponse<decimal>.Deserialize(json4);

            Assert.True(response1Deserialized.IsSuccess);
            Assert.True(response2Deserialized.IsSuccess);
            Assert.True(response3Deserialized.IsSuccess);
            Assert.True(response4Deserialized.IsSuccess);
        }

        [Fact]
        public void ShouldReturnSuccess_WhenOkContainer()
        {
            var fakeData = new ModelTest("test");
            var appResponse = ActionResponse<ModelTest>.Ok(fakeData);

            Assert.NotNull(appResponse);
            Assert.True(appResponse.IsSuccess);
        }

        [Fact]
        public void ShouldReturnFailure_WhenNotOkContainer()
        {
            var appResponse = ActionResponse<ModelTest>.BadRequest();

            Assert.NotNull(appResponse);
            Assert.False(appResponse.IsSuccess);
        }

        [Fact]
        public void ShouldReturnFailure_ExceptionContainer()
        {
            var appResponse = ActionResponse<ModelTest>.InternalError(new Exception("Error"));

            Assert.NotNull(appResponse);
            Assert.False(appResponse.IsSuccess);
        }

        [Fact]
        public void ShouldReturnFailure_WhenValidationResponseList()
        {
            var validations = new List<ValidationResponse>();

            for (int i = 1; i <= 5; i++)
                validations.Add(ValidationResponse.Build($"Error {i}", i.ToString(), $"Property {i}"));

            var appResponse = ActionResponse<ModelTest>.UnprocessableEntity(validations);

            Assert.False(appResponse.IsSuccess);
            Assert.NotEmpty(appResponse.Validations);
        }
    }
}
