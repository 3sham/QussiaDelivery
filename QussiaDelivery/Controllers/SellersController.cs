using ClassLibraryQussia;
using QussiaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QussiaDelivery.Controllers
{
    public class SellersController : ApiController
    {
        QussiaDeliveryEntities db = new QussiaDeliveryEntities();
        ModelFactory mf = new ModelFactory();

        [Route("api/Sellers/login")]
        [HttpPost]
        public IHttpActionResult login(string username, string password)
        {
            HandelErrorObject<UsersModel> handelError = new HandelErrorObject<UsersModel>();
            UsersModel user = db.Users.Where(c => c.User_Name == username).Where(c => c.Password == password).ToList().Select(c => mf.create(c)).FirstOrDefault();
           
            if (user != null)
            {
                if (user.Block == true)
                {
                    handelError.data = null;
                    var message = string.Format("User is blocked");
                    handelError.status = false;
                    handelError.message = message;
                    return Ok(handelError);
                }
                else
                {
                    handelError.data = user;
                    handelError.status = true;
                    handelError.message = "";
                    return Ok(handelError);
                }
                    

            }
            else
            {
                handelError.data = null;
                var message = string.Format("User name or Password incorrect");
                handelError.status = false;
                handelError.message = message;
                return Ok(handelError);
            }
            
            

        }
        [Route("api/Sellers/ChangePassword")]
        [HttpPost]
        public IHttpActionResult ChangePassword(int userid, int codereset, string Newpassword)
        {
            HandelErrorObject<UsersModel> handelError = new HandelErrorObject<UsersModel>();
            UsersModel user = db.Users.AsNoTracking().Where(c => c.User_ID == userid).ToList().Select(c => mf.create(c)).FirstOrDefault();
            if (user != null)
            {
                if (codereset == user.Code_Reset)
                {
                    user.Password = Newpassword;
                    User u = new User()
                    {
                        User_ID = user.User_ID,
                        User_Name = user.User_Name,
                        Name = user.Name,
                        Password = user.Password,
                        Phone = user.Phone,
                        Code_Reset = user.Code_Reset,
                        Cash = user.Cash,
                        Point = user.Point,
                        Block = user.Block
                    };
                    //db.Entry(user).State = EntityState.Detached;
                    db.Entry(u).State = EntityState.Modified;
                    db.SaveChanges();
                    handelError.data = user;
                    handelError.status = true;
                    handelError.message = "";
                    return Ok(handelError);

                }
                else
                {
                    handelError.data = null;
                    var message = string.Format("Code Reset is incorrect");
                    handelError.status = false;
                    handelError.message = message;
                    return Ok(handelError);
                }

            }
            else
            {
                handelError.data = null;
                var message = string.Format("User ID incorrect");
                handelError.status = false;
                handelError.message = message;
                return Ok(handelError);
            }

        }
    }
}
