using System;
using System.Collections.Generic;

namespace MedicineTrackingSystemApi.Models
{
    public partial class MediciceTrackingSystem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Notes { get; set; }
    }
}
