using System;
using System.Runtime.Serialization;

namespace BLL.Services
{
    [Serializable]
    internal class FolderNotFoundException : Exception
    {
        public FolderNotFoundException()
        {
        }

        public FolderNotFoundException(string message) : base(message)
        {
        }

        public FolderNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FolderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}