namespace LibraryCatalog.Models
{
    public class ShelfAndLibrary
    {
        public Library Library { get; set; } = new Library();

        public Shelf Shelf { get; set; } = new Shelf();

        public ShelfAndLibrary()
            {           
            }
        public ShelfAndLibrary(Library library)
        {
            Library = library;
        }
    }
    
}
