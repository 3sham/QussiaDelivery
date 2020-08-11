using ClassLibraryQussia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QussiaDelivery.Models
{
    public class ModelFactory
    {
        private QussiaDeliveryEntities db = new QussiaDeliveryEntities();
        public CategoryModel create(Category category)
        {
            return new CategoryModel()
            {
                Cat_ID = category.Cat_ID,
                Ar_Name = category.Ar_Name,
                En_Name = category.En_Name,
                Img_Path = category.Img_Path,
                Genders = category.Genders.Select(h => create(h)),
                Types = category.Types.Select(s => create(s))
            };
        }
       
        public ItemModel create(Item item)
        {

           
               return new ItemModel()
                {
                    Item_ID = item.Item_ID,
                    Item_Name = item.Item_Name,
                    Item_Desc = item.Item_Desc,
                    Item_Price = item.Item_Price,
                    Discount = item.Discount,
                    Offer = item.Offer,
                    Approval = item.Approval,
                    Percent = item.Percent,
                    Cat_ID = item.Cat_ID,
                    Type_ID = item.Type_ID,
                    User_ID = item.User_ID,
                    Genders = item.Genders.Select(s=>create(s)),
                    Images = item.Images.Select(h=>create(h))

                };
            
            
        }
        public ImagesModel create (Image image)
        {


            return new ImagesModel()
            {
                Img_ID = image.Img_ID,
                Image_Path = image.Image_Path
            };
           
        }

        public GenderModel create(Gender gender)
        {
            return new GenderModel()
            {
                Gender_ID = gender.Gender_ID,
                Gender_Name_AR = gender.Gender_Name_AR,
                Gender_Name_EN = gender.Gender_Name_EN
            };
        }
        public TypesModel create(ClassLibraryQussia.Type type)
        {
            return new TypesModel()
            {
                Type_ID = type.Type_ID,
                Type_Name_AR = type.Type_Name_AR,
                Type_Name_EN = type.Type_Name_EN
            };
        }
        public ClientsModel create(Client client)
        {
            return new ClientsModel()
            {
                Client_ID = client.Client_ID,
                full_name = client.full_name,
                email = client.email,
                phone_number = client.phone_number,
                password = client.password,
                cash = client.cash,
                block = client.block,
                points = client.points
            };

        }
        public UsersModel create(User user)
        {
            return new UsersModel()
            {
                User_ID = user.User_ID,
                User_Name =user.User_Name,
                Name = user.Name,
                Password = user.Password,
                Phone = user.Phone,
                Code_Reset = user.Code_Reset,
                Cash = user.Cash,
                Point = user.Point,
                Block = user.Block
                
                
            };

        }
    }
}