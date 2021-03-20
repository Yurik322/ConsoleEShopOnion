using System;
using OA_DataAccess.Entities;
using OA_DataAccess.Repositories;
using OA_Repository.DTO;
using OA_Service.Implementation;

namespace OA_UI.Controllers
{
    /// <summary>
    /// Class controller for user services.
    /// </summary>
    public class UserController
    {
        private readonly EfUnitOfWork _unitOfWork = new EfUnitOfWork();
        private readonly UserService _userService;
        public event EventHandler<ProcessEventArgs> ProcessHandler;
        private UserDTO _currentUser = new UserDTO();
        public UserController()
        {
            _currentUser.Role = Roles.Guest;
            _userService = new UserService(_unitOfWork);
        }

        /// <summary>
        /// Method for login user.
        /// </summary>
        public UserDTO Login()
        {
            if (_currentUser.Role > Roles.Guest)
            {
                OnProcessCompleted(new ProcessEventArgs("You must logout in order to login"));
                return _currentUser;
            }
            try
            {
                Console.WriteLine("Enter your email:");
                var email = Console.ReadLine();
                _currentUser = _userService.Login(email);
                OnProcessCompleted(new ProcessEventArgs());
            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs("There is no such user"));
            }

            return _currentUser;
        }

        /// <summary>
        /// Method for register new user.
        /// </summary>
        public UserDTO Register()
        {
            if (_currentUser.Role > Roles.Guest)
            {
                OnProcessCompleted(new ProcessEventArgs("You must logout to register a new account"));
                return _currentUser;
            }
            try
            {
                Console.WriteLine("Enter your email: ");
                var email = Console.ReadLine();
                _currentUser = _userService.Register(email);
                OnProcessCompleted(new ProcessEventArgs());
            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs("Incorrect input"));
            }

            return _currentUser;
        }

        /// <summary>
        /// Method for update personal user data.
        /// </summary>
        public void ChangePersonalData()
        {
            if (_currentUser.Role < Roles.Registered)
            {
                OnProcessCompleted(new ProcessEventArgs("You are not permitted to use this command"));
                return;
            }
            try
            {
                var user = new User();
                Console.WriteLine("Enter your new email:");
                var email = Console.ReadLine();
                if (!user.IsValidEmail(email))
                {
                    throw new ArgumentException("Invalid email");
                }
                _currentUser.Email = email;
                _userService.UpdateUser(_currentUser.Id, _currentUser);
                OnProcessCompleted(new ProcessEventArgs());
            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs("Incorrect input"));
            }
        }

        /// <summary>
        /// Method for update user data by administrator.
        /// </summary>
        public void ChangeUserData()
        {
            if (_currentUser.Role < Roles.Admin)
            {
                OnProcessCompleted(new ProcessEventArgs("You are not permitted to use this command"));
                return;
            }
            try
            {
                Console.WriteLine("Enter id of user You want to change:");
                var id = Console.ReadLine();
                GetUserById(id);
                Console.WriteLine("Enter new email for user:");
                var email = Console.ReadLine();
                var user = _userService.GetUserById(Convert.ToInt32(id));
                if (!UserDTO.IsValidEmail(email))
                {
                    throw new ArgumentException("Invalid email");
                }
                user.Email = email;
                _userService.UpdateUser(user.Id, user);
                OnProcessCompleted(new ProcessEventArgs());
            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs("Incorrect input"));
            }
        }

        /// <summary>
        /// Method for get user by id.
        /// </summary>
        /// <param name="id">id of user.</param>
        public void GetUserById(string id)
        {
            if (_currentUser.Role < Roles.Admin)
            {
                OnProcessCompleted(new ProcessEventArgs("You are not permitted to use this command"));
                return;
            }
            try
            {
                var user = _userService.GetUserById(Convert.ToInt32(id));
                Console.WriteLine("Registered personal information:");
                Console.WriteLine(user.ToString());
            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs("There is no such user"));
            }
        }

        /// <summary>
        /// Method for logout current user.
        /// </summary>
        public UserDTO Logout()
        {
            if (_currentUser.Role < Roles.Registered)
            {
                OnProcessCompleted(new ProcessEventArgs("You must login first in order to logout"));
                return new UserDTO();
            }
            _currentUser = new UserDTO { Role = Roles.Guest };
            OnProcessCompleted(new ProcessEventArgs());
            return new UserDTO();
        }

        protected virtual void OnProcessCompleted(ProcessEventArgs eventArgs)
        {
            ProcessHandler?.Invoke(this, eventArgs);
        }
    }
}
