using System;
using Chat.Main.Services;
using Chat.Main.Services.Factories;
using NUnit.Framework;

namespace ChatTests.Main.Services
{
    [TestFixture]
    public class MessageServiceTest : MessageServiceTestBase
    {
        private ServiceLocator _serviceLocator;

        [SetUp]
        public void Init()
        {
            _serviceLocator = new ServiceLocator();

            _serviceLocator.RegisterService<IUserFactoryService>(new UserFactoryService(_serviceLocator));
            _serviceLocator.RegisterService<IUserService>(new UserService(_serviceLocator));

            _serviceLocator.RegisterService<ICategoryFactoryService>(new CategoryFactoryService(_serviceLocator));
            _serviceLocator.RegisterService<ICategoryService>(new CategoryService(_serviceLocator));

            _serviceLocator.RegisterService<IMessageFactoryService>(new MessageFactoryService(_serviceLocator));
            _serviceLocator.RegisterService<IMessageService>(new MessageService(_serviceLocator));
        }

        [TearDown]
        public void Dispose()
        {

        }

        protected override IMessageService GetMessageService()
        {
            return _serviceLocator.GetService<IMessageService>();
        }

        protected override IUserService GetUserService()
        {
            return _serviceLocator.GetService<IUserService>();
        }

        protected override ICategoryService GetCategoryService()
        {
            return _serviceLocator.GetService<ICategoryService>();
        }
    }
}