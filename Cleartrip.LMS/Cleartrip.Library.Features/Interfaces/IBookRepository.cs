using Cleartrip.Library.Features.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleartrip.Library.Features.Interfaces
{
    public interface IBookRepository
    {
        IList<Book> SearchByTitle(string title);
        IList<Book> SearchByAuthor(string author);
        bool AddBook(Book book);
        bool UpdateBook(Book book, bool status);
    }
}
