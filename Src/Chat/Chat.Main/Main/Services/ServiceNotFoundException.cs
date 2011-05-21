using System;

namespace Chat.Main.Services
{
    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException(string message)
            : base(message)
        {
        }
    }
}