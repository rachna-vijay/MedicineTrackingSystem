using MedicineTrackingSystem.Resources.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineTrackingSystem.Resources.Contracts
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> GetMedicines();
        Task<Medicine> GetMedicineById(Guid? guid);
        Task UpdateMedicine(Medicine medicine);
        Task AddMedicine(Medicine medicine);
    }
}
