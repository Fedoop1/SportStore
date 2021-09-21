using System.ComponentModel.DataAnnotations.Schema;

namespace SportStore.Models
{
    [NotMapped]
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
