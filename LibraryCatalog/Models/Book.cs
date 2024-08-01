using System.ComponentModel.DataAnnotations;

namespace LibraryCatalog.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "שם הספר")]

        public string Name { get; set; } = string.Empty;
        public int Height { get; set; }
        public int Width { get; set; }

        public Shelf shelf { get; set; }

        public Book() 
        {
        }
        public Book(string name, int height, int width, Shelf shelf)
        {
            Name = name;
            Height = height;
            Width = width;
            this.shelf = shelf;
        }
    }
}
