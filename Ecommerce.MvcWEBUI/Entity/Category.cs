using System.Collections.Generic;
using Ecommerce.MvcWEBUI.Entity;

namespace Ecommerce.MvcWEBUI.Entity
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}