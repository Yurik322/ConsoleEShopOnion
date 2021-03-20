namespace OA_Repository.DTO
{
    /// <summary>
    /// Data transfer object for product model.
    /// </summary>
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Category: {Category}, Description: {Description}, Price: {Price}";
        }
    }
}
