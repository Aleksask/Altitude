using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Chat.Main.IO
{
    public class RdfCategoryReader : ICategoryReader
    {
        private static string _classString = @"http://www.eml-research.de/WikipediaOntology/Class#_";
        private static string _individualString = @"http://www.eml-research.de/WikipediaOntology/Individual#:_";
        private XmlReader _reader;
        private int _id;
        private string _name;
        private List<long> _isAIds;

        public RdfCategoryReader(XmlReader reader)
        {
            _reader = reader;
            _isAIds = new List<long>();
            _reader.MoveToContent();
        }

        public long Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public IList<long> IsAIds
        {
            get { return _isAIds; }
        }

        public bool Read()
        {
            while (_reader.Read())
            {
                if (_reader.IsStartElement() && _reader.Name == "rdf:Description")
                {
                    _id = -1;
                    _name = null;
                    _isAIds.Clear();

                    _reader.MoveToAttribute("rdf:about");
                    if (_reader.Value.StartsWith(_classString))
                        _id = int.Parse(_reader.Value.Substring(_classString.Length));
                    else if (_reader.Value.StartsWith(_individualString))
                        _id = int.Parse(_reader.Value.Substring(_individualString.Length));
                    _reader.MoveToElement();

                    while (_reader.Read())
                    {
                        if (_reader.IsStartElement() && _reader.Name == "rdfs:label")
                        {
                            _reader.Read();
                            _name = _reader.Value;
                            break;
                        }
                        else if (_reader.IsStartElement() && _reader.Name == "rdfs:subClassOf")
                        {
                            _reader.MoveToAttribute("rdf:resource");
                            if (_reader.Value.StartsWith(_classString))
                            {
                                var parentId = int.Parse(_reader.Value.Substring(_classString.Length));
                                _isAIds.Add(parentId);
                            }
                        }
                        else if (_reader.IsStartElement() && _reader.Name == "rdf:type")
                        {
                            _reader.MoveToAttribute("rdf:resource");
                            if (_reader.Value.StartsWith(_classString))
                            {
                                var parentId = int.Parse(_reader.Value.Substring(_classString.Length));
                                _isAIds.Add(parentId);
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
