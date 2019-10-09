﻿// <copyright file="ImageFilesController.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using FileStorage.Implementation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.WebApi.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageFilesController : ControllerBase
    {
        private readonly IImageFileService _imageFileService;

        public ImageFilesController(IImageFileService imageFileService)
        {
            _imageFileService = imageFileService;
        }

        [HttpPut("{id}/Blackout")]
        public IActionResult Get(Guid id)
        {
            _imageFileService.Blackout(id);
            return NoContent();
        }
    }
}