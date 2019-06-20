using System.Collections.Generic;
using System.IO;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface ITxtFileService
    {
        int GetTxtFileSymbolsCount(int id);

        string GetTxtFile(int id);
    }
}