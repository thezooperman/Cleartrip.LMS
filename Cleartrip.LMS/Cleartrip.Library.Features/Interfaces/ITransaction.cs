using Cleartrip.Library.Features.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleartrip.Library.Features.Interfaces
{
    public interface ITransaction
    {
        Transaction CreateTransaction(Book book, User user);
        IList<Transaction> FetchTransactionsByUser(User user);
        IList<Transaction> FetchTransactionsByBook(Book book);
    }
}
