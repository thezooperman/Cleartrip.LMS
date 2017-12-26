using Cleartrip.Library.Features.Interfaces;
using Cleartrip.Library.Features.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleartrip.Library.Features.Implementation
{
    public class UserRepository : IUserRepository
    {
        private IList<User> _users = null;
        public UserRepository()
        {
            _users = new List<User>();
        }
        public bool AddUser(User user)
        {
            var isUserAdded = false;
            var checkIfUserIsPresent = _users.ToList().Exists(u => user.FirstName.ToLowerInvariant() == u.FirstName.ToLowerInvariant() && u.LastName.ToLowerInvariant() == u.LastName.ToLowerInvariant() && u.Address.ToLowerInvariant() == user.Address.ToLowerInvariant());
            if (!checkIfUserIsPresent)
            {
                user.Id = Guid.NewGuid();
                user.IsBorrowLimited = false;
                _users.Add(user);
                isUserAdded = true;
            }
            else
                throw new ArgumentException($"User - {user.FirstName + " " + user.LastName}, is already added.");
            return isUserAdded;
        }

        public bool LimitBorrowCapacity(User user)
        {
            var isLimited = false;
            var getUser = _users.Where(u => u.Id == user.Id).FirstOrDefault();
            if (getUser != null)
            {
                getUser.IsBorrowLimited = true;
                isLimited = false;
            }
            return isLimited;
        }

        public User GetUserById(Guid userId)
        {
            return _users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public void UpdateBorrowCapacity(User user, bool addCapacity)
        {
            var getUser = _users.Where(a => a.Id == user.Id).FirstOrDefault();
            if (getUser != null)
            {
                if (addCapacity)
                    getUser.BorrowCapacity += 1;
                else
                    getUser.BorrowCapacity -= 1;
            }
        }
    }
}
