using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
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
        public IActionResult GetSymbols(int id)
        {
            return Ok(_txtFileService.GetTxtFileSymbolsCount(id));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new TxtResponse(_txtFileService.GetTxtFile(id)));
        }
    }
}