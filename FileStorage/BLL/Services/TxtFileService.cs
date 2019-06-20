using BLL.DTO;
using BLL.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace BLL.Services
{
    public class TxtFileService : ITxtFileService
    {
        private readonly IFileService _fileService;
        private static readonly IList<string> _extensions = new ReadOnlyCollection<string>(new List<string> { "txt", "docx" });

        public TxtFileService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public int GetTxtFileSymbolsCount(int id)
        {
            FileDTO fileDTO = _fileService.Get(id);
            if (CheckType(fileDTO))
            {
                throw new WrongTypeException("Error type!");
            }
            string allText = File.ReadAllText(_fileService.ReturnFullPath(fileDTO));
            return allText.Length;
        }

        public string GetTxtFile(int id)
        {
            FileDTO fileDTO = _fileService.Get(id);
            if (CheckType(fileDTO))
            {
                throw new WrongTypeException("Error type!");
            }
            string allText = File.ReadAllText(_fileService.ReturnFullPath(fileDTO));
            return allText;
        }

        private static bool CheckType(FileDTO fileDTO)
        {
            return !_extensions.Contains(fileDTO.Name.Split('.').Last());
        }
    }
}