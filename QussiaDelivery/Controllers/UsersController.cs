using ClassLibraryQussia;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QussiaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QussiaDelivery.Controllers
{
    public class UsersController : ApiController
    {
        QussiaDeliveryEntities db = new QussiaDeliveryEntities();
        ModelFactory mf = new ModelFactory();

        public IHttpActionResult getusers()
        {
            return Ok(db.Clients.ToList().Select(m => mf.create(m)));
        }

        [Route("api/Users/createUser")]
        public IHttpActionResult createUser(ClientsModel clientModel)
        {
            HandelErrorObject<Client> handelError = new HandelErrorObject<Client>();
            ClientsModel clientobj = db.Clients.Where(c => c.email == clientModel.email).ToList().Select(c => mf.create(c)).FirstOrDefault();
            try
            {
                if (clientobj == null)
                {
                    Client client = new Client();
                    client.Client_ID = clientModel.Client_ID;
                    client.full_name = clientModel.full_name;
                    client.email = clientModel.email;
                    client.phone_number = clientModel.phone_number;
                    client.password = clientModel.password;
                    client.cash = clientModel.cash;
                    client.block = clientModel.block;
                    client.points = clientModel.points;
                    db.Clients.Add(client);
                    db.SaveChanges();

                    string Clientjson = JsonConvert.SerializeObject(client);
                    JObject jo = JObject.Parse(Clientjson);
                    jo.Property("password").Remove();
                    Client ClientCon = jo.ToObject<Client>();

                    handelError.data = ClientCon;
                    handelError.status = true;
                    handelError.message = "User created successfully";
                    return Ok(handelError);
                }
                else
                {
                    handelError.data = null;
                    handelError.status = false;
                    handelError.message = "Email is used";
                    return Ok(handelError);
                }
               
            }
            catch (Exception e)
            {
                handelError.data = null;
                handelError.status = false;
                handelError.message = e.Message;
                return Ok(handelError);
            }


        }
        [Route("api/Users/login")]
        [HttpPost]
        public IHttpActionResult login([FromBody] Client clientobj)
        {
            HandelErrorObject<ClientsModel> handelError = new HandelErrorObject<ClientsModel>();
            ClientsModel client = db.Clients.Where(c => c.email == clientobj.email).Where(c => c.password == clientobj.password).ToList().Select(c => mf.create(c)).FirstOrDefault();
            if (client != null)
            {
                string Clientjson = JsonConvert.SerializeObject(client);
                JObject jo = JObject.Parse(Clientjson);
                jo.Property("password").Remove();
                ClientsModel ClientCon = jo.ToObject<ClientsModel>();

                handelError.data = ClientCon;
                handelError.status = true;
                handelError.message = "";
                return Ok(handelError);

            }
            else
            {
                handelError.data = null;
                var message = string.Format("Email or Password incorrect");
                handelError.status = false;
                handelError.message = message;
                return Ok(handelError);
            }

        }
    }
}