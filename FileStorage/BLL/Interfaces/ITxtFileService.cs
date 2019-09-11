namespace BLL.Interfaces
{
    public interface ITxtFileService
    {
        int GetTxtFileSymbolsCount(int id);

        string GetTxtFile(int id);
    }
}