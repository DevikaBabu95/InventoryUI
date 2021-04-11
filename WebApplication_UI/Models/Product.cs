using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_UI.Models
{
    public class Product
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Action { get; set; }
        public string Output { get; set; }
        public List<Product> ProInfo { get; set; }
    }

    public class Pdetail
    {

        public Product Pro;
        public string Output { get; set; }
        public List<Product> ProInfo { get; set; }

    }
    
}