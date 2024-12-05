using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; } 
        public Order Order { get; set; } 

        public int ProductId { get; set; } 
        public Product Product { get; set; } 

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } 

    }
}
