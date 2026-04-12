namespace Notebook.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using static Notebook.GCommon.EntityValidation;

    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNoteTitleLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(MaxNoteContentLength)]
        public string Content { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;
    }
}
