using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryCatalog.Models

{
    public class Library
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "שם הספריה")]
        
        public string Name { get; set; } = string.Empty;

        public Genre genre { get; set; }

        public List<Shelf>  Shelves { get; set; }
        public Library() 
        {
            Shelves = new List<Shelf>();
        }
        public Library(string name, Genre genre)
        {
            this.Name = name;
            this.genre = genre;
            Shelves = new List<Shelf>();
        }
    }
    
}
