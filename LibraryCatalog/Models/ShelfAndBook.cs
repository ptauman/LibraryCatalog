using System;

namespace LibraryCatalog.Models
{
    public class ShelfAndBook
    {
        

        public Shelf Shelf { get; set; } = new Shelf();
        public Book book { get; set; } = new Book();

        public ShelfAndBook()
        {
        }
        public ShelfAndBook(Shelf shelf)
        {
            Shelf = shelf;
        }
    }
}
