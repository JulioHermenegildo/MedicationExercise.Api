using System.ComponentModel.DataAnnotations;

namespace MedicationExercise.Api.Model
{
    public class MedicationModel
    {
        [Key]
        public Guid medicationId { get; set; }
        [Required]
        public string medicationName { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public DateTime createdDate { get; set; }
    }

    public class MedicationUpdateModel
    {
        public string? medicationName { get; set; }
        public int? quantity { get; set; }
        public DateTime? createdDate { get; set; }
    }
}
