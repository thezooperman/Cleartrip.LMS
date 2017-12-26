using Cleartrip.Library.Features.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleartrip.Library.Features.Interfaces
{
    public interface ILibraryRepository
    {
        bool AddBook(Book book);
        bool AddUser(User user);
        DateTime IssueBook(Book book, User user);
        bool ReturnBook(Book book);
        bool LimitBorrow(int userId);
        void SearchByUser(string user);
    }
}
