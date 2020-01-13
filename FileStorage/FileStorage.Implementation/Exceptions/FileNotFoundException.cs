// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Globalization;
using FileStorage.Implementation.Resourses.Exceptions;

namespace FileStorage.Implementation.Exceptions
{
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
    }
}