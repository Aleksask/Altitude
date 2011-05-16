using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Altitude.Collections;

namespace Chat.Main
{
    public class CategoryIndex
    {
        private Dictionary<long, string> _idIndex;
        private Trie<object> _labelIndex;
        private Dictionary<long, List<long>> _isAIndex;
        private Dictionary<long, List<long>> _isAIndexInverse;

        public CategoryIndex(ICategoryReader categoryReader)
        {
            _idIndex = new Dictionary<long, string>();
            _labelIndex = new Trie<object>();
            _isAIndex = new Dictionary<long, List<long>>();
            _isAIndexInverse = new Dictionary<long, List<long>>();

            while (categoryReader.Read())
            {
                var catName = categoryReader.Name.Replace("_", " ");
                _idIndex.Add(categoryReader.Id, catName);
                _labelIndex.Put(catName, categoryReader.Id);

                foreach (var parentId in categoryReader.IsAIds)
                {
                    if (!_isAIndex.ContainsKey(categoryReader.Id))
                        _isAIndex.Add(categoryReader.Id, new List<long>());
                    _isAIndex[categoryReader.Id].Add(parentId);

                    if (!_isAIndexInverse.ContainsKey(parentId))
                        _isAIndexInverse.Add(parentId, new List<long>());
                    _isAIndexInverse[parentId].Add(categoryReader.Id);
                }
            }
        }

        public Dictionary<long, string> IdIndex { get { return _idIndex; } }

        public Trie<object> LabelIndex { get { return _labelIndex; } }

        public Dictionary<long, List<long>> IsAIndex { get { return _isAIndex; } }

        public Dictionary<long, List<long>> IsAIndexInverse { get { return _isAIndexInverse; } }

        public string GetCategory(long id)
        {
            return IdIndex[id];
        }

        public IEnumerable<long> GetChildCategories(long id)
        {
            List<long> values;
            if (_isAIndexInverse.TryGetValue(id, out values))
                return values;

            return new List<long>();
        }

        public IEnumerable<long> GetParentCategories(long id)
        {
            List<long> values;
            if (_isAIndex.TryGetValue(id, out values))
                return values;

            return new List<long>();
        }

        public IEnumerable<long> GetMatches(string value, int count)
        {
            _labelIndex.Matcher.ResetMatch();
            foreach (var chr in value.ToCharArray())
                _labelIndex.Matcher.NextMatch(chr);

            foreach (var item in _labelIndex.Matcher.GetPrefixMatches().Take(count))
                yield return (long) item;
        }
    }
}
