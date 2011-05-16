using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Linq;
using Altitude.Collections;
using NUnit.Framework;
using Chat.Main;
using Chat.Main.IO;
using Chat.Main.Model;
using Chat.Main.Providers;

namespace ChatTests.Main
{
    [TestFixture]
	public class WikiTest
	{
        [Test]
		public void Test()
		{
            var categoryIndex = GetCategoryIndex();
            foreach (var category in categoryIndex.GetSuggestedCategories("Comp").Take(10))
			{
                Debug.WriteLine(category.Name);

                foreach (var subItem in categoryIndex.GetChildCategories(category.Id))
                    Debug.WriteLine(category.Name + "." + categoryIndex.GetCategory(subItem).Name);

                foreach (var subItem in categoryIndex.GetParentCategories(category.Id))
                    Debug.WriteLine(category.Name + ".." + categoryIndex.GetCategory(subItem).Name);
				
                Debug.WriteLine("");
			}
		}

        private ICategoryProvider GetCategoryIndex()
        {
            var index = new CategoryProvider(new CategoryFactory());
            using (var categoryReader = new WikipediaCategoryCursor())
                index.AddCategories(categoryReader);

            return index;
        }
	}
}

