using System;
using Lucidity.Utilities.Contracts.Logging;

namespace Lucidity.Utilities.Logging
{
    public class StubLog : ILog
    {
        public string Add(int level, string message, Exception ex, object objects)
        {
            return null;
        }

        public string Add(int level, string message)
        {
            return null;
        }

        public string Add(int level, string message, Exception ex)
        {
            return null;
        }

        public string Add(int level, string message, object objects)
        {
            return null;
        }

        public string Add(Exception ex)
        {
            return null;
        }

        public string Add(string message)
        {
            return null;
        }

        public string Add(string message, object objects)
        {
            return null;
        }

        public string Add(object objects)
        {
            return null;
        }

        public string Add()
        {
            return null;
        }
    }
}
