using System.Collections.Generic;
using AutoMapper;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public ValuesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<FileView>> GetAll()
        {
            return Mapper.Map<List<FileView>>(_fileService.GetAll());
        }
    }
}