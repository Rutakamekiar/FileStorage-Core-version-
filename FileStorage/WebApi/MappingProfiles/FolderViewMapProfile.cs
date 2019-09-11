using AutoMapper;
using BLL.DTO;
using WebApi.Models;

namespace WebApi.MappingProfiles
{
    public class FolderViewMapProfile : Profile
    {
        public FolderViewMapProfile()
        {
            CreateMap<FolderDto, FolderView>();
            CreateMap<FolderView, FolderDto>();
        }
    }
}