namespace LibraryCatalog.Models
{
    public class SetBox
    {
        public Shelf Shelf { get; set; } = new Shelf();
        public List<Book> Books { get; set; }

        public SetBox()
        {
            Books = new List<Book>();
        }

        public SetBox(Shelf shelf, int NumberSet)
        {
            Shelf = shelf;
            Books = new List<Book>();
            for (int i = 0; i < NumberSet; i++)
            {
                Books.Add(new Book());
            }
        }
    }
}
