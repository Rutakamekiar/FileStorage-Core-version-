using System;
using System.Threading.Tasks;

namespace FileStorage.Implementation.Interfaces
{
    public interface ITxtFileService
    {
        Task<int> GetTxtFileSymbolsCount(Guid id);

        Task<string> GetTxtFile(Guid id);
    }
}