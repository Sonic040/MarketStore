using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MarketStore.Models
{
    public partial class Store1
    {
        public Store1()
        {
            CategoryStores = new HashSet<CategoryStore>();
        }

        public decimal Storeid { get; set; }
        public string Namestore { get; set; }
        public string Img { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public virtual ICollection<CategoryStore> CategoryStores { get; set; }
    }
}
