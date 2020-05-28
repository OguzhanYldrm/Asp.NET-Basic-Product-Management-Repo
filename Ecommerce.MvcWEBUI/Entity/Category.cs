using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ecommerce.MvcWEBUI.Entity;

namespace Ecommerce.MvcWEBUI.Entity
{
    public class Category
    {
        public int Id { get; set; }


        [DisplayName("Category Name")]
        [StringLength(maximumLength:15,ErrorMessage = "Maximum 15 character is allowed.")]
        public string Name { get; set; }
        [DisplayName("Category Description")]
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}