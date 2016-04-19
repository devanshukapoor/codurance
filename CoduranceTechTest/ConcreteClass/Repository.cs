using CoduranceTechTest.Interface;
using System.Collections.Generic;
using System.Linq;

namespace CoduranceTechTest
{
    public class Repository : IRepository
    {
        private readonly IList<IUser> _usersList;

        public Repository(IList<IUser> usersList)
        {
            // Remove the users from the list injected by Ninject
            while (usersList.Count > 0)
            {
                usersList.RemoveAt(0);
            }
            _usersList = usersList;
        }

        public void Create(IUser user)
        {
            var foundUser = _usersList
                                .Where(u => u.UserName.ToLowerInvariant().Equals(user.UserName.ToLowerInvariant()))
                                .Select(u => u).FirstOrDefault();
            if (foundUser == null)
            {
                _usersList.Add(user);
            }
        }

        public IUser Read(string userName)
        {
            return _usersList
                    .Where(u => u.UserName.ToLowerInvariant().Equals(userName.ToLowerInvariant()))
                    .Select(u => u).FirstOrDefault();
        }
    }
}
