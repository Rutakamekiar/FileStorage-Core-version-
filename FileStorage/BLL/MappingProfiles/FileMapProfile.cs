using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.MappingProfiles
{
    public sealed class FileMapProfile : Profile
    {
        public FileMapProfile()
        {
            CreateMap<FileDTO, UserFile>();
            CreateMap<UserFile, FileDTO>();
        }
    }
}