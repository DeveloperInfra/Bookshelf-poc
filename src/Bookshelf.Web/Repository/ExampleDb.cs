using System.Collections.Generic;

namespace Bookshelf.Web.Repository
{
    public class ExampleDb
    {
        private List<Book> _books;

        public ExampleDb()
        {
            Books = new List<Book>
                {
                    new Book
                        {
                            Id = 1,
                            Title = "Functional JavaScript",
                            Author = "Michael Fogus",
                            Image = "functional-js.gif",
                            Completed = false
                        },
                    new Book
                        {
                            Id = 2,
                            Title = "JavaScript: The Definitive Guide, 6th Edition",
                            Author = "David Flanagan",
                            Image = "js-definitive-guide.gif",
                            Completed = true
                        },
                    new Book
                        {
                            Id = 3,
                            Title = "Node for Front-End Developers",
                            Author = "Garann Means",
                            Image = "node-for-fed.gif",
                            Completed = false
                        },
                    new Book
                        {
                            Id = 4,
                            Title = "JavaScript Web Applications",
                            Author = "Alex MacCaw",
                            Image = "js-web-apps.gif",
                            Completed = false
                        },
                    new Book
                        {
                            Id = 5,
                            Title = "Learning JavaScript Design Patterns",
                            Author = "Addy Osmani",
                            Image = "js-patterns.gif",
                            Completed = false
                        },
                    new Book
                        {
                            Id = 6,
                            Title = "Maintainable JavaScript",
                            Author = "Nicholas C. Zakas",
                            Image = "maintainable-js.gif",
                            Completed = true
                        }
                };
        }

        public List<Book> Books
        {
            get { return _books ?? (_books = new List<Book>()); }
            set { _books = value; }
        }
    }
}