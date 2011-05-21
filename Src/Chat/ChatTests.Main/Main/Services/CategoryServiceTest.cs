using Chat.Main.Services;
using Chat.Main.Services.Factories;
using NUnit.Framework;

namespace ChatTests.Main.Services
{
    [TestFixture]
    class CategoryServiceTest : CategoryServiceTestBase
    {
        private ServiceLocator _serviceLocator;

        [SetUp]
        public void Init()
        {
            _serviceLocator = new ServiceLocator();
            _serviceLocator.RegisterService<ICategoryFactoryService>(new CategoryFactoryService(_serviceLocator));
            _serviceLocator.RegisterService<ICategoryService>(new CategoryService(_serviceLocator));
        }

        [TearDown]
        public void Dispose()
        {
            
        }
        
        protected override ICategoryService GetCategoryService()
        {
            return _serviceLocator.GetService<ICategoryService>();
        }
    }
}