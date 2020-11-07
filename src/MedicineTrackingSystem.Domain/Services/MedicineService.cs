using AutoMapper;
using MedicineTrackingSystem.Domain.Constants;
using MedicineTrackingSystem.Domain.Contracts;
using MedicineTrackingSystem.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using MedicineTrackingSystem.Resources.Contracts;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;
using MedicineTrackingSystem.Resources.Entities;

namespace MedicineTrackingSystem.Domain.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMapper _mapper;
        ILogger<MedicineService> _logger;

        /// <summary>
        /// medicine service
        /// </summary>
        /// <param name="medicineRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public MedicineService(IMedicineRepository medicineRepository, IMapper mapper, ILogger<MedicineService> logger)
        {
            _medicineRepository = medicineRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// create new medicine
        /// </summary>
        /// <param name="medicine"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> AddMedicine(MedicineEditDto medicine)
        {
            try
            {
                Medicine medicineEntity = _mapper.Map<Medicine>(medicine);

                await _medicineRepository.AddMedicine(medicineEntity);
                medicine.MedicineGuid = medicineEntity.MedicineGuid;
                return ServiceResponse.Success(medicine, ResponseCodes.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured in {this.GetType().Name}. Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// get all medicines
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MedicineDto>> GetAllMedicines()
        {
            try
            {
                var medicines = await _medicineRepository.GetMedicines();
                return _mapper.Map<IEnumerable<MedicineDto>>(medicines);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured in {this.GetType().Name}. Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// get medicine by id
        /// </summary>
        /// <returns></returns>
        private async Task<Medicine> GetMedicineById(Guid? id)
        {
            return await _medicineRepository.GetMedicineById(id);
        }

        /// <summary>
        /// update medicine
        /// </summary>
        /// <param name="medicineDto"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> UpdateMedicine(MedicineEditDto medicineDto)
        {
            try
            {
                var medicine = await GetMedicineById(medicineDto.MedicineGuid);
                if (medicine == null)
                {
                    return ServiceResponse.Failure("Medicine does not exist.", ResponseCodes.NotFound);
                }

                medicine = _mapper.Map(medicineDto, medicine);
                await _medicineRepository.UpdateMedicine(medicine);
                return ServiceResponse.Success(medicineDto, ResponseCodes.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured in {this.GetType().Name}. Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// get medicine by id
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse> GetMedicineById(Guid medicineGuid)
        {
            try
            {
                var medicine = await GetMedicineById(medicineGuid);

                return ServiceResponse.Success(_mapper.Map<MedicineDto>(medicine), ResponseCodes.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured in {this.GetType().Name}. Error: {ex.Message}");
                throw;
            }
        }
    }
}
