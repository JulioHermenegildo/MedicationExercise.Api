using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicationExercise.Api.Model;
using Microsoft.AspNetCore.JsonPatch;


namespace MedicationExercise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly MedicationDbContext _context;

        public MedicationsController(MedicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationModel>>> GetMedications()
        {
            return await _context.Medications.ToListAsync();
        }

        [HttpGet("{medicationName}")]
        public async Task<ActionResult<MedicationModel>> GetMedicationsByName(string medicationName)
        {
            var medications = await _context.Medications
                .Where(m => m.medicationName.Contains(medicationName))
                .ToListAsync();

            if (medications == null)
            {
                return NotFound();
            }

            return Ok(medications);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMedication(Guid id, [FromBody] MedicationUpdateModel patch)
        {
            var medication = await _context.Medications.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }

            // estas coisas deveriam estar num ficheiro À parte
            if (patch.medicationName != null)
                medication.medicationName = patch.medicationName;

            if (patch.quantity.HasValue)
                medication.quantity = patch.quantity.Value;

            if (patch.createdDate.HasValue)
                medication.createdDate = patch.createdDate.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedications(Guid id)
        {
            var medications = await _context.Medications.FindAsync(id);
            if (medications == null)
            {
                return NotFound();
            }

            _context.Medications.Remove(medications);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
