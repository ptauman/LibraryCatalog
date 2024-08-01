using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LibraryCatalog.Models
{
    public class Shelf
    {
        [Key]

        public int Id { get; set; }
        [Display(Name = "כותרת המדף")]

        public string Number { get; set; } = string.Empty;
        public int Height { get; set; }
        public int Width { get; set; }
        public Library Library { get; set; }

        public List<Book> Books {  get; set; }
        public Shelf() 
        {
            Books = new List<Book>();

        }
        public Shelf(string number,int height, int width,  Library library)
        {
            Books = new List<Book>();
            Library = library;
            Number = number;
            Height = height;
            Width = width;
        }
    }
}
