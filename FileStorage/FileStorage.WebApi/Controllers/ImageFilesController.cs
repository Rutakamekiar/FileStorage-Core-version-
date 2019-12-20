// <copyright file="ImageFilesController.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;
using FileStorage.Implementation.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileStorage.WebApi.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageFilesController : ControllerBase
    {
        private readonly IImageFileService _imageFileService;
        private readonly ILogger<ImageFilesController> _logger;

        public ImageFilesController(IImageFileService imageFileService,
                                    ILogger<ImageFilesController> logger)
        {
            _imageFileService = imageFileService;
            _logger = logger;
        }

        [HttpPut("{id}/Blackout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(Guid id)
        {
            await _imageFileService.BlackoutAsync(id);
            return NoContent();
        }
    }
}