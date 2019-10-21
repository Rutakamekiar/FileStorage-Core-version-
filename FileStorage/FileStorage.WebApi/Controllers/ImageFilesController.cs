// <copyright file="ImageFilesController.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;
using FileStorage.Contracts.Interfaces;
using FileStorage.Implementation.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.WebApi.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageFilesController : ControllerBase
    {
        private readonly IImageFileService _imageFileService;
        private readonly ILoggerManager _loggerManager;

        public ImageFilesController(IImageFileService imageFileService,
                                    ILoggerManager loggerManager)
        {
            _imageFileService = imageFileService;
            _loggerManager = loggerManager;
        }

        [HttpPut("{id}/Blackout")]
        public async Task<IActionResult> Get(Guid id)
        {
            await _imageFileService.BlackoutAsync(id);
            return NoContent();
        }
    }
}