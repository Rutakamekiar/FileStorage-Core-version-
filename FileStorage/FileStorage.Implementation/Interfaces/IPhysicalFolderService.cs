namespace FileStorage.Implementation.Interfaces
{
    public interface IPhysicalFolderService
    {
        void CreateFolder(string path);
        void DeleteFolder(string path);
        bool CheckFolder(string path);
        string[] GetFolders(string path);
        string[] GetFolderFiles(string path);
        void ReplaceFolder(string oldPath, string newPath);
    }
}
