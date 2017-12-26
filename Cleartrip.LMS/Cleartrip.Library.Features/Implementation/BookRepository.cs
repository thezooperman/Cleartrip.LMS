using Cleartrip.Library.Features.Interfaces;
using Cleartrip.Library.Features.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleartrip.Library.Features.Implementation
{
    public class BookRepository : IBookRepository
    {
        private IList<Book> _books;
        public BookRepository()
        {
            _books = new List<Book>();
        }
        public bool AddBook(Book book)
        {
            var isAdded = false;
            book.Id = Guid.NewGuid();
            book.IsAvailable = true;
            _books.Add(book);

            return isAdded;
        }

        public IList<Book> SearchByAuthor(string author)
        {
            return _books.Where(b => b.Author.ToLowerInvariant().Contains(author.ToLowerInvariant())).ToList<Book>();
        }

        public IList<Book> SearchByTitle(string title)
        {
            return _books.Where(b => b.Name.ToLowerInvariant().Contains(title.ToLowerInvariant())).ToList<Book>();
        }

        public bool UpdateBook(Book book, bool status)
        {
            var isUpdated = false;
            var fetchBook = _books.Where(b => b.Id == book.Id).FirstOrDefault();
            if (fetchBook != null)
            {
                fetchBook.IsAvailable = status;
            }

            return isUpdated;
        }

        public IList<Book> GetAllBooks()
        {
            return _books;
        }
    }
}
