using System.ComponentModel.DataAnnotations;
using PatientDocs.Models.Enums;

namespace PatientDocs.Models
{
    public class Patient{
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(15)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string MRN { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        

    }
}