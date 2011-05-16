using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chat.Main.Model;

namespace Chat.Main
{
    public interface IChatApp : IDisposable
    {
        /// <summary>
        /// Gets a list of relavant categories based on the search term.  Works like google suggest
        /// </summary>
        /// <example>
        /// 'Cricke' would give you Cricket, Cricketers, etc...
        /// </example>
        /// <param name="value"></param>
        /// <returns></returns>
        IEnumerable<ICategory> GetSuggestedCategories(string value);

        /// <summary>
        /// Returns a set of child categories
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        IEnumerable<ICategory> GetChildCategories(ICategory category);

        /// <summary>
        /// Returns a set of parent categories
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        IEnumerable<ICategory> GetParentCategories(ICategory category);

        /// <summary>
        /// Gets all messages in chronological order that satisfy the specified categories
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        IEnumerable<IMessage> GetMessages(IEnumerable<ICategory> categories);

        /// <summary>
        /// Posts a message
        /// </summary>
        /// <param name="message"></param>
        void PostMessage(IMessage message);
    }
}
