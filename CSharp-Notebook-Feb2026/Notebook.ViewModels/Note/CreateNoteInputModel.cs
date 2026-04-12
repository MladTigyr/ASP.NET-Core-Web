namespace Notebook.ViewModels.Note
{
    using System.ComponentModel.DataAnnotations;
    using static GCommon.EntityValidation;
    public class CreateNoteInputModel
    {
        [Required]
        [MinLength(MinNoteTitleLength)]
        [MaxLength(MaxNoteTitleLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(MinNoteContentLength)]
        [MaxLength(MaxNoteContentLength)]
        public string Content { get; set; } = null!;
    }
}
