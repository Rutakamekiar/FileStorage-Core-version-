using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileStorage.Contracts;
using FileStorage.Implementation.Exceptions;
using FileStorage.Implementation.Interfaces;

namespace FileStorage.Implementation.Services
{
    public class TxtFileService : ITxtFileService
    {
        private static readonly IList<string> Extensions =
            new ReadOnlyCollection<string>(new List<string> {"txt", "docx"});

        private readonly IFileService _fileService;

        public TxtFileService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<int> GetTxtFileSymbolsCount(Guid id)
        {
            var fileDto = _fileService.Get(id);
            if (CheckType(fileDto)) throw new WrongTypeException("Error type!");
            var allText = File.ReadAllText(await _fileService.ReturnFullPathAsync(fileDto));
            return allText.Length;
        }

        public async Task<string> GetTxtFile(Guid id)
        {
            var fileDto = _fileService.Get(id);
            if (CheckType(fileDto)) throw new WrongTypeException("Error type!");
            var allText = File.ReadAllText(await _fileService.ReturnFullPathAsync(fileDto));
            return allText;
        }

        private static bool CheckType(MyFile fileDto)
        {
            return !Extensions.Contains(fileDto.Name.Split('.').Last());
        }
    }
}