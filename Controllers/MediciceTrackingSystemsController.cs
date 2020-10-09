using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicineTrackingSystemApi.Models;
using MedicineTrackingSystemApi.BusinessLogics;

namespace MedicineTrackingSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediciceTrackingSystemsController : ControllerBase
    {
        private readonly TestContext _context;
        private IExceptNotes _exceptNotes;

        public MediciceTrackingSystemsController(TestContext context, IExceptNotes exceptNotes)
        {
            _context = context;
            _exceptNotes = exceptNotes;
        }

     //get all the meds
        [HttpGet]
        public async Task<ActionResult<object>> GetMediciceTrackingSystem()
        {
            var result= await _context.MediciceTrackingSystem.ToListAsync();
            var resultWithoutNotes=_exceptNotes.RemoveNotes(result);
            return resultWithoutNotes;
        }

        //search using name 
        [HttpGet("{name}")]
        public async Task<IEnumerable<MediciceTrackingSystem>> GetMediciceTrackingSystem(string name)
        {
            var result = await _context.MediciceTrackingSystem.ToListAsync();
            var mainresult = result.Where(x => x.Name == name);

            if (mainresult == null)
            {
               
            }

            return mainresult;
        }


  
        [HttpPost]
        public async Task<ActionResult<MediciceTrackingSystem>> PostMediciceTrackingSystem(MediciceTrackingSystem mediciceTrackingSystem)
        {
            var currentDate = DateTime.Now;
            try { 
            if (mediciceTrackingSystem.ExpiryDate < currentDate.AddDays(30))
            {
                //returm warning 
            
                _context.MediciceTrackingSystem.Add(mediciceTrackingSystem);
                
            }
            if (mediciceTrackingSystem.ExpiryDate < currentDate.AddDays(15))
            {
                    throw new Exception("Medicine expiry date is near");
            }
            
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MediciceTrackingSystemExists(mediciceTrackingSystem.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMediciceTrackingSystem", new { id = mediciceTrackingSystem.Id }, mediciceTrackingSystem);
        }

        private bool MediciceTrackingSystemExists(int id)
        {
            return _context.MediciceTrackingSystem.Any(e => e.Id == id);
        }
    }
}
