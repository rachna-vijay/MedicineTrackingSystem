using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineTrackingSystem.API.Models.Request
{
    public class NotesEditRequest
    {
        public Guid MedicineGuid { get; set; }
        public string Notes { get; set; }
    }
}
