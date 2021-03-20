using System;
using System.Collections.Generic;
using System.Linq;
using OA_DataAccess.EF;
using OA_DataAccess.Entities;
using OA_DataAccess.Interfaces;

namespace OA_DataAccess.Repositories
{
    /// <summary>
    /// Class repository for work with users.
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        private readonly EShopContext _db;

        public UserRepository(EShopContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Method for get all users from db.
        /// </summary>
        /// <returns>list of all users.</returns>
        public IEnumerable<User> GetAll()
        {
            return _db.Users.ToList();
        }

        /// <summary>
        /// Method for get user by id from db.
        /// </summary>
        /// <param name="id">id of user.</param>
        /// <returns>get user.</returns>
        public User GetById(int id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Method for create user in db.
        /// </summary>
        /// <param name="user">new user.</param>
        public void Create(User user)
        {
            user.Id = _db.Users.Count > 0 ? _db.Users.Max(x => x.Id) + 1 : 1;
            _db.Users.Add(user);
        }

        /// <summary>
        /// Method for update user in db.
        /// </summary>
        /// <param name="user">updated user.</param>
        public void Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var index = _db.Users.FindIndex(x => x.Id == user.Id);
            if (index < 0)
            {
                IndexOutOfRangeException indexOutOfRangeException = new IndexOutOfRangeException("user");
                throw indexOutOfRangeException;
            }
            _db.Users.RemoveAt(index);
            _db.Users.Add(user);
        }
    }
}
