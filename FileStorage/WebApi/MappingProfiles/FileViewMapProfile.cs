using AutoMapper;
using BLL.DTO;
using WebApi.Models;

namespace WebApi.MappingProfiles
{
    public class FileViewMapProfile : Profile
    {
        public FileViewMapProfile()
        {
            CreateMap<FileDto, FileView>();
            CreateMap<FileView, FileDto>();
        }
    }
}