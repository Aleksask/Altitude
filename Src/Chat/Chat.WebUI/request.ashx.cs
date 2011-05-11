using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Linq;
using Chat.Main;
using Chat.Main.IO;
using Jayrock.Json;
using Jayrock.JsonRpc;
using Jayrock.JsonRpc.Web;

namespace Chat.WebUI
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class request : IHttpHandler 
    {
        private static CategoryIndex _categoryIndex;

        static request()
        {
            using (var file = new FileStream(@"C:\Git\Altitude\Src\Chat\Chat.WebUI\wikipediaOntology.owl", FileMode.Open))
                using (var categoryReader = new RdfCategoryReader(XmlReader.Create(file)))
                    _categoryIndex = new CategoryIndex(categoryReader);
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string term = context.Request["term"];
            var categories = GetCategories(term);
            WriteCategories(categories, context.Response.Output);
        }

        private void WriteCategories(IEnumerable<string> categories, TextWriter writer)
        {
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                jsonWriter.WriteStartArray();
                foreach (var category in categories)
                {
                    jsonWriter.WriteStartObject();
                    jsonWriter.WriteMember("id");
                    jsonWriter.WriteString(category);
                    jsonWriter.WriteMember("name");
                    jsonWriter.WriteString(category);
                    jsonWriter.WriteMember("value");
                    jsonWriter.WriteString(category);
                    jsonWriter.WriteEndObject();
                }
                jsonWriter.WriteEndArray();
            }
        }

        private IEnumerable<string> GetCategories(string term)
        {
            List<string> categoryTerms = Split(term);
            List<int> ids = new List<int>();
            for (int i = 0; i < categoryTerms.Count; i++)
            {
                if (categoryTerms[i] == ".")
                    ids = _categoryIndex.GetChildCategories(ids[0]).ToList();
                else if (categoryTerms[i] == "..")
                    ids = _categoryIndex.GetParentCategories(ids[0]).ToList();
                else
                {
                    if (i == categoryTerms.Count - 1)
                        ids = new List<int>(_categoryIndex.GetMatches(categoryTerms[i], 100));
                    else
                        ids = new List<int>(_categoryIndex.GetMatches(categoryTerms[i], 1));
                }
            }

            foreach (var id in ids)
                yield return _categoryIndex.GetCategory(id);
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

                    sb.Append((Char) chr);
                    lastWasDot = true;
                }
                else
                {
                    if (lastWasDot)
                    {
                        items.Add(sb.ToString());
                        sb = new StringBuilder();
                    }

                    sb.Append((Char) chr);
                    lastWasDot = false;
                }

                chr = reader.Read();
            } while (chr != -1);

            items.Add(sb.ToString());

            return items;
        }
    }
}
