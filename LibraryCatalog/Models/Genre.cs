using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryCatalog.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "שם הז'אנר")]

        public string Name { get; set; } = string.Empty;

        public List<Library> Libraries { get; set; }
        public Genre() 
        {
            Libraries = new List<Library>();
        }
        public Genre(string name)
        {
            Name = name;
            Libraries = new List<Library>();
        }
        public void AddLibrary(string name)
        {
            //הוספת תמונה חדשה מערך התמונות.כפרנד נוסיף את הקלאס הנוכחי שדרכו בוצעה הקריאה
            Libraries.Add(new Library(name, this));
        }
    }

}
