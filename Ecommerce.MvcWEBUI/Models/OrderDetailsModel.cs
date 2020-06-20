using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce.MvcWEBUI.Entity;

namespace Ecommerce.MvcWEBUI.Models
{
    public class OrderDetailsModel
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }

        public string AddressTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        public virtual List<OrderLineModel> OrderLines { get; set; }
    }

    public class OrderLineModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

    }
}