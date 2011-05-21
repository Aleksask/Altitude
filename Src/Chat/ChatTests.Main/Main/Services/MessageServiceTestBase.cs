using System;
using System.Collections.Generic;
using System.Linq;
using Chat.Main.App;
using Chat.Main.Model;
using Chat.Main.Services;
using NUnit.Framework;

namespace ChatTests.Main.Services
{
    [TestFixture]
    public abstract class MessageServiceTestBase
    {
        protected abstract IMessageService GetMessageService();

        protected abstract IUserService GetUserService();

        protected abstract ICategoryService GetCategoryService();

        [Test]
        public void CanCreateCategories()
        {
            CreateDefaultData();

            var messageService = GetMessageService();
            var messageInfos = GetMessageInfos();
            messageService.PostMessages(messageInfos);
        }

        private void CreateDefaultData()
        {
            // Create categories
            GetCategoryService().CreateCategory(BasicChatAppFactory.GetCategoryInfo());

            // Create users
            GetUserService().CreateUser(new SignUpInfo("tmitchel2", "tmitchel2", "tmitchel2@googlemail.com"));
            GetUserService().CreateUser(new SignUpInfo("mburgess", "mburgess", "michael@mnetuk.com"));
            GetUserService().CreateUser(new SignUpInfo("akekys", "akekys", "akekys@gmail.com"));
        }

        private IEnumerable<IMessageInfo> GetMessageInfos()
        {
            var tmitchel2User = GetUserService().GetUser("tmitchel2");
            var mburgessUser = GetUserService().GetUser("mburgess");
            var akekysUser = GetUserService().GetUser("akekys");

            var newsCategory = GetCategoryService().GetCategory("News");
            var tottenhamHotspurCategory = GetCategoryService().GetCategory("Tottenham Hotspur");

            return new[]
                       {
                           new MessageInfo("I heard Harry Redknapp has told Jermain Defoe to lift his level instead of complaining about his lack of first-team football.",
                               tmitchel2User, new[] {newsCategory, tottenhamHotspurCategory}, new DateTimeOffset(2011, 5, 21, 13, 0, 0, TimeSpan.Zero)),
                           new MessageInfo("Yeah mate :)", 
                               akekysUser, new[] {newsCategory, tottenhamHotspurCategory}, new DateTimeOffset(2011, 5, 21, 13, 10, 0, TimeSpan.Zero)),
                           new MessageInfo("Knob off", 
                               mburgessUser, new[] {newsCategory, tottenhamHotspurCategory}, new DateTimeOffset(2011, 5, 21, 13, 20, 0, TimeSpan.Zero))
                       };
        }
    }
}
