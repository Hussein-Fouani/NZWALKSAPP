﻿using AutoMapper;
using NZWALKS.Models;
using NZWALKS.Models.DTO;

namespace NZWALKS.Mappings
{
    public class automapping:Profile
    {
        public automapping()
        {
            CreateMap<Regions, RegionDTO>().ReverseMap();

        }
    }
}