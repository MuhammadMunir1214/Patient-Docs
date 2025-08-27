using System.ComponentModel.DataAnnotations;
using PatientDocs.Models.Enums;

namespace PatientDocs.Models
{
    public class Document{
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public int UploadedByUserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}