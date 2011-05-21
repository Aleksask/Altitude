//
// Copyright (C) 2011 Thomas Mitchell
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//

using System;
using Chat.Main.Model;
using Chat.Main.Services;
using Chat.Main.Services.Factories;

namespace Chat.Main.App
{
    public class BasicChatAppFactory : ChatAppFactoryBase
    {
        protected override ILogService CreateLogService(IServiceLocator serviceLocator)
        {
            return new ConsoleLogService(serviceLocator);
        }

        protected override IUserFactoryService CreateUserFactoryService(IServiceLocator serviceLocator)
        {
            return new UserFactoryService(serviceLocator);
        }

        protected override IMessageFactoryService CreateMessageFactoryService(IServiceLocator serviceLocator)
        {
            return new MessageFactoryService(serviceLocator);
        }

        protected override ICategoryFactoryService CreateCategoryFactoryService(IServiceLocator serviceLocator)
        {
            return new CategoryFactoryService(serviceLocator);
        }

        protected override ICategoryService CreateCategoryService(IServiceLocator serviceLocator)
        {
            return new CategoryService(serviceLocator);
        }

        protected override IUserService CreateUserService(IServiceLocator serviceLocator)
        {
            return new UserService(serviceLocator);
        }

        protected override IMessageService CreateMessageService(IServiceLocator serviceLocator)
        {
            return new MessageService(serviceLocator);
        }

        protected override void OnAfterRegisterServices(IServiceLocator serviceLocator)
        {
            base.OnAfterRegisterServices(serviceLocator);

            // Populate users
            var userService = serviceLocator.GetService<IUserService>();
            userService.CreateUser(userService.CreateSignUpInfo("Tom", "TomsPassword", "tmitchel2@googlemail.com"));
            userService.CreateUser(userService.CreateSignUpInfo("Aleksas", "AleksasPassword", "aleksas@googlemail.com"));
            userService.CreateUser(userService.CreateSignUpInfo("Mike", "MikesPassword", "mike@googlemail.com"));

            // Populate categories
            var categoryService = serviceLocator.GetService<ICategoryService>();
            categoryService.CreateCategory(GetCategoryInfo());

            // Populate messages
            var messageService = serviceLocator.GetService<IMessageService>();
        }

        public static CategoryInfo GetCategoryInfo()
        {
            return
                new CategoryInfo("Everything",
                             new CategoryInfo("News"),
                             new CategoryInfo("Sport",
                                          new CategoryInfo("Football",
                                                       new CategoryInfo("Football Teams",
                                                                    new CategoryInfo("Manchester United"),
                                                                    new CategoryInfo("Chelsea"),
                                                                    new CategoryInfo("Manchester City"),
                                                                    new CategoryInfo("Arsenal"),
                                                                    new CategoryInfo("Tottenham Hotspur"),
                                                                    new CategoryInfo("Liverpool"),
                                                                    new CategoryInfo("Everton"),
                                                                    new CategoryInfo("Fulham"),
                                                                    new CategoryInfo("Stoke"),
                                                                    new CategoryInfo("Bolton"),
                                                                    new CategoryInfo("West Brom"),
                                                                    new CategoryInfo("Newscastle"),
                                                                    new CategoryInfo("Aston Villa"),
                                                                    new CategoryInfo("Sunderland"),
                                                                    new CategoryInfo("Blackburn"),
                                                                    new CategoryInfo("Wolverhampton"),
                                                                    new CategoryInfo("Birmingham"),
                                                                    new CategoryInfo("Blackpool"),
                                                                    new CategoryInfo("Wigan"),
                                                                    new CategoryInfo("West Ham"))
                                              )),
                             new CategoryInfo("Software",
                                          new CategoryInfo("Software Language",
                                                       new CategoryInfo("C#"),
                                                       new CategoryInfo("Java"),
                                                       new CategoryInfo("Go"),
                                                       new CategoryInfo("C"),
                                                       new CategoryInfo("C++"),
                                                       new CategoryInfo("Visual Basic"),
                                                       new CategoryInfo("Python"),
                                                       new CategoryInfo("Ruby")))
                    );
        }
    }
}
