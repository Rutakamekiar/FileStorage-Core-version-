using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace WebApi.Controllers
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

        //Ok
        [HttpPut]
        [Route("{id}/Blackout")]
        public IActionResult Get(int id)
        {
            _imageFileService.Blackout(id);
            return NoContent();
        }
    }
}