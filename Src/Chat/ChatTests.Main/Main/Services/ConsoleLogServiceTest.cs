using Chat.Main.Services;
using NUnit.Framework;

namespace ChatTests.Main.Services
{
    [TestFixture]
    public class ConsoleLogServiceTest : LogServiceTestBase
    {
        private ServiceLocator _serviceLocator;

        [SetUp]
        public void Init()
        {
            _serviceLocator = new ServiceLocator();
            _serviceLocator.RegisterService<ILogService>(new ConsoleLogService(_serviceLocator));
        }

        [TearDown]
        public void Dispose()
        {

        }
        
        protected override ILogService GetLogService()
        {
            return _serviceLocator.GetService<ILogService>();
        }
    }
}