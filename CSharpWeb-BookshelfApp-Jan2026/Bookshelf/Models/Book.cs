namespace Bookshelf.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidation;

    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxBookTitleLength)]
        public string Title { get; set; } = null!;

        public int Year { get; set; }

        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; } = null!;
    }
}