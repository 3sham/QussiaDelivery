using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QussiaDelivery.Models
{
    public class ClientsModel
    {
        public int Client_ID { get; set; }
        public string full_name { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<double> cash { get; set; }
        public Nullable<int> points { get; set; }
        public Nullable<bool> block { get; set; }
    }
}