using AutoMapper;
using BEApi.Dtos;
using BEApi.Model;

namespace BEApi.DiaryProfiles
{
    public class DiaryProfiles : Profile
    {
        public DiaryProfiles()
        {
            CreateMap<DiaryDetails, DiaryDetailDto>();
            CreateMap<Diary, DiaryDto>();
        }
    }
}