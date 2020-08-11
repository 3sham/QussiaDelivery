using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QussiaDelivery.Models
{
    public class HandelCategoriesError
    {
        public IEnumerable<CategoryModel> data { get; set; }
        public bool status { get; set; }
        public string error { get; set; }
    }
}