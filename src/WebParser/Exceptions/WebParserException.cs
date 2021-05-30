using System;
using System.Runtime.Serialization;

namespace WebParser.Exceptions
{
    [Serializable]
    public class WebParserException : Exception
    {

        public WebParserException(string message) : base(message)
        {
        }

        protected WebParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
