using AutoMapper;
using NZWALKS.Models;
using NZWALKS.Models.DTO;
using NZWALKS.Models.WalksDTO;

namespace NZWALKS.Mappings
{
    public class Automapping:Profile
    {
        public Automapping()
        {
            CreateMap<Regions, RegionDTO>().ReverseMap();  
            CreateMap<AddRegionDTO, Regions>().ReverseMap();
            CreateMap<UpdateRegionDTO, Regions>().ReverseMap();
            CreateMap<AddWalkDTO,Walks>().ReverseMap();
            CreateMap<WalkDTO,Walks>().ReverseMap();
            CreateMap<Difficulty,DifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalkDTO,Walks>().ReverseMap();
            CreateMap<Image,ImageUploadRequestDto>().ReverseMap();
        }
    }
}
