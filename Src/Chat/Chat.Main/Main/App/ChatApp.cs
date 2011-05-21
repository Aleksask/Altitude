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
using System.Collections.Generic;
using Chat.Main.Model;
using Chat.Main.Services;

namespace Chat.Main.App
{
    /// <summary>
    /// A basic chat application
    /// </summary>
    public class ChatApp : IChatApp
    {
        private readonly IServiceLocator _serviceLocator;

        public ChatApp(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        protected internal IServiceLocator ServiceLocator
        {
            get { return _serviceLocator; }
        }

        protected internal ICategoryService CategoryService
        {
            get { return ServiceLocator.GetService<ICategoryService>(); }
        }

        protected internal IMessageService MessageService
        {
            get { return ServiceLocator.GetService<IMessageService>(); }
        }
        
        public IEnumerable<ICategory> GetSuggestedCategories(string value)
        {
            return CategoryService.GetSuggestedCategories(value);
        }

        public IEnumerable<ICategory> GetChildCategories(ICategory category)
        {
            return CategoryService.GetChildCategories(category);
        }

        public IEnumerable<ICategory> GetParentCategories(ICategory category)
        {
            return CategoryService.GetParentCategories(category);
        }

        public IEnumerable<IMessage> GetMessages(IEnumerable<ICategory> categories)
        {
            return MessageService.GetMessages(categories);
        }
        
        public void PostMessage(IMessageInfo message)
        {
            MessageService.PostMessage(message);
        }

        public void Dispose()
        {
        }
    }
}
