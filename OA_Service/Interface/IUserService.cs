using System.Collections.Generic;
using OA_Repository.DTO;

namespace OA_Service.Interface
{
    /// <summary>
    /// Interface for getting methods from user service.
    /// </summary>
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO GetUserById(int id);
        UserDTO Register(string email);
        void UpdateUser(int id, UserDTO user);
        UserDTO Login(string email);
    }
}
