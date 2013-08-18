using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bookshelf.Web.Repository;

namespace Bookshelf.Web.API
{
    public class BookController : ApiController
    {
        public BookController()
        {
            Repository = new ExampleDataRepository();
        }

        private IExampleDataRepository Repository { get; set; }

        public IEnumerable<Book> GetAll()
        {
            return Repository.All();
        }

        public Book Get(int id)
        {
            Book book = Repository.Select(id);
            if (book == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return book;
        }

        public HttpResponseMessage Post(Book book)
        {
            book = Repository.Insert(book);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, book);
            string uri = Url.Link("DefaultApi", new {id = book.Id});
            response.Headers.Location = new Uri(uri);

            return response;
        }

        public void Put(int id, Book book)
        {
            book.Id = id;
            if (!Repository.Update(book))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            Repository.Delete(id);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}