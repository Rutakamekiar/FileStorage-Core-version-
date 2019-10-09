// <copyright file="TxtFilesController.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;
using FileStorage.Contracts.Interfaces;
using FileStorage.Implementation.Interfaces;
using FileStorage.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.WebApi.Controllers
{
    [Route("api/txt")]
    [ApiController]
    public class TxtFilesController : ControllerBase
    {
        private readonly ITxtFileService _txtFileService;
        private readonly ILoggerManager _loggerManager;

        public TxtFilesController(ITxtFileService txtFileService,
                                  ILoggerManager loggerManager)
        {
            _txtFileService = txtFileService;
            _loggerManager = loggerManager;
        }

        [HttpGet("{id}/Symbols")]
        public IActionResult GetSymbols(Guid id)
        {
            return Ok(_txtFileService.GetTxtFileSymbolsCount(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(new TxtResponse
            {
                Text = await _txtFileService.GetTxtFile(id)
            });
        }
    }
}