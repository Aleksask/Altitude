using Chat.Main.Services;
using NUnit.Framework;

namespace ChatTests.Main.Services
{
    [TestFixture]
    public class ServiceLocatorTest
    {
        [Test]
        public void ThrowsServiceNotFoundException()
        {
            var serviceLocator = new ServiceLocator();
            Assert.Throws(typeof (ServiceNotFoundException), () => serviceLocator.GetService<ICategoryService>());
        }
    }
}
