using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.MappingProfiles
{
    public sealed class FolderMapProfile : Profile
    {
        public FolderMapProfile()
        {
            CreateMap<FolderDto, UserFolder>();
            CreateMap<UserFolder, FolderDto>();
        }
    }
}