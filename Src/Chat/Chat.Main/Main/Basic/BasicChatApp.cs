using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Chat.Main.Model;
using Chat.Main.IO;

namespace Chat.Main.Basic
{
    public class BasicChatApp : IChatApp
    {
        private CategoryIndex _categoryIndex;
        private ICategoryFactory _categoryFactory;
        private IMessageFactory _messageFactory;

        public BasicChatApp()
        {
            _categoryFactory = new BasicCategoryFactory();
            _messageFactory = new BasicMessageFactory();
            _categoryIndex = GetCategoryIndex();
        }

        private CategoryIndex GetCategoryIndex()
        {
            using (var stream = GetType().Assembly.GetManifestResourceStream("Chat.Main.Resources.wikipediaOntology.owl"))
                using (var categoryReader = new RdfCategoryReader(XmlReader.Create(stream)))
                    return new CategoryIndex(categoryReader);
        }

        public IEnumerable<ICategory> GetSuggestedCategories(string term)
        {
            List<string> categoryTerms = Split(term);
            List<long> ids = new List<long>();
            for (int i = 0; i < categoryTerms.Count; i++)
            {
                if (categoryTerms[i] == ".")
                    ids = _categoryIndex.GetChildCategories(ids[0]).ToList();
                else if (categoryTerms[i] == "..")
                    ids = _categoryIndex.GetParentCategories(ids[0]).ToList();
                else
                {
                    if (i == categoryTerms.Count - 1)
                        ids = new List<long>(_categoryIndex.GetMatches(categoryTerms[i], 100));
                    else
                        ids = new List<long>(_categoryIndex.GetMatches(categoryTerms[i], 1));
                }
            }

            foreach (var id in ids)
                yield return _categoryFactory.CreateCategory(id, _categoryIndex.GetCategory(id));
        }

        private static List<string> Split(string term)
        {
            var items = new List<string>();
            var reader = new StringReader(term);
            StringBuilder sb = new StringBuilder();

            bool lastWasDot = false;
            int chr = reader.Read();
            do
            {
                if (chr == Char.Parse("."))
                {
                    if (!lastWasDot)
                    {
                        items.Add(sb.ToString());
                        sb = new StringBuilder();
                    }

                    sb.Append((Char)chr);
                    lastWasDot = true;
                }
                else
                {
                    if (lastWasDot)
                    {
                        items.Add(sb.ToString());
                        sb = new StringBuilder();
                    }

                    sb.Append((Char)chr);
                    lastWasDot = false;
                }

                chr = reader.Read();
            } while (chr != -1);

            items.Add(sb.ToString());

            return items;
        }
        
        public IEnumerable<IMessage> GetMessages(IEnumerable<ICategory> categories)
        {
            throw new NotImplementedException();
        }

        public void PostMessage(IMessage message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICategory> GetChildCategories(ICategory category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICategory> GetParentCategories(ICategory category)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
