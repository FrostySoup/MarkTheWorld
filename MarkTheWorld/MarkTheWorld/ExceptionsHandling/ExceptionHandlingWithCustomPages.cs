using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MarkTheWorld.ExceptionsHandling
{
    public class ExceptionHandlingWithCustomPages : Exception
    {
        public ExceptionHandlingWithCustomPages()
        {
        }

        public ExceptionHandlingWithCustomPages(string message)
            : base(message)
        {
        }

        public ExceptionHandlingWithCustomPages(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ExceptionHandlingWithCustomPages(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}