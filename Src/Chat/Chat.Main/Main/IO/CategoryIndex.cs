using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Altitude.Collections;

namespace Chat.Main.IO
{
    public class CategoryIndex
    {
        private Dictionary<int, string> _idIndex;
        private Trie<object> _labelIndex;
        private Dictionary<int, List<int>> _isAIndex;
        private Dictionary<int, List<int>> _isAIndexInverse;

        public CategoryIndex(ICategoryReader categoryReader)
        {
            _idIndex = new Dictionary<int, string>();
            _labelIndex = new Trie<object>();
            _isAIndex = new Dictionary<int, List<int>>();
            _isAIndexInverse = new Dictionary<int, List<int>>();

            while (categoryReader.Read())
            {
                var catName = categoryReader.Name.Replace("_", " ");
                _idIndex.Add(categoryReader.Id, catName);
                _labelIndex.Put(catName, categoryReader.Id);

                foreach (var parentId in categoryReader.IsAIds)
                {
                    if (!_isAIndex.ContainsKey(categoryReader.Id))
                        _isAIndex.Add(categoryReader.Id, new List<int>());
                    _isAIndex[categoryReader.Id].Add(parentId);

                    if (!_isAIndexInverse.ContainsKey(parentId))
                        _isAIndexInverse.Add(parentId, new List<int>());
                    _isAIndexInverse[parentId].Add(categoryReader.Id);
                }
            }
        }

        public Dictionary<int, string> IdIndex { get { return _idIndex; } }

        public Trie<object> LabelIndex { get { return _labelIndex; } }

        public Dictionary<int, List<int>> IsAIndex { get { return _isAIndex; } }

        public Dictionary<int, List<int>> IsAIndexInverse { get { return _isAIndexInverse; } }

        public string GetCategory(int id)
        {
            return IdIndex[id];
        }

        public IEnumerable<int> GetChildCategories(int id)
        {
            List<int> values;
            if (_isAIndexInverse.TryGetValue(id, out values))
                return values;

            return new List<int>();
        }

        public IEnumerable<int> GetParentCategories(int id)
        {
            List<int> values;
            if (_isAIndex.TryGetValue(id, out values))
                return values;

            return new List<int>();
        }

        public IEnumerable<int> GetMatches(string value, int count)
        {
            _labelIndex.Matcher.ResetMatch();
            foreach (var chr in value.ToCharArray())
                _labelIndex.Matcher.NextMatch(chr);

            foreach (var item in _labelIndex.Matcher.GetPrefixMatches().Take(count))
                yield return (int)item;
        }
    }
}
