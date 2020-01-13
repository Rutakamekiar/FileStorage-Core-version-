// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;
using FileStorage.Contracts.Responses;
using FileStorage.Implementation.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileStorage.WebApi.Controllers
{
    [Route("api/txt")]
    [ApiController]
    public class TxtFilesController : ControllerBase
    {
        private readonly ITxtFileService _txtFileService;
        private readonly ILogger<TxtFilesController> _logger;

        public TxtFilesController(ITxtFileService txtFileService, ILogger<TxtFilesController> logger)
        {
            _txtFileService = txtFileService;
            _logger = logger;
        }

        [HttpGet("{id}/Symbols")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSymbols(Guid id)
        {
            return Ok(await _txtFileService.GetTxtFileSymbolsCountAsync(id));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(new TxtResponse
            {
                Text = await _txtFileService.GetTxtFileAsync(id)
            });
        }
    }
}