using AutoMapper;
using FileStorage.Contracts;
using FileStorage.Implementation.DataAccess.Entities;

namespace FileStorage.Implementation.AutoMapperConfig
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MyFile, FileEntity>().ReverseMap();
            CreateMap<Folder, FolderEntity>().ReverseMap();
            CreateMap<User, UserEntity>().ReverseMap();
        }
    }
}