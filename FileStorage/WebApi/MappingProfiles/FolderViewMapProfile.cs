using AutoMapper;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.MappingProfiles
{
    public class FolderViewMapProfile : Profile
    {
        public FolderViewMapProfile()
        {
            CreateMap<FolderDTO, FolderView>();
            CreateMap<FolderView, FolderDTO>();
        }
    }
}