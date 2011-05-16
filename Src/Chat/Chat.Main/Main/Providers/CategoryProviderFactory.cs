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
using System.Linq;
using System.Text;
using Chat.Main.Model;
using Chat.Main.IO;

namespace Chat.Main.Providers
{
    /// <summary>
    /// Defines different types of category providers
    /// </summary>
    public class CategoryProviderFactory
    {
        public static ICategoryProvider CreateFromHardCoded()
        {
            var categoryIndex = new CategoryProvider(new CategoryFactory());
            categoryIndex.AddCategory(new CategoryWithParents(1, "Everything", new long[] { }));

            categoryIndex.AddCategory(new CategoryWithParents(2, "Sport", new long[] { 1 }));
            categoryIndex.AddCategory(new CategoryWithParents(3, "Software", new long[] { 1 }));

            categoryIndex.AddCategory(new CategoryWithParents(4, "Football Teams", new long[] { 2 }));

            categoryIndex.AddCategory(new CategoryWithParents(7, "C#", new long[] { 3 }));
            categoryIndex.AddCategory(new CategoryWithParents(8, "Java", new long[] { 3 }));

            categoryIndex.AddCategory(new CategoryWithParents(5, "Tottenham Hotspurs", new long[] { 4 }));
            categoryIndex.AddCategory(new CategoryWithParents(6, "Chelsea", new long[] { 4 }));
            return categoryIndex;
        }

        public static ICategoryProvider CreateFromWikipedia()
        {
            var categoryIndex = new CategoryProvider(new CategoryFactory());
            using (var categoryReader = new WikipediaCategoryCursor())
                categoryIndex.AddCategories(categoryReader);

            return categoryIndex;
        }
    }
}
