﻿//
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
using System.Linq;
using System.Text;
using Chat.Main.Model;
using Chat.Main.Providers;

namespace Chat.Main
{
    /// <summary>
    /// A basic chat application
    /// </summary>
    public class ChatApp : IChatApp
    {
        private ICategoryProvider _categoryProvider;
       
        public ChatApp(ICategoryProvider categoryProvider)
        {
            _categoryProvider = categoryProvider;
        }
        
        public IEnumerable<ICategory> GetSuggestedCategories(string value)
        {
            return _categoryProvider.GetSuggestedCategories(value);
        }

        public IEnumerable<ICategory> GetChildCategories(long categoryId)
        {
            return _categoryProvider.GetChildCategories(categoryId).Select(f => _categoryProvider.GetCategory(f));          
        }

        public IEnumerable<ICategory> GetParentCategories(long categoryId)
        {
            return _categoryProvider.GetParentCategories(categoryId).Select(f => _categoryProvider.GetCategory(f));
        }

        public IEnumerable<IMessage> GetMessages(long[] categoryIds)
        {
            throw new NotImplementedException();
        }

        public void PostMessage(string message, long[] categoryIds)
        {
            throw new NotImplementedException();
        }
        
        public void Dispose()
        {
        }
    }
}