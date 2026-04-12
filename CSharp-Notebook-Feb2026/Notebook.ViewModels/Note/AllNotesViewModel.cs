namespace Notebook.ViewModels.Note
{
    using System.ComponentModel.DataAnnotations;

    using static GCommon.EntityValidation;

    public class AllNotesViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(MinNoteTitleLength)]
        [MaxLength(MaxNoteTitleLength)]
        public string Title { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Created On")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime CreatedOn { get; set; }
    }
}
