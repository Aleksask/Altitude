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
            userService.AddUser(userService.CreateSignUpInfo("Tom", "TomsPassword", "tmitchel2@googlemail.com"));
            userService.AddUser(userService.CreateSignUpInfo("Aleksas", "AleksasPassword", "aleksas@googlemail.com"));
            userService.AddUser(userService.CreateSignUpInfo("Mike", "MikesPassword", "mike@googlemail.com"));

            // Populate categories
            var categoryService = (CategoryService) serviceLocator.GetService<ICategoryService>();
            categoryService.AddCategory(new CategoryWithParents(1, "Everything", new long[] { }));
            categoryService.AddCategory(new CategoryWithParents(2, "Sport", new long[] { 1 }));
            categoryService.AddCategory(new CategoryWithParents(3, "Software", new long[] { 1 }));
            categoryService.AddCategory(new CategoryWithParents(4, "Football Teams", new long[] { 2 }));
            categoryService.AddCategory(new CategoryWithParents(7, "C#", new long[] { 3 }));
            categoryService.AddCategory(new CategoryWithParents(8, "Java", new long[] { 3 }));
            categoryService.AddCategory(new CategoryWithParents(5, "Tottenham Hotspurs", new long[] { 4 }));
            categoryService.AddCategory(new CategoryWithParents(6, "Chelsea", new long[] { 4 }));

            // Populate messages
            var messageService = serviceLocator.GetService<IMessageService>();
        }
    }
}
