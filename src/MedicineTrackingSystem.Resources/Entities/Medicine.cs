using System;
using System.Collections.Generic;

namespace MedicineTrackingSystem.Resources.Entities
{
    public class Medicine
    {
        public Guid MedicineGuid { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Notes { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
