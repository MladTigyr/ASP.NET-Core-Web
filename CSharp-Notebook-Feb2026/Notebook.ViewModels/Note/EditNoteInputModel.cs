namespace Notebook.ViewModels.Note
{
    using System.ComponentModel.DataAnnotations;

    public class EditNoteInputModel : CreateNoteInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}
