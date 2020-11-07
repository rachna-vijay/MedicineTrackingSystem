using MedicineTrackingSystem.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicineTrackingSystem.Domain.Contracts
{
    public interface IMedicineService
    {
        Task<IEnumerable<MedicineDto>> GetAllMedicines();

        Task<ServiceResponse> GetMedicineById(Guid medicineGuid);

        Task<ServiceResponse> AddMedicine(MedicineEditDto medicine);

        Task<ServiceResponse> UpdateMedicine(MedicineEditDto medicine);
    }
}
