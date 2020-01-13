﻿// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using FileStorage.Implementation.Resourses.Exceptions;

namespace FileStorage.Implementation.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base(Localization.UserNotFound)
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}