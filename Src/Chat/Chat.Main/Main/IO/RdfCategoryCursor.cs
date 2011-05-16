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
using System.Xml;
using Chat.Main.Model;

namespace Chat.Main.IO
{
    /// <summary>
    /// Reads categories from an Rdf xml file
    /// </summary>
    public class RdfCategoryCursor : Cursor<ICategoryWithParentCategories>
    {
        class CategoryWithParentCategoriesCursor : ICategoryWithParentCategories
        {
            private  IList<long> _parentIds;
            private  long _id;
            private  string _name;

            public CategoryWithParentCategoriesCursor()
            {
                _id = 0;
                _name = "";
                _parentIds = new List<long>();
            }

            public long  Id
            {
	            get { return _id; }
                set { _id = value; }
            }

            public string  Name
            {
	            get { return _name; }
                set { _name = value; }
            }

            public IList<long> ParentIds
            {
                get { return _parentIds; }
            }

        }

        private static string _classString = @"http://www.eml-research.de/WikipediaOntology/Class#_";
        private static string _individualString = @"http://www.eml-research.de/WikipediaOntology/Individual#:_";
        private XmlReader _reader;

        public RdfCategoryCursor(XmlReader reader)
            : base(new CategoryWithParentCategoriesCursor())
        {
            _reader = reader;
            _reader.MoveToContent();
        }

        private new CategoryWithParentCategoriesCursor Value 
        {
            get { return base.Value as CategoryWithParentCategoriesCursor; }
        }

        public override bool Read()
        {
            while (_reader.Read())
            {
                if (_reader.IsStartElement() && _reader.Name == "rdf:Description")
                {
                    Value.Id = 0;
                    Value.Name = "";
                    Value.ParentIds.Clear();

                    _reader.MoveToAttribute("rdf:about");
                    if (_reader.Value.StartsWith(_classString))
                        Value.Id = int.Parse(_reader.Value.Substring(_classString.Length));
                    else if (_reader.Value.StartsWith(_individualString))
                        Value.Id = int.Parse(_reader.Value.Substring(_individualString.Length));
                    _reader.MoveToElement();

                    while (_reader.Read())
                    {
                        if (_reader.IsStartElement() && _reader.Name == "rdfs:label")
                        {
                            _reader.Read();
                            Value.Name = _reader.Value.Replace("_", " ");
                            break;
                        }
                        else if (_reader.IsStartElement() && _reader.Name == "rdfs:subClassOf")
                        {
                            _reader.MoveToAttribute("rdf:resource");
                            if (_reader.Value.StartsWith(_classString))
                            {
                                var parentId = int.Parse(_reader.Value.Substring(_classString.Length));
                                Value.ParentIds.Add(parentId);
                            }
                        }
                        else if (_reader.IsStartElement() && _reader.Name == "rdf:type")
                        {
                            _reader.MoveToAttribute("rdf:resource");
                            if (_reader.Value.StartsWith(_classString))
                            {
                                var parentId = int.Parse(_reader.Value.Substring(_classString.Length));
                                Value.ParentIds.Add(parentId);
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public void Close()
        {
            if (_reader != null)
                _reader.Close();
        }

        public void Dispose()
        {
            Close();
        }
    }
}
