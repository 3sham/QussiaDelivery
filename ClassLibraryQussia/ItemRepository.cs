using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryQussia
{
   public class ItemRepository
    {
        QussiaDeliveryEntities db = new QussiaDeliveryEntities();
   
        public void CreateImage(string path , int? itemID)
        {
            Image image = new Image();
            image.Item_ID = itemID;
            image.Image_Path = path;
            db.Images.Add(image);
            db.SaveChanges();
        }
        public void CreateGender_Items(int gender_id, int? itemID)
        {
            Gender g = db.Genders.FirstOrDefault(c => c.Gender_ID == gender_id);

            db.Items.FirstOrDefault(x => x.Item_ID == itemID).Genders.Add(g);
            db.SaveChanges();
        }


    }
}
