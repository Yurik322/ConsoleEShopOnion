using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OA_DataAccess.Entities
{
    /// <summary>
    /// User entity model.
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [Required]
        public Roles Role { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Email: {Email}, Role: {Role}";
        }

        public bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
    }
}
