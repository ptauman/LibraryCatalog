using Microsoft.EntityFrameworkCore;
using LibraryCatalog.Models;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryCatalog.DAL
{//קלאס של מסמך הדאטא בייס שיורש מקלאס מובנה של השפה. דרכו נוכל להפעיל מתודות שונות על הקובץ
    public class DataLayer : DbContext
    {//קונטרקטור שמקבל כתובת החיבור וגם מעביר אותו למחלקת האם
        public DataLayer(string connectionstring) : base(GetOptions(connectionstring))
        {//פונקציה יוצרת קובץ דאטא בייס מאחורי הקלעים או יוצרת בו טבלאות במידת הצו
            Database.EnsureCreated();
            Seed();
        }
        private void Seed()
        {//ברירת מחדל שדרכה נבדוק אם הקובץ עובדלפני הוספה דרך האתר
            if (Genres.Any())
            {
                return;
            }
            Genre genre = new Genre
            {
                Name = "science"
            };
            Genres.Add(genre);
            SaveChanges();
        }
        //פונקציה שבונה לנו משתנה שרדכו נוכל לבצע חיבור בפועל
        //ניצור משתנה של דיביקונטקסט עם אפשרויות, מנתמש במתודה של אסקיואל, ניתן למתודה את הכתובת הדרושה לה, החזרת התוצאה למי שקרא לפונקציה
        private static DbContextOptions GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
        }
        //יצירת טבלה של זאנרים
        public DbSet<Genre> Genres { get; set; }
        //יצירת טבלה של ספריות
        public DbSet<Library> Libraries { get; set; }

        public DbSet<Shelf> Shelves { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); ;
            modelBuilder.Entity<Library>().HasIndex(c=>c.Name).IsUnique();
        }

        public void AddGenre(string name)
        {
            //הוספת תמונה חדשה מערך התמונות.כפרנד נוסיף את הקלאס הנוכחי שדרכו בוצעה הקריאה
            Genres.Add(new Genre(name));
        }

    }
}
