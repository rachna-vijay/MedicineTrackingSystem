using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicineTrackingSystem.Domain.Dtos
{
    public class MedicineEditDto
    {
        public Guid? MedicineGuid { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Notes { get; set; }
    }
}
