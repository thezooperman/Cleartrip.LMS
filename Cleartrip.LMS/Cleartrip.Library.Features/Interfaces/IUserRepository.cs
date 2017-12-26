using Cleartrip.Library.Features.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleartrip.Library.Features.Interfaces
{
    public interface IUserRepository
    {
        bool AddUser(User user);
        bool LimitBorrowCapacity(User user);
        User GetUserById(Guid userId);
        void UpdateBorrowCapacity(User user, bool addCapacity);
        IList<User> SearchByUser(string userName);
        IList<User> GetAllUsers();
    }
}
