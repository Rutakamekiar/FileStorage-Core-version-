using AutoMapper;
using BLL.DTO;
using WebApi.Models;

namespace WebApi.MappingProfiles
{
    public class FileViewMapProfile : Profile
    {
        public FileViewMapProfile()
        {
            CreateMap<FileDTO, FileView>();
            CreateMap<FileView, FileDTO>();
        }
    }
}