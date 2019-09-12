using System;
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

        public TxtFilesController(ITxtFileService txtFileService)
        {
            _txtFileService = txtFileService;
        }

        //Ok
        [HttpGet]
        [Route("{id}/Symbols")]
        public IActionResult GetSymbols(Guid id)
        {
            return Ok(_txtFileService.GetTxtFileSymbolsCount(id));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(new TxtResponse(_txtFileService.GetTxtFile(id)));
        }
    }
}