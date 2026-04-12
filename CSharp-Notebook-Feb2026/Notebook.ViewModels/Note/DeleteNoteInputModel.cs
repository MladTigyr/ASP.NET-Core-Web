namespace Notebook.ViewModels.Note
{
    using System.ComponentModel.DataAnnotations;

    public class DeleteNoteInputModel : EditNoteInputModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Created On")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime CreatedOn { get; set; }
    }
}
