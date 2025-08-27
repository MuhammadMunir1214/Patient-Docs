using System.ComponentModel.DataAnnotations;
using PatientDocs.Models.Enums;

namespace PatientDocs.Models
{
    public class Note{
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int AuthorUserId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}