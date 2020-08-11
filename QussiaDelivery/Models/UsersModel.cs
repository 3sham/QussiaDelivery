using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QussiaDelivery.Models
{
    public class UsersModel
    {
        public int User_ID { get; set; }
        public string Name { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int Code_Reset { get; set; }
        public bool Block { get; set; }
        public Nullable<double> Cash { get; set; }
        public Nullable<int> Point { get; set; }
    }
}