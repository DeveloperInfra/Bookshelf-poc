namespace Bookshelf.Web.Repository
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public bool Completed { get; set; }
    }
}