// <copyright file="FolderNotFoundException.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Globalization;
using System.Runtime.Serialization;
using FileStorage.Implementation.Resourses.Exceptions;

namespace FileStorage.Implementation.Exceptions
{
    [Serializable]
    public class FolderNotFoundException : Exception
    {
        public FolderNotFoundException()
        {
        }

        public FolderNotFoundException(string folderId)
            : base(string.Format(CultureInfo.CurrentCulture,
                                 Localization.FolderNotFoundTemplate,
                                 folderId))
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