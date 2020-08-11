using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QussiaDelivery.Models
{
    public class ItemModel
    {
        public int Item_ID { get; set; }
        public string Item_Name { get; set; }
        public string Item_Desc { get; set; }
        public double Item_Price { get; set; }
        public bool Approval { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<bool> Offer { get; set; }
        public Nullable<double> Percent { get; set; }
        public Nullable<int> Cat_ID { get; set; }
        public Nullable<int> User_ID { get; set; }
        public Nullable<int> Type_ID { get; set; }

        public virtual IEnumerable<ImagesModel> Images { get; set; }
        public virtual IEnumerable<GenderModel> Genders { get; set; }
    }
}