// <copyright file="WrongTypeException.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Runtime.Serialization;
using FileStorage.Implementation.Resourses.Exceptions;

namespace FileStorage.Implementation.Exceptions
{
    [Serializable]
    public class WrongTypeException : Exception
    {
        public WrongTypeException() : base(Localization.ErrorType)
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