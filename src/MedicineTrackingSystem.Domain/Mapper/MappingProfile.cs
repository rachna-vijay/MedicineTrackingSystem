using AutoMapper;
using MedicineTrackingSystem.Domain.Dtos;
using MedicineTrackingSystem.Resources.Entities;
using System;

namespace MedicineTrackingSystem.Domain.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Medicine, MedicineDto>();

            CreateMap<MedicineEditDto, Medicine>()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
                .ForMember(x => x.Notes, opt => opt.Ignore())
                .AfterMap((s, d) => d.MedicineGuid = s.MedicineGuid ?? Guid.NewGuid());
        }
    }
}
