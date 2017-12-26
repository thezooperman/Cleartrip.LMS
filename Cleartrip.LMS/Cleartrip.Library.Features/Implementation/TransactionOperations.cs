using Cleartrip.Library.Features.Interfaces;
using Cleartrip.Library.Features.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleartrip.Library.Features.Implementation
{
    public class TransactionOperations : ITransaction
    {
        private IList<Transaction> _transactions = null;

        public TransactionOperations()
        {
            _transactions = new List<Transaction>();
        }
        public Transaction CreateTransaction(Book book, User user)
        {
            Transaction tempTransaction = null;
            if (book.IsAvailable)
            {
                tempTransaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    BookId = book.Id,
                    UserId = user.Id,
                    DateOfIssue = DateTime.UtcNow
                };
                tempTransaction.DueDate = tempTransaction.DateOfIssue.AddDays(45);
                _transactions.Add(tempTransaction);
            }
            else
            {
                tempTransaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    BookId = book.Id,
                    UserId = user.Id,
                    DateOfIssue = DateTime.MinValue,
                    DueDate = DateTime.MinValue
                };
                _transactions.Add(tempTransaction);
            }
            return tempTransaction;
        }

        public IList<Transaction> FetchTransactionsByUser(User user)
        {
            return _transactions.Where(t => t.UserId == user.Id).ToList();
        }

        public IList<Transaction> FetchTransactionsByBook(Book book)
        {
            return _transactions.Where(t => t.BookId == book.Id).ToList();
        }
    }
}
