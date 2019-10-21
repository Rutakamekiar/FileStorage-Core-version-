// <copyright file="TxtFileService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FileStorage.Contracts.DTO;
using FileStorage.Implementation.Exceptions;
using FileStorage.Implementation.Interfaces;

namespace FileStorage.Implementation.Services
{
    public class TxtFileService : ITxtFileService
    {
        private static readonly IList<string> _extensions =
            new ReadOnlyCollection<string>(
                new List<string>
                {
                    "txt", "docx"
                });

        private readonly IFileService _fileService;
        private readonly IPhysicalFileService _physicalFileService;

        public TxtFileService(IFileService fileService, IPhysicalFileService physicalFileService)
        {
            _fileService = fileService;
            _physicalFileService = physicalFileService;
        }

        public async Task<int> GetTxtFileSymbolsCountAsync(Guid id)
        {
            var file = await _fileService.GetByIdAsync(id);
            if (CheckType(file))
            {
                throw new WrongTypeException();
            }

            var allText = _physicalFileService.ReadAllText(await _fileService.ReturnFullPathAsync(file));
            return allText.Length;
        }

        public async Task<string> GetTxtFile(Guid id)
        {
            var fileDto = await _fileService.GetByIdAsync(id);
            if (CheckType(fileDto))
            {
                throw new WrongTypeException();
            }

            var allText = _physicalFileService.ReadAllText(await _fileService.ReturnFullPathAsync(fileDto));
            return allText;
        }

        private static bool CheckType(MyFile file)
        {
            return !_extensions.Contains(file.Name.Split('.').Last());
        }
    }
}