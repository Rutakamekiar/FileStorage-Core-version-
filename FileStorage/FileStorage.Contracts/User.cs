using System;

namespace FileStorage.Contracts
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public long MemorySize { get; set; }
    }
}