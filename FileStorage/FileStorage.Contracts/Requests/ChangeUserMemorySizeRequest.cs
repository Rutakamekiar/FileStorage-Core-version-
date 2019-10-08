namespace FileStorage.Contracts.Requests
{
    public class ChangeUserMemorySizeRequest
    {
        public string UserId { get; set; }
        public long MemorySize { get; set; }
    }
}
