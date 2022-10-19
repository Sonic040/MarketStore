using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MarketStore.Models
{
    public partial class Category1
    {
        public Category1()
        {
            CategoryStores = new HashSet<CategoryStore>();
            Products = new HashSet<Product>();
        }

        public decimal CategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public virtual ICollection<CategoryStore> CategoryStores { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
