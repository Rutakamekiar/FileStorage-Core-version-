using System;
using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    [Serializable]
    internal class FolderWrongNameException : Exception
    {
        public FolderWrongNameException()
        {
        }

        public FolderWrongNameException(string message) : base(message)
        {
        }

        public FolderWrongNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FolderWrongNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}