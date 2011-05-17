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

namespace Chat.Main.IO
{
    /// <summary>
    /// A Cursor is a fast enumerator of data.  The Current item never changes and no object instances are created, instead the values with the object can change.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Cursor<T> : IEnumerable<T>, IDisposable
    {
        private class CursorEnumerator<TInner> : IEnumerator<TInner>
        {
            private readonly Cursor<TInner> _cursor;

            public CursorEnumerator(Cursor<TInner> cursor)
            {
                _cursor = cursor;
            }

            public TInner Current
            {
                get { return _cursor.Value; }
            }
            
            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                return _cursor.Read();
            }

            public void Reset()
            {
                _cursor.Reset();
            }

            public void Dispose()
            {
            }

        }

        private readonly T _cursor;

        protected Cursor(T cursor)
        {
            _cursor = cursor;
        }

        public T Value { get { return _cursor; } }

        public abstract bool Read();
        
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new CursorEnumerator<T>(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
