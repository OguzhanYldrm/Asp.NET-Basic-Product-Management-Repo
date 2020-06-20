using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.MvcWEBUI.Entity
{
    public enum EnumOrderState
    {
        [Display(Name = "Waiting Approval")]
        Waiting,
        [Display(Name = "Order Completed")]
        Completed
    }
}