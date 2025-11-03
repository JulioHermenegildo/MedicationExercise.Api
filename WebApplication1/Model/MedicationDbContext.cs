using Microsoft.EntityFrameworkCore;

namespace MedicationExercise.Api.Model
{
    public class MedicationDbContext:DbContext
    {
        public MedicationDbContext(DbContextOptions<MedicationDbContext> options) : base(options) { }

        public DbSet<MedicationModel> Medications { get; set; }
    }
}
