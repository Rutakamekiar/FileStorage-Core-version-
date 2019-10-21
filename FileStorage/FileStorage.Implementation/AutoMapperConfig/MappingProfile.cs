// <copyright file="MappingProfile.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using AutoMapper;
using FileStorage.Contracts.DTO;
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