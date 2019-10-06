﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileStorage.Implementation.Exceptions;
using FileStorage.Implementation.Interfaces;

namespace FileStorage.Implementation.Services
{
    public class ImageFileService : IImageFileService
    {
        private static readonly IList<string> Extensions =
            new ReadOnlyCollection<string>(new List<string> {"img", "bmp"});

        private readonly IFileService _fileService;

        public ImageFileService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task Blackout(Guid id)
        {
            var file = _fileService.Get(id);
            if (!Extensions.Contains(file.Name.Split('.').Last())) throw new WrongTypeException("Error type!");
            var path = await _fileService.ReturnFullPathAsync(file);
            var newName = path.Split('.').First() + "-copy.bmp";
            using (var image = (Bitmap)Image.FromFile(path))
            {
                for (var y = 0; y < image.Height; ++y)
                for (var x = 0; x < image.Width; ++x)
                {
                    var c = image.GetPixel(x, y);
                    image.SetPixel(x, y, Color.FromArgb(c.A, (byte) (c.R - 120), c.G, c.B));
                }

                image.Save(newName, ImageFormat.Bmp);
            }

            File.Delete(path);
            File.Move(newName, path);
        }
    }
}