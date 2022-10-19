using System;
using System.Collections.Generic;

#nullable disable

namespace MarketStore.Models
{
    public partial class Home
    {
        public Home()
        {
            User1s = new HashSet<User1>();
        }

        public decimal Homeid { get; set; }
        public string Uses { get; set; }
        public string Parghraph { get; set; }
        public string Img { get; set; }

        public virtual ICollection<User1> User1s { get; set; }
    }
}
