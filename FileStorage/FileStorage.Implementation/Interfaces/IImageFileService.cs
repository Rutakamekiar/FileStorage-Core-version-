using System;
using System.Threading.Tasks;

namespace FileStorage.Implementation.Interfaces
{
    public interface IImageFileService
    {
        Task Blackout(Guid id);
    }
}