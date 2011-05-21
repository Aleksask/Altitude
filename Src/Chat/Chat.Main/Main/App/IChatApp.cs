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

namespace Chat.Main.App
{
    /// <summary>
    /// Defines a basic chat application facade
    /// </summary>
    public interface IChatApp : IDisposable
    {
        /// <summary>
        /// Gets a list of relavant categories based on the search term.  Works like google suggest
        /// </summary>
        /// <example>
        /// 'Cricke' would give you Cricket, Cricketers, etc...
        /// </example>
        /// <param name="value"></param>
        /// <returns></returns>
        IEnumerable<ICategory> GetSuggestedCategories(string value);

        /// <summary>
        /// Returns a set of child categories
        /// </summary>
        /// <returns></returns>
        IEnumerable<ICategory> GetChildCategories(ICategory category);

        /// <summary>
        /// Returns a set of parent categories
        /// </summary>
        /// <returns></returns>
        IEnumerable<ICategory> GetParentCategories(ICategory category);

        /// <summary>
        /// Gets all messages in chronological order that satisfy the specified categories
        /// </summary>
        /// <returns></returns>
        IEnumerable<IMessage> GetMessages(IEnumerable<ICategory> categories);

        /// <summary>
        /// Posts a message
        /// </summary>
        /// <param name="message"></param>
        void PostMessage(IMessageInfo message);
    }
}
