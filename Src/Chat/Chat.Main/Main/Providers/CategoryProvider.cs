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
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Altitude.Collections;
using Chat.Main.IO;
using Chat.Main.Model;

namespace Chat.Main.Providers
{
    /// <summary>
    /// Provides a fast lookup of category information
    /// </summary>
    public class CategoryProvider : ICategoryProvider
    {
        private ICategoryFactory _categoryFactory;
        private Dictionary<long, ICategory> _idIndex;
        private Trie<object> _labelIndex;
        private Dictionary<long, List<long>> _isAIndex;
        private Dictionary<long, List<long>> _isAIndexInverse;

        public CategoryProvider(ICategoryFactory categoryFactory)
        {
            _categoryFactory = categoryFactory;
            _idIndex = new Dictionary<long, ICategory>();
            _labelIndex = new Trie<object>();
            _isAIndex = new Dictionary<long, List<long>>();
            _isAIndexInverse = new Dictionary<long, List<long>>();
        }
        
        public void AddCategories(IEnumerable<ICategoryWithParentCategories> categories)
        {
            foreach (var category in categories)
                AddCategory(category);
        }

        public void AddCategory(ICategoryWithParentCategories category)
        {
            _idIndex.Add(category.Id, _categoryFactory.CreateCategory(category.Id, category.Name));
            _labelIndex.Put(category.Name, category.Id);

            foreach (var parentId in category.ParentIds)
            {
                if (!_isAIndex.ContainsKey(category.Id))
                    _isAIndex.Add(category.Id, new List<long>());
                _isAIndex[category.Id].Add(parentId);

                if (!_isAIndexInverse.ContainsKey(parentId))
                    _isAIndexInverse.Add(parentId, new List<long>());
                _isAIndexInverse[parentId].Add(category.Id);
            }
        }

        private Dictionary<long, ICategory> IdIndex { get { return _idIndex; } }

        private Trie<object> LabelIndex { get { return _labelIndex; } }

        private Dictionary<long, List<long>> IsAIndex { get { return _isAIndex; } }

        private Dictionary<long, List<long>> IsAIndexInverse { get { return _isAIndexInverse; } }

        private IEnumerable<long> GetMatches(string value)
        {
            _labelIndex.Matcher.ResetMatch();
            foreach (var chr in value.ToCharArray())
                _labelIndex.Matcher.NextMatch(chr);

            foreach (var item in _labelIndex.Matcher.GetPrefixMatches())
                yield return (long)item;
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

        public ICategory GetCategory(long id)
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
                
        public IEnumerable<ICategory> GetSuggestedCategories(string value)
        {
            // TODO : Needs rewrite
            List<string> categoryTerms = Split(value);
            List<long> ids = new List<long>();
            for (int i = 0; i < categoryTerms.Count; i++)
            {
                if (categoryTerms[i] == ".")
                    ids = GetChildCategories(ids[0]).ToList();
                else if (categoryTerms[i] == "..")
                    ids = GetParentCategories(ids[0]).ToList();
                else
                {
                    if (i == categoryTerms.Count - 1)
                        ids = new List<long>(GetMatches(categoryTerms[i]).Take(100));
                    else
                        ids = new List<long>(GetMatches(categoryTerms[i]).Take(1));
                }
            }

            foreach (var id in ids)
                yield return GetCategory(id);
        }
    }
}
