// <copyright file="FolderWrongNameException.cs" company="Kovalov Systems">
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
    public class FolderWrongNameException : Exception
    {
        public FolderWrongNameException() : base(Localization.FolderWrongName)
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