using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassLibraryQussia;
namespace QussiaDelivery.Models
{
    public class UsersSecurity
    {
       
        public static bool Login(string username , string password)
        {
            QussiaDeliveryEntities db = new QussiaDeliveryEntities();
            return db.Users.Any(user => user.User_Name.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);

        }
    }
}