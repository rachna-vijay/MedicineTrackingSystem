using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MedicineTrackingSystem.Domain.Mapper;
using MedicineTrackingSystem.Domain.Contracts;
using MedicineTrackingSystem.Domain.Services;
using FluentValidation;
using MedicineTrackingSystem.Domain.Dtos;
using MedicineTrackingSystem.Domain.Validations;

namespace MedicineTrackingSystem.Domain
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IValidator<MedicineEditDto>, MedicineValidator>();
            services.AddScoped<IMedicineService, MedicineService>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
