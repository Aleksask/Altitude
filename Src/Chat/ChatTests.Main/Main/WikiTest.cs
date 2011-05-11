using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Altitude.Collections;
using NUnit.Framework;
using Chat.Main;
using Chat.Main.IO;

namespace ChatTests.Main
{
    [TestFixture]
	public class WikiTest
	{
        [Test]
		public void Test()
		{
            CategoryIndex categoryIndex;
            using (var file = new FileStream("../../wikipediaOntology.owl", FileMode.Open))
            {
                using (var categoryReader = new RdfCategoryReader(XmlReader.Create(file)))
                {
                    categoryIndex = new CategoryIndex(categoryReader);
                }
            }

            categoryIndex.LabelIndex.Matcher.ResetMatch();
            categoryIndex.LabelIndex.Matcher.NextMatch('C');
            categoryIndex.LabelIndex.Matcher.NextMatch('o');
            categoryIndex.LabelIndex.Matcher.NextMatch('m');
            categoryIndex.LabelIndex.Matcher.NextMatch('p');

            foreach (var item in categoryIndex.LabelIndex.Matcher.GetPrefixMatches())
			{
				var id = (int) item;
                var name = categoryIndex.IdIndex[id].Replace("_", " ");
				Debug.WriteLine(name);

                if (categoryIndex.IsAIndexInverse.ContainsKey(id))
				{
                    foreach (var subItem in categoryIndex.IsAIndexInverse[id])
                        Debug.WriteLine(name + "." + categoryIndex.IdIndex[subItem].Replace("_", " "));
				}

                if (categoryIndex.IsAIndex.ContainsKey(id))
				{
                    foreach (var subItem in categoryIndex.IsAIndex[id])
                        Debug.WriteLine(name + ".." + categoryIndex.IdIndex[subItem].Replace("_", " "));
				}

                Debug.WriteLine("");
			}
		}
	}
}

