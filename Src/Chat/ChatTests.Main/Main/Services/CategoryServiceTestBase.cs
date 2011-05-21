using System.Linq;
using Chat.Main.App;
using Chat.Main.Services;
using NUnit.Framework;

namespace ChatTests.Main.Services
{
    [TestFixture]
    abstract class CategoryServiceTestBase
    {
        protected abstract ICategoryService GetCategoryService();
        
        [Test]
        public void CanCreateCategories()
        {
            var service = GetCategoryService();
            var categoryInfo = BasicChatAppFactory.GetCategoryInfo();
            var categories = service.CreateCategory(categoryInfo).ToList();

            Assert.AreEqual(35, categories.Count);
        }

        [Test]
        public void CanGetCategory()
        {
            var service = GetCategoryService();
            var categoryInfo = BasicChatAppFactory.GetCategoryInfo();
            var categories = service.CreateCategory(categoryInfo).ToList();

            const int id = 2;
            var expectedCategory = categories.Where(f => f.Id == id).Single();
            var actualCategory = service.GetCategory(id);

            Assert.AreEqual(expectedCategory, actualCategory);
        }

        [Test]
        public void CanGetChildCategories()
        {
            var service = GetCategoryService();
            var categoryInfo = BasicChatAppFactory.GetCategoryInfo();
            var categories = service.CreateCategory(categoryInfo).ToList();

            var category = categories.Where(f => f.Name == "Football Teams").Single();
            var childCategories = service.GetChildCategories(category).ToList();

            Assert.AreEqual(20, childCategories.Count);
        }
        
        [Test]
        public void CanGetEmptyChildCategories()
        {
            var service = GetCategoryService();
            var categoryInfo = BasicChatAppFactory.GetCategoryInfo();
            var categories = service.CreateCategory(categoryInfo).ToList();

            var category = categories.Where(f => f.Name == "Ruby").Single();
            var childCategories = service.GetChildCategories(category).ToList();

            Assert.AreEqual(0, childCategories.Count);
        }

        [Test]
        public void CanGetParentCategories()
        {
            var service = GetCategoryService();
            var categoryInfo = BasicChatAppFactory.GetCategoryInfo();
            var categories = service.CreateCategory(categoryInfo).ToList();

            var category = categories.Where(f => f.Name == "Football Teams").Single();
            var parentCategory = service.GetParentCategories(category).Single();

            Assert.AreEqual("Football", parentCategory.Name);
        }

        [Test]
        public void CanGetEmptyParentCategories()
        {
            var service = GetCategoryService();
            var categoryInfo = BasicChatAppFactory.GetCategoryInfo();
            var categories = service.CreateCategory(categoryInfo).ToList();

            var category = categories.Where(f => f.Name == "Everything").Single();
            var parentCategories = service.GetParentCategories(category).ToList();

            Assert.AreEqual(0, parentCategories.Count);
        }

        [Test]
        public void CanGetSuggestedCategories()
        {
            var service = GetCategoryService();
            var categoryInfo = BasicChatAppFactory.GetCategoryInfo();
            service.CreateCategory(categoryInfo);

            var suggestedCategories = service.GetSuggestedCategories("Foot").ToList();
            
            Assert.AreEqual(2, suggestedCategories.Count);
            Assert.AreEqual("Football", suggestedCategories[0].Name);
            Assert.AreEqual("Football Teams", suggestedCategories[1].Name);
        }

        [Test]
        public void CanGetSuggestedChildCategories()
        {
            var service = GetCategoryService();
            var categoryInfo = BasicChatAppFactory.GetCategoryInfo();
            service.CreateCategory(categoryInfo);

            var suggestedCategories = service.GetSuggestedCategories("Everything.").ToList();

            Assert.AreEqual(3, suggestedCategories.Count);
            Assert.AreEqual("News", suggestedCategories[0].Name);
            Assert.AreEqual("Sport", suggestedCategories[1].Name);
            Assert.AreEqual("Software", suggestedCategories[2].Name);
        }

        [Test]
        public void CanGetSuggestedParentCategories()
        {
            var service = GetCategoryService();
            var categoryInfo = BasicChatAppFactory.GetCategoryInfo();
            service.CreateCategory(categoryInfo);

            var suggestedCategories = service.GetSuggestedCategories("Football..").ToList();

            Assert.AreEqual(1, suggestedCategories.Count);
            Assert.AreEqual("Sport", suggestedCategories[0].Name);
        }

    }
}
