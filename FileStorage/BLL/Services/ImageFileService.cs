using BLL.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace BLL.Services
{
    public class ImageFileService : IImageFileService
    {
        private readonly IFileService _fileService;
        private static readonly IList<string> _extensions = new ReadOnlyCollection<string>(new List<string> { "img", "bmp" });

        public ImageFileService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void Blackout(int id)
        {
            var file = _fileService.Get(id);
            if (!_extensions.Contains(file.Name.Split('.').Last()))
            {
                throw new WrongTypeException("Error type!");
            }
            string path = _fileService.ReturnFullPath(file);
            string newName = path.Split('.').First() + "-copy.bmp";
            using (var _image = (Bitmap)Image.FromFile(path))
            {
                for (int y = 0; y < _image.Height; ++y)
                {
                    for (int x = 0; x < _image.Width; ++x)
                    {
                        Color c = _image.GetPixel(x, y);
                        _image.SetPixel(x, y, Color.FromArgb(c.A, (byte)(c.R - 120), c.G, c.B));
                    }
                }
                _image.Save(newName, ImageFormat.Bmp);
            }
            File.Delete(path);
            File.Move(newName, path);
        }
    }
}