using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QussiaDelivery.Models
{
    public class CategoryModel
    {
        public int Cat_ID { get; set; }
        public string En_Name { get; set; }
        public string Ar_Name { get; set; }
        public string Img_Path { get; set; }

        public IEnumerable<TypesModel> Types { get; set; }
        public IEnumerable<GenderModel> Genders { get; set; }
    }
}