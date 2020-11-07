using AutoMapper;
using MedicineTrackingSystem.API.Models.Request;
using MedicineTrackingSystem.API.Models.Response;
using MedicineTrackingSystem.Domain.Dtos;
using System;

namespace MedicineTrackingSystem.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NotesEditRequest, MedicineEditDto>();

            CreateMap<MedicineDto, MedicineListResponse>();
        }
    }
}
