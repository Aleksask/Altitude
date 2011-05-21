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
using Chat.Main.Services.Factories;

namespace Chat.Main.Services
{
    public class MessageService : ServiceBase, IMessageService
    {
        private readonly IDictionary<long, IMessage> _messagesById;
        private readonly IDictionary<long, IList<long>> _messageIdsByCategoryId;

        public MessageService(IServiceLocator serviceLocator) 
            : base(serviceLocator)
        {
            _messagesById = new Dictionary<long, IMessage>();
            _messageIdsByCategoryId = new Dictionary<long, IList<long>>();
        }

        protected IMessageFactoryService MessageFactoryService
        {
            get { return ServiceLocator.GetService<IMessageFactoryService>(); }
        }

        protected ICategoryService CategoryService
        {
            get { return ServiceLocator.GetService<ICategoryService>(); }
        }

        public IEnumerable<IMessage> GetMessages(IEnumerable<ICategory> categories)
        {
            throw new NotImplementedException();
        }

        public void PostMessage(IMessageInfo message)
        {
            var msg = MessageFactoryService.CreateMessage(message);
            _messagesById.Add(msg.Id, msg);

            // Post the message to each category it is tagged with
            foreach (var categoryId in msg.CategoryIds)
            {
                IList<long> msgIds;
                if (!_messageIdsByCategoryId.TryGetValue(categoryId, out msgIds))
                    msgIds = new List<long>();

                msgIds.Add(msg.Id);
            }
        }

        public void PostMessages(IEnumerable<IMessageInfo> messages)
        {
            foreach (var message in messages)
                PostMessage(message);
        }
    }
}