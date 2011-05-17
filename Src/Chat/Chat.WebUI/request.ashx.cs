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

using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Services;
using Chat.Main;
using Chat.Main.App;
using Chat.Main.Model;
using Chat.Main.Services;
using Jayrock.Json;

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
        private static readonly object _chatAppLocker = new object();

        private static IChatApp GetChatApp()
        {
            if (_chatApp != null)
                return _chatApp;

            lock (_chatAppLocker)
            {
                if (_chatApp != null)
                    return _chatApp;

                _chatApp = GetChatAppFactory().CreateChatApp();
            }

            return _chatApp;
        }

        private static IChatAppFactory GetChatAppFactory()
        {
            return new BasicChatAppFactory();
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string term = context.Request["term"];
            var categories = GetChatApp().GetSuggestedCategories(term);
            WriteCategories(categories, context.Response.Output);
        }

        private static void WriteCategories(IEnumerable<ICategory> categories, TextWriter writer)
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
