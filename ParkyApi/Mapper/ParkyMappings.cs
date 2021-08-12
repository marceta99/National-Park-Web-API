using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ParkyApi.Models; 
using ParkyApi.Models.Dtos; 

namespace ParkyApi.Mapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap(); 
            CreateMap<Trail, TrailDto>().ReverseMap(); 
            CreateMap<Trail, TrailUpdateDto>().ReverseMap(); 
            CreateMap<Trail, TrailCreateDto>().ReverseMap(); 
            //reverse map add functionality that we can map both ways from model to Dto
            //and also from dto to Model
        }



    }
}
