using System;
using System.Collections.Generic;

namespace FileStorage.Contracts
{
    public class Folder
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Path { get; set; }
        public ICollection<MyFile> Files { get; set; }
        public ICollection<Folder> Folders { get; set; }
        public Guid? ParentFolderId { get; set; }
        public Folder ParentFolder { get; set; }
    }
}