using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;

namespace BLL.Services
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

        public int GetTxtFileSymbolsCount(int id)
        {
            var fileDto = _fileService.Get(id);
            if (CheckType(fileDto)) throw new WrongTypeException("Error type!");
            var allText = File.ReadAllText(_fileService.ReturnFullPath(fileDto));
            return allText.Length;
        }

        public string GetTxtFile(int id)
        {
            var fileDto = _fileService.Get(id);
            if (CheckType(fileDto)) throw new WrongTypeException("Error type!");
            var allText = File.ReadAllText(_fileService.ReturnFullPath(fileDto));
            return allText;
        }

        private static bool CheckType(FileDto fileDto)
        {
            return !Extensions.Contains(fileDto.Name.Split('.').Last());
        }
    }
}