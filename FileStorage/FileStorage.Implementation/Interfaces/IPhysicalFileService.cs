namespace FileStorage.Implementation.Interfaces
{
    public interface IPhysicalFileService
    {
        void CreateFile(string path, byte[] bytes);
        byte[] ReadFile(string path);
        bool CheckFile(string path);
        long GetFileLength(string path);
        void DeleteFile(string path);
        void ReplaceFile(string oldPath, string newPath);
        string ReadAllText(string path);
    }
}
