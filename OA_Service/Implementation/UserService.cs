using System;
using System.Collections.Generic;
using System.Linq;
using OA_DataAccess.Entities;
using OA_DataAccess.Interfaces;
using OA_Service.Interface;
using AutoMapper;
using OA_Repository.DTO;

namespace OA_Service.Implementation
{
    /// <summary>
    /// Class with user services.
    /// </summary>
    public class UserService : IUserService
    {
        private IUnitOfWorks Db { get; }
        private readonly IMapper _mapper;

        public UserService(IUnitOfWorks unitOfWorks)
        {
            Db = unitOfWorks;
            _mapper = new MapperConfiguration(configure =>
            {
                configure.CreateMap<User, UserDTO>();
                configure.CreateMap<UserDTO, User>();
            }).CreateMapper();
        }

        /// <summary>
        /// Method for get all UserDTO objects.
        /// </summary>
        /// <returns>collection of UserDTO.</returns>
        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(Db.Users.GetAll());
        }

        /// <summary>
        /// Method for get UserDTO object by id.
        /// </summary>
        /// <param name="id">id of UserDTO.</param>
        /// <returns>UserDTO object.</returns>
        public UserDTO GetUserById(int id)
        {
            var user = _mapper.Map<User, UserDTO>(Db.Users.GetById(id));
            if (user is null)
            {
                throw new ArgumentException("No Registered with such Id", nameof(id));
            }
            return user;
        }

        /// <summary>
        /// Method for create new UserDTO object.
        /// </summary>
        /// <param name="email">email of user.</param>
        /// <returns>new UserDTO object.</returns>
        public UserDTO Register(string email)
        {
            UserDTO user = new UserDTO();
            var userFromRepository = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(Db.Users.GetAll());
            user.Role = Roles.Registered;
            user.Email = email;
            Db.Users.Create(_mapper.Map<UserDTO, User>(user));
            return user;
        }

        /// <summary>
        /// Method for update UserDTO object.
        /// </summary>
        /// <param name="id">id of UserDTO.</param>
        /// <param name="user">UserDTO object.</param>
        public void UpdateUser(int id, UserDTO user)
        {
            var userFromRepository = _mapper.Map<User, UserDTO>(Db.Users.GetById(id));
            if (userFromRepository == null)
            {
                throw new ArgumentException();
            }
            userFromRepository.Id = user.Id;
            userFromRepository.Email = user.Email;
            Db.Users.Update(_mapper.Map<UserDTO, User>(user));
        }

        /// <summary>
        /// Method for login UserDTO object.
        /// </summary>
        /// <param name="email">user credential.</param>
        /// <returns>user logs.</returns>
        public UserDTO Login(string email)
        {
            var userFromRepository = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(Db.Users.GetAll());
            if (userFromRepository == null)
            {
                throw new ArgumentException();
            }
            var fromRepository = userFromRepository.ToList();
            if (fromRepository.Any(x => x.Email.Equals(email)))
            {
                return fromRepository.First(x => x.Email.Equals(email));
            }
            throw new ArgumentException();
        }
    }
}
