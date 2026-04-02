namespace Bookshelf.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidation;
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxAuthorNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(MaxAuthorCountryLength)]
        public string Country { get; set; } = null!;
        
        public virtual ICollection<Book> Books { get; set; } 
            = new HashSet<Book>();
    }
}
