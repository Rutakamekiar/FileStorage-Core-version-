// <copyright company="THTR Development Ltd.">
//     Confidential and Proprietary
//     Copyright 2018 THTR Development Ltd.
//     ALL RIGHTS RESERVED.
// </copyright>

using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FileStorage.WebApi.Swagger
{
    public class SecurityRequirementsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Security = new List<IDictionary<string, IEnumerable<string>>>()
            {
                new Dictionary<string, IEnumerable<string>>()
                {
                    { "Bearer", System.Array.Empty<string>() }
                }
            };
        }
    }
}