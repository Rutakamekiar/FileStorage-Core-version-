// <copyright file="ObjectExtensions.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

namespace FileStorage.Implementation.Extensions
{
    public static class ObjectExtensions
    {
        // TODO i want compare any objects with any field
        public static bool IsEmptyObject<T>(this T item)
        {
            return item == null;
        }
    }
}
