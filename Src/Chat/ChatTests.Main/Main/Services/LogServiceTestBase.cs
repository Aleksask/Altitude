using Chat.Main.Services;
using NUnit.Framework;

namespace ChatTests.Main.Services
{
    [TestFixture]
    public abstract class LogServiceTestBase
    {
        protected abstract ILogService GetLogService();

        [Test]
        public void DebuggingDoesntFail()
        {
            var logService = GetLogService();
            logService.Debug("Test");
        }

        [Test]
        public void DebuggingWithArgsDoesntFail()
        {
            var logService = GetLogService();
            logService.Debug("Test {0}", "Arg");
        }
    }
}
