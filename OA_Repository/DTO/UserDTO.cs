using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using OA_DataAccess.Entities;

namespace OA_Repository.DTO
{
    /// <summary>
    /// Data transfer object for user model.
    /// </summary>
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [Required]
        public Roles Role { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Email: {Email}, Role: {Role}";
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
    }
}
