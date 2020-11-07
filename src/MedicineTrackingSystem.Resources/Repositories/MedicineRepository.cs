using Microsoft.Extensions.Logging;
using MedicineTrackingSystem.Resources.Contracts;
using MedicineTrackingSystem.Resources.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.IO;
using Newtonsoft.Json;

namespace MedicineTrackingSystem.Resources.Repositories
{
    /// <summary>
    /// medicine repository
    /// </summary>
    public class MedicineRepository : IMedicineRepository
    {
        private readonly ILogger<MedicineRepository> _logger;
        private readonly IHostEnvironment _hostingEnvironment;

        public MedicineRepository(ILogger<MedicineRepository> logger, IHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// read json
        /// </summary>
        /// <returns></returns>
        public async Task<List<Medicine>> ReadJson()
        {
            var jsonPath = Path.Combine(_hostingEnvironment.ContentRootPath, "json");
            if (!Directory.Exists(jsonPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(jsonPath);
            }
            var path = Directory.GetFiles(jsonPath, "*medicine*").FirstOrDefault();
            var medicines = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<List<Medicine>>(medicines);
        }

        /// <summary>
        /// write to json
        /// </summary>
        /// <param name="medicines"></param>
        /// <returns></returns>
        public async Task WriteJson(List<Medicine> medicines)
        {
            var jsonPath = Path.Combine(_hostingEnvironment.ContentRootPath, "json");
            if (!Directory.Exists(jsonPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(jsonPath);
            }
            var path = Directory.GetFiles(jsonPath, "*medicine*").FirstOrDefault();
            var medicinesJson = JsonConvert.SerializeObject(medicines);

            await File.WriteAllTextAsync(path, medicinesJson);
        }

        /// <summary>
        /// add medicine
        /// </summary>
        /// <param name="medicine"></param>
        /// <returns></returns>
        public async Task AddMedicine(Medicine medicine)
        {
            try
            {
                var medicines = await ReadJson();
                medicines.Add(medicine);
                await WriteJson(medicines);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured in {this.GetType().Name}. Error: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Medicine>> GetMedicines()
        {
            try
            {
                return await ReadJson();
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
        /// <param name="medicineGuid"></param>
        /// <returns></returns>
        public async Task<Medicine> GetMedicineById(Guid? medicineGuid)
        {
            try
            {
                var medicines = await ReadJson();
                return medicines.FirstOrDefault(x => x.MedicineGuid == medicineGuid);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured in {this.GetType().Name}. Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// update medicine
        /// </summary>
        /// <param name="medicine"></param>
        /// <returns></returns>
        public async Task UpdateMedicine(Medicine medicine)
        {
            try
            {
                var medicines = await ReadJson();
                medicines.Remove(medicines.FirstOrDefault(x => x.MedicineGuid == medicine.MedicineGuid));
                medicines.Add(medicine);
                await WriteJson(medicines);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured in {this.GetType().Name}. Error: {ex.Message}");
                throw;
            }
        }
    }
}
