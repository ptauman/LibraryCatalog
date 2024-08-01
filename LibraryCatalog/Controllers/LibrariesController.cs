using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.DAL;
using Microsoft.EntityFrameworkCore;
using LibraryCatalog.Models;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;


namespace LibraryCatalog.Controllers
{
    public class LibrariesController : Controller
    {//הצגת הספריות והז'אנרים
        public IActionResult Index()
        {//רשימה שתכיל את כל הספריות
            List<Library> AllLibraries = new List<Library>();
            //רשימה שמכילה את כל הז'אנרים והספריות
            List<Genre> AllGenres = Data.Get.Genres.Include(f => f.Libraries).ToList();
            //לולאה שמחלצת את כל הז'אנרים אחד אחד
            foreach (Genre genre in AllGenres)
            {//לולאה שמחלצת את כל הספריות מז'אנר ספציפי
                foreach (Library library in genre.Libraries)
                {//הוספת כל הספריות לרשימת הספריות
                    AllLibraries.Add(library);
                }
            }
            //הצגת רשימת הספריות
            return View(AllLibraries);
        }

        //דף הוספת ספריה
        public IActionResult CreateLibraty()
        {
            return View();
        }
        //הוספת ספריה בפועל
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateLibraty(string? libName, string? genName)
        {//בדיקה שהמשתמש הזין ערך כל שהוא
            if (libName != null && genName != null)
            { 

                    //בדיקה שיש ז'אנר בשם הזה
                Genre? genre = Data.Get.Genres.FirstOrDefault(f => f.Name == genName);
                if (genre == null)
                {//הודעה מתאימה למקרה שאין ז'אנר
                    TempData["Massage"] = "ז'אנר לא קיים";
                    return RedirectToAction("Index");
                }//בדיקה שאין ספריה בשם הזה
                if (Data.Get.Genres.Any(f => f.Libraries.Any(f => f.Name == libName)))
                {//הודעה מתאימה
                    TempData["Massage"] = "שם הספריה כבר קיים";
                    return RedirectToAction("Index");
                }
                //הוספת הספריה ושמירת השינויים
                genre.AddLibrary(libName);

                Data.Get.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //פונקציה שתציג את המדפים הקיימים בספריה
        public IActionResult Shelves(string? name)
        {//קבלת הספריה
           Library? library1 = Data.Get.Libraries.Include(f => f.Shelves).FirstOrDefault(f => f.Name == name);
            if ( library1 == null)
            {

                return RedirectToAction("Index");
            }//הצגת המדפים של הספריה
            return View(library1.Shelves.ToList());           
        }

        //פונקציה לדף הוספת מדף
        public IActionResult CreateShlf(string? name)
        {//קבלת הספריה המתאימה
            Library? library = Data.Get.Libraries.FirstOrDefault(f => f.Name == name);
            if (library == null)
            {

                return RedirectToAction("Index");
            }
            //שימוש במשתנה שמכיל ספריה ומדף
            ShelfAndLibrary shelfAndLibrary = new ShelfAndLibrary(library);
            return View(shelfAndLibrary);
        }

        //פונקציה להוספת מדף בפועל
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateShlf(ShelfAndLibrary shelfAndLibrary)
        {//קבלת הספריה הנכונה
            Library? library = Data.Get.Libraries.FirstOrDefault(f => f.Id == shelfAndLibrary.Library.Id);
            if (library == null)
            { 
                return RedirectToAction("Index");
            }
            //הוספת המדף ושמירת השינויים
            library.Shelves.Add(shelfAndLibrary.Shelf);
            Data.Get.SaveChanges();
            return RedirectToAction("Index");
        }

        //פונקציה לדף הוספת ספר
        public IActionResult Creatabook(int? id)
        {//מציאת המדף המתאים
            Shelf? shelf = Data.Get.Shelves.FirstOrDefault(f => f.Id ==  id);
            if (shelf == null)
            {
                return RedirectToAction("Index");
            }//יצירת משתנה שיכיל הן את המדף והן את הספר
            ShelfAndBook shelfAndBook = new ShelfAndBook(shelf);
            return View(shelfAndBook);
        }

        //פונקציה להוספת ספר כוללת השערים הלוגיים נדרשים
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Creatabook(ShelfAndBook shelfAndBook)
        {//קבלת המדף
            Shelf? shelf = Data.Get.Shelves.Include(f => f.Books).FirstOrDefault(f => f.Id == shelfAndBook.Shelf.Id);
            if (shelf == null)
            {

                return RedirectToAction("Index");

            }

            //קבלת נתוני אורך ורוחב של הספר ושל המדף
            Book MyBook = shelfAndBook.book;
            int ShelfWidth = shelf.Width;
            //לולאה שתעדכן את הרוחב העדכני במדף
            foreach(Book book in shelf.Books)
            {
                ShelfWidth-=book.Width;
            }
            //בדיקת הרוחב והגובה המתאימים
            if (MyBook.Height> shelf.Height)
            {//הודעת שגיאה למקרה גובה
                TempData["Massage"] = "מידי גבוה. הסף לא התווסף";
                return RedirectToAction("Creatabook", shelfAndBook);
            }//בדיקת גובה נמוך 
            if (MyBook.Height < (shelf.Height-10))
            {
                TempData["Massage"] = "מידי נמוך. הספר התווסף וחבל";               
            }
            if (MyBook.Width > ShelfWidth)
            {//בדיקת רוחב והודעת שגיאה
                TempData["Massage"] = "מידי רחב. הספר לא התווסף";
                return RedirectToAction("Creatabook", shelfAndBook);
            }
            //ביצוע ההוספה בפועל ושמירת השינויים
            shelf.Books.Add(shelfAndBook.book);
            Data.Get.SaveChanges();
            //מעבר לדף ההתחלה עם דף מתאים
            TempData["Massage"] = "הספר התווסף בהצלחה";

            return RedirectToAction("Index");
        }

        //פונקציה לקבלת מספר הספרים בסט
        public IActionResult CreataNumber(int id)
        {//קבלת המדף
            Shelf? shelf = Data.Get.Shelves.FirstOrDefault(f => f.Id == id);
            if (shelf == null)
            {
                return RedirectToAction("Index");
            }//משתנה מיוחד שיכיל את המדף ואת מספר הספרים 
            NumberAndShelf NumberSet = new NumberAndShelf(shelf);
            return View(NumberSet);
        }
        //פונקציה לדף הוספת סט ספרים
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreataSetbookLoc(NumberAndShelf NumberSet)
        {//קבלת המשתנה המיוחד של המדף ומספר הספרים
            Shelf? shelf = Data.Get.Shelves.FirstOrDefault(f => f.Id == NumberSet.Shelf.Id);
            if (shelf == null)
            {
                return RedirectToAction("Index");
            }
            int chak = NumberSet.SetNmber;
            //משתנה מכיל רשימת ספרים באורך המתאים ואת המדף
            SetBox setBox = new SetBox(shelf, chak);
            return View(setBox);
        }

        //פונקציה להוספת סט הספרים בפועל

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreataSetbook(SetBox setBox)
        {//קבלת המדף הנוכחי
            Shelf? shelf = Data.Get.Shelves.Include(f => f.Books).FirstOrDefault(f => f.Id == setBox.Shelf.Id);
            if (shelf == null)
            {
                return RedirectToAction("Index");
            }
            //משתנים שיכילו את רוחב המדף ורוחב הסט
            int SetWidth = 0;
            int ShelfWidth = shelf.Width;
            //לולאה שתבדוק מה הרוחב הקיים במדף
            foreach (Book book in shelf.Books)
            {
                ShelfWidth -= book.Width;
            }
            //לולאה שרצה על כל ספר בסט החדש
            foreach (Book MyBook in setBox.Books)
            {//בדיקת הגובה
                if (MyBook.Height > shelf.Height)
                {
                    TempData["Massage"] = "מידי גבוה. הסט לא התווסף";
                    return RedirectToAction("Index");
                }//עדכון הרוחב
                SetWidth += MyBook.Width;
            }//בדיקת הרוחב
            if (SetWidth > ShelfWidth)
            {
                TempData["Massage"] = "מידי רחב. הסט לא התווסף";
                return RedirectToAction("Index");
            }
            //הוספת כל המערך בבת אחת
            shelf.Books.AddRange(setBox.Books);
            Data.Get.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
