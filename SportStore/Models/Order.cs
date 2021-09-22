using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Please, enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, enter the first address line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        [Required(ErrorMessage = "Please, enter the city name")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please, enter a state name")]
        public string State { get; set; }
        public string Zip { get; set; }
        [Required(ErrorMessage = "Please, enter a country name")]
        public string Country { get; set; }
        public bool GiftWarp { get; set; }

        [BindNever]
        public bool Snipped { get; set; }

        [BindNever]
        public virtual ICollection<CartLine> Lines { get; set; } = new HashSet<CartLine>();
    }
}
