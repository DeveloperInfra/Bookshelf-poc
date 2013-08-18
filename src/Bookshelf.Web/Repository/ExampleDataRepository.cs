using System.Collections.Generic;
using System.Linq;

namespace Bookshelf.Web.Repository
{
    public interface IExampleDataRepository
    {
        IEnumerable<Book> All();
        Book Select(int id);
        Book Insert(Book book);
        bool Update(Book book);
        void Delete(int id);
        void ClearCache();
        bool HasCache();
    }

    public class ExampleDataRepository : IExampleDataRepository
    {
        private const string Key = "books";
        private readonly ICacheProvider _cache;

        public ExampleDataRepository()
            : this(new DefaultCacheProvider())
        {
        }

        public ExampleDataRepository(ICacheProvider cacheProvider)
        {
            _cache = cacheProvider;

            SeedCache();
        }

        private List<Book> Cache
        {
            get { return _cache.Get(Key) as List<Book> ?? new List<Book>(); }
        }

        public IEnumerable<Book> All()
        {
            return Cache;
        }

        public Book Select(int id)
        {
            return Cache.Find(x => x.Id == id);
        }

        public Book Insert(Book book)
        {
            if (book.Id == default(int))
            {
                book.Id = Cache.Count + 1;
            }
            Cache.Add(book);

            return book;
        }

        public bool Update(Book book)
        {
            int index = Cache.FindIndex(x => x.Id == book.Id);
            if (index == -1)
            {
                return false;
            }

            Cache.RemoveAt(index);
            Cache.Add(book);

            return true;
        }

        public void Delete(int id)
        {
            Book book = Cache.Find(x => x.Id == id);
            Cache.Remove(book);
        }

        public void ClearCache()
        {
            _cache.Invalidate(Key);
        }

        public bool HasCache()
        {
            return _cache.IsSet(Key);
        }

        private void SeedCache()
        {
            // If the cache does not exist, we need to read it from the db
            if (!_cache.IsSet(Key))
            {
                // Get the db data
                List<Book> books = new ExampleDb().Books;

                if (books.Any())
                {
                    // Put this data into the cache for 30 minutes
                    _cache.Set(Key, books, 30);
                }
            }
        }
    }
}