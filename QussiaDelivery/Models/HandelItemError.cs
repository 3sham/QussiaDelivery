using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QussiaDelivery.Models
{
    public class HandelItemError
    {
        public IEnumerable<ItemModel> data { get; set; }
        public bool status { get; set; }
        public string error { get; set; }

    }
}