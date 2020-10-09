using MedicineTrackingSystemApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineTrackingSystemApi.BusinessLogics
{
    public interface IExceptNotes
    {
        object RemoveNotes(IEnumerable<MediciceTrackingSystem> mediciceTrackingSystems);
    }
    public class MedicineDisplayRules : IExceptNotes
    {
        public object RemoveNotes(IEnumerable<MediciceTrackingSystem> mediciceTrackingSystems)
        {
            
              var result= mediciceTrackingSystems.Select(x => new { x.Brand, x.ExpiryDate, x.Name, x.Price, x.Quantity }).ToList().FirstOrDefault();

            return result;
           
        }
    }
}
