using System.Collections.Generic;
using System.Linq;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IFolderService _folderService;

        public ValuesController(IFileService fileService, IUserService userService, IFolderService folderService)
        {
            _fileService = fileService;
            _userService = userService;
            _folderService = folderService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<FileView>> GetAll()
        {
            return AutoMapper.Mapper.Map<List<FileView>>(_fileService.GetAll());
        }
    }
}