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
using Chat.Main.Model;
using Chat.Main.Basic;
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
        private static IChatApp _chatApp;

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string term = context.Request["term"];

            if (_chatApp == null)
                _chatApp = CreateChatApp();

            var categories = _chatApp.GetSuggestedCategories(term);
            WriteCategories(categories, context.Response.Output);
        }

        private IChatApp CreateChatApp()
        {
            return new BasicChatApp();
        }

        private void WriteCategories(IEnumerable<ICategory> categories, TextWriter writer)
        {
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                jsonWriter.WriteStartArray();
                foreach (var category in categories)
                {
                    jsonWriter.WriteStartObject();
                    jsonWriter.WriteMember("id");
                    jsonWriter.WriteString(category.Id.ToString());
                    jsonWriter.WriteMember("name");
                    jsonWriter.WriteString(category.Name);
                    jsonWriter.WriteMember("value");
                    jsonWriter.WriteString(category.Name);
                    jsonWriter.WriteEndObject();
                }
                jsonWriter.WriteEndArray();
            }
        }

    }
}
