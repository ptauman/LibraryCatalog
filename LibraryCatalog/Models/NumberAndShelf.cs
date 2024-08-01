namespace LibraryCatalog.Models
{
    public class NumberAndShelf
    {
        public Shelf Shelf { get; set; } = new Shelf();
        public int SetNmber {get; set; } 

        public NumberAndShelf()
        {
        }
        public NumberAndShelf(Shelf shelf)
        {
            Shelf = shelf;
        }
    }
}
