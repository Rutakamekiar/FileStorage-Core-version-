using System;

namespace FileStorage.Implementation.Interfaces
{
    public interface ITxtFileService
    {
        int GetTxtFileSymbolsCount(Guid id);

        string GetTxtFile(Guid id);
    }
}