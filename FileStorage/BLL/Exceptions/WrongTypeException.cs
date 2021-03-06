﻿using System;
using System.Runtime.Serialization;

namespace BLL.Services
{
    [Serializable]
    internal class WrongTypeException : Exception
    {
        public WrongTypeException()
        {
        }

        public WrongTypeException(string message) : base(message)
        {
        }

        public WrongTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}