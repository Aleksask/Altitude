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

namespace Chat.Main.Services
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, IService> _services;

        public ServiceLocator()
        {
            _services = new Dictionary<Type, IService>();
        }

        public void RegisterService<T>(T service) where T : IService
        {
            _services.Add(typeof(T), service);
        }

        public bool TryGetService<T>(out T service) where T : IService
        {
            IService value;
            if (_services.TryGetValue(typeof(T), out value))
            {
                service = (T)value;
                return true;
            }
            service = default(T);
            return false;
        }

        public T GetService<T>() where T : IService
        {
            T service;
            if (TryGetService<T>(out service))
                return service;

            throw new ServiceNotFoundException(string.Format("'{0}' service not found",typeof(T).Name));
        }
    }
}
