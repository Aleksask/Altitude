using System.Collections.Generic;
using System.Linq;

namespace Chat.Main.Model
{
    public class CategoryInfo : ICategoryInfo
    {
        private readonly string _name;
        private readonly IList<ICategoryInfo> _children;
        private readonly IList<ICategoryInfo> _parents;

        public CategoryInfo(string name, params ICategoryInfo[] categoriesInfo)
        {
            _name = name;
            _parents = new List<ICategoryInfo>();
            _children = categoriesInfo.ToList();

            foreach (var child in _children)
                child.Parents.Add(this);
        }

        public string Name
        {
            get { return _name; }
        }

        public IList<ICategoryInfo> Children
        {
            get { return _children; }
        }

        public IList<ICategoryInfo> Parents
        {
            get { return _parents; }
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}