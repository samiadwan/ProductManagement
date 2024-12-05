using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
