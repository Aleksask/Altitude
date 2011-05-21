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

using System.Collections.Generic;
using Chat.Main.Model;

namespace Chat.Main.Services
{
    /// <summary>
    /// Defines a provider able to lookup category information
    /// </summary>
    public interface ICategoryService : IService
    {
        /// <summary>
        /// Creates new categories based upon the categoryinfo structure
        /// </summary>
        /// <param name="categoryInfo"></param>
        /// <returns></returns>
        IEnumerable<ICategory> CreateCategory(ICategoryInfo categoryInfo);

        /// <summary>
        /// Gets a set of categories that match the specified search value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IEnumerable<ICategory> GetSuggestedCategories(string value);

        /// <summary>
        /// Gets a category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICategory GetCategory(long id);

        /// <summary>
        /// Gets a category by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        bool TryGetCategory(long id, out ICategory category);

        /// <summary>
        /// Gets a category by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ICategory GetCategory(string name);

        /// <summary>
        /// Gets a category by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        bool TryGetCategory(string name, out ICategory category);

        /// <summary>
        /// Gets a categories children
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        IEnumerable<ICategory> GetChildCategories(ICategory category);

        /// <summary>
        /// Gets a categories parents
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        IEnumerable<ICategory> GetParentCategories(ICategory category);
    }
}
