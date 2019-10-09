// <copyright file="FileNotFoundException.cs" company="Kovalov Systems">
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
    public class FileNotFoundException : Exception
    {
        public FileNotFoundException()
        {
        }

        public FileNotFoundException(string fileId)
            : base(string.Format(CultureInfo.CurrentCulture,
                                 Localization.FileNotFoundTemplate,
                                 fileId))
        {
        }

        public FileNotFoundException(string fileId, Exception innerException)
            : base(string.Format(CultureInfo.CurrentCulture,
                                 Localization.FileNotFoundTemplate,
                                 fileId),
                   innerException)
        {
        }

        protected FileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}