using ArchitectureTools.Security;

namespace ArchitectureTools.Tests.Security
{
    public class SecurityKeyTests
    {
        [Fact]
        public void ShouldReturnSuccess_WhenGenerate()
        {
            var key = SecurityKey.BuildNew();
            var otherKey = SecurityKey.BuildNew();

            bool equal = key.Compare(otherKey.Value);
            Assert.False(equal);

            equal = key.Compare(key.Value);
            Assert.True(equal);
        }
    }
}
