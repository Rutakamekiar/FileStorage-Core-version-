using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.MappingProfiles
{
    public sealed class FolderMapProfile : Profile
    {
        public FolderMapProfile()
        {
            CreateMap<FolderDTO, UserFolder>();
            CreateMap<UserFolder, FolderDTO>();
        }
    }
}