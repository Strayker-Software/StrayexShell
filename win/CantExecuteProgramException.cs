using System;
using System.Runtime.Serialization;

namespace strayex_shell_win
{
    public class CantExecuteProgramException : Exception
    {
        public CantExecuteProgramException()
        {
            
        }

        public CantExecuteProgramException(string message)
            : base(message)
        {

        }

        public CantExecuteProgramException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        protected CantExecuteProgramException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
