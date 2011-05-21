using System;
using System.Collections.Generic;

namespace Chat.Main.Collections
{
    public interface IPrefixMatcher<V> where V : class
    {
        String GetPrefix();
        void ResetMatch();
        void BackMatch();
        char LastMatch();
        bool NextMatch(char next);
        List<V> GetPrefixMatches();
        bool IsExactMatch();
        V GetExactMatch();
    }
}
