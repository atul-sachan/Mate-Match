using System.Linq;
using AutoMapper;
using Mate_Match_API.DTOs;
using Mate_Match_API.Data.Models;

namespace Mate_Match_API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest=> dest.PhotoUrl, opt=>{
                    opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => opt.MapFrom<AgeResolver<UserForListDto>>());

            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest=> dest.PhotoUrl, opt=>{
                    opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => opt.MapFrom<AgeResolver<UserForDetailedDto>>());
                
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotosForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForRegisterDto, User>();
        }
    }
}