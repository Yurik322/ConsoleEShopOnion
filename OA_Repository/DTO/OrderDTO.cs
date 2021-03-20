using System.Text.RegularExpressions;

namespace OA_Repository.DTO
{
    /// <summary>
    /// Data transfer object for order model.
    /// </summary>
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        public static bool IsName(string s)
        {
            if (s == null)
            {
                return false;
            }

            s = s.Replace("-", "")
                .Replace(" ", "")
                .ToUpper();

            return Regex.IsMatch(s, @"^[a-zA-Z0-9]+$");
        }

        public override string ToString()
        {
            return $"Id: {Id}, Registered Id: {UserId}, Status: {Status}, Total price to pay: {TotalPrice}";
        }
    }
}
