using Cleartrip.Library.Features.Interfaces;
using Cleartrip.Library.Features.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleartrip.Library.Features.Implementation
{
    public class LibraryOperations : ILibraryRepository
    {
        private IBookRepository _bookRepo = null;
        private IUserRepository _userRepo = null;
        private ITransaction _transaction = null;
        public LibraryOperations(IBookRepository bookRepo, ITransaction tran, IUserRepository userRepository)
        {
            _bookRepo = bookRepo;
            _transaction = tran;
            _userRepo = userRepository;
        }

        public bool AddBook(Book book)
        {
            var isBookAdded = false;
            if (book != null)
                isBookAdded = _bookRepo.AddBook(book);
            else
                throw new ArgumentException($"Argument - {book}, must be valid.");
            return isBookAdded;
        }

        public bool AddUser(User user)
        {
            var isUserAdded = false;
            if (user != null)
                isUserAdded = _userRepo.AddUser(user);
            else
                throw new ArgumentException($"Argument - {user}, must be valid.");
            return isUserAdded;
        }

        public DateTime IssueBook(Book book, User user)
        {
            var returnDate = DateTime.MinValue;
            if (book != null)
            {
                var queryBook = _bookRepo.SearchByTitle(book.Name).FirstOrDefault(b => b.IsAvailable == true);
                var checkBorrowLimit = _userRepo.GetUserById(user.Id);
                if (checkBorrowLimit.IsBorrowLimited)
                {
                    Console.WriteLine("User Borrow Capacity Limited, no further books issued");
                    return DateTime.MinValue;
                }
                if (checkBorrowLimit.BorrowCapacity < 1)
                {
                    Console.WriteLine("User Borrow Capacity exhausted, no further books issued");
                    return DateTime.MinValue;
                }
                if (queryBook != null)
                {
                    if (_transaction == null)
                        _transaction = new TransactionOperations();
                    var tran = _transaction.CreateTransaction(queryBook, user);
                    returnDate = tran.DueDate;
                    _bookRepo.UpdateBook(book, false);
                    _userRepo.UpdateBorrowCapacity(user, false);
                }
            }
            return returnDate;
        }

        public bool ReturnBook(Book book)
        {
            var isReturned = false;
            if (book != null)
            {
                var queryTran = _transaction.FetchTransactionsByBook(book).OrderByDescending(t => t.DateOfIssue).FirstOrDefault();
                var queryBook = _bookRepo.SearchByTitle(book.Name).Where(b => b.Id == book.Id && b.IsAvailable == false).FirstOrDefault();
                if (queryBook != null && queryTran != null)
                {
                    var queryUser = _userRepo.GetUserById(queryTran.UserId);
                    if (queryTran.DueDate >= DateTime.UtcNow)
                    {
                        _transaction.CreateTransaction(book, queryUser);
                        _bookRepo.UpdateBook(book, true);
                        _userRepo.UpdateBorrowCapacity(queryUser, true);
                    }
                    else
                    {
                        ;//TODO:Business Logic for late fine
                    }
                    isReturned = true;
                }
            }
            return isReturned;
        }

        public bool LimitBorrow(int userId)
        {
            throw new NotImplementedException();
        }

        public void SearchByUser(string user)
        {
            throw new NotImplementedException();
        }
    }
}
