﻿using System;
using System.Runtime.Serialization;

namespace FileStorage.Implementation.Exceptions
{
    [Serializable]
    internal class FileNotFoundException : Exception
    {
        public FileNotFoundException()
        {
        }

        public FileNotFoundException(string message) : base(message)
        {
        }

        public FileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}