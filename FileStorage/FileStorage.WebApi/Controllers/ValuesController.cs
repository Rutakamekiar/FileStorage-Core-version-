using System.Collections.Generic;
using AutoMapper;
using FileStorage.Implementation.Interfaces;
using FileStorage.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public ValuesController(IFileService fileService,
                                IMapper mapper)
        {
            _fileService = fileService;
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<FileView>> GetAll()
        {
            return _mapper.Map<List<FileView>>(_fileService.GetAll());
        }
    }
}