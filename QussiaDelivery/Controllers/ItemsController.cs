using ClassLibraryQussia;
using QussiaDelivery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Data.Entity;


namespace QussiaDelivery.Controllers
{
    public class ItemsController : ApiController
    {
        ItemRepository IR = new ItemRepository();
        ModelFactory mf = new ModelFactory();
        QussiaDeliveryEntities db = new QussiaDeliveryEntities();

        [System.Web.Http.Route("api/items/GetItemsByCategoryID/{id}")]
        public IHttpActionResult GetItemsByCategoryID(int id)
        {
            List<ItemModel> item = db.Items.Where(c => c.Cat_ID == id).Where(a=>a.Approval == true).ToList().Select(c => mf.create(c)).ToList();
            HandelError<ItemModel> handelError = new HandelError<ItemModel>();

            if (item.Count() > 0)
            {

                handelError.data = item;
                handelError.status = true;
                handelError.message = "";
                return Ok(handelError);


            }
            else
            {
                handelError.data = null;
                var message = string.Format("Category with id = {0} not found", id);
                handelError.status = false;
                handelError.message = message;
                return Ok(handelError);
            }


        }

        [System.Web.Http.Route("api/items/GetItemsFilter/{id?}/{typeID?}/{genderID?}")]
        public IHttpActionResult GetItemsFilter(int? id = null, int? typeID = null, int? genderID = null )
        {
            List<ItemModel> item = db.Items.Where(x => x.Cat_ID == id && (typeID == null || x.Type_ID == typeID) ).ToList().Select(c => mf.create(c)).ToList();
            //List<ItemModel> item = db.Items.Include(b => b.Genders).Where(x => x.Cat_ID == id && (typeID == null || x.Type_ID == typeID) && (genderID == null || x.Genders.Select(d => d.Gender_ID).FirstOrDefault() == genderID)).ToList().Select(c => mf.create(c)).ToList();
            HandelError<ItemModel> handelError = new HandelError<ItemModel>();

            if (item.Count() > 0)
            {
                
                handelError.data = item;
                handelError.status = true;
                handelError.message = "";
                return Ok(handelError);


            }
            else
            {
                handelError.data = null;
                var message = string.Format("Category with id = {0} not found", id);
                handelError.status = false;
                handelError.message = message;
                return Ok(handelError);
            }


        }

        [ResponseType(typeof(Item))]
        public HttpResponseMessage CreateItem(ItemModel itemModel)
        {
            HandelErrorObject<Item> handelError = new HandelErrorObject<Item>();
            try
            {
                Item item = new Item();
                item.Item_Name = itemModel.Item_Name;
                item.Item_Desc = itemModel.Item_Desc;
                item.Item_Price = itemModel.Item_Price;
                item.Offer = itemModel.Offer;
                item.Percent = itemModel.Percent;
                item.Type_ID = itemModel.Type_ID;
                item.User_ID = itemModel.User_ID;
                item.Cat_ID = itemModel.Cat_ID;
                item.Approval = itemModel.Approval;
                item.Discount = itemModel.Discount;
                db.Items.Add(item);
                db.SaveChanges();
                foreach (var obj in itemModel.Images.ToList())
                {
                    IR.CreateImage(obj.Image_Path, item.Item_ID);
                }
                foreach (var obj in itemModel.Genders.ToList())
                {
                    IR.CreateGender_Items(obj.Gender_ID, item.Item_ID);
                }
                handelError.data = item;
                handelError.status = true;
                handelError.message = "";
                return Request.CreateResponse(HttpStatusCode.OK, handelError);
            }
            catch (Exception e)
            {
                handelError.data = null;
                handelError.status = false;
                handelError.message = e.Message;
                return Request.CreateResponse(HttpStatusCode.NotFound, handelError);
            }

            
            
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/upload-image")]
        public async Task<HttpResponseMessage> UploadImage()
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/Images");
            var provider = new MultipartFormDataStreamProvider(root);
            List<string> ImagesPath = new List<string>();
            HandelError<string> HandelImage = new HandelError<string>();
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var file in provider.FileData)
                {
                    string datemilli = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds().ToString();
                    var name =file.Headers.ContentDisposition.FileName ;
                    name = datemilli + name.Trim('"');

                    var localfilename = file.LocalFileName;
                    var filepath = Path.Combine(root, name);
                    //ImagesID.Add(SavaFilePath(localfilename, filepath));
                    var ImagePath = "http://gerges96-001-site1.dtempurl.com/Images/" + name;
                    ImagesPath.Add(ImagePath);
                    File.Move(localfilename, filepath);
                    if (File.Exists(localfilename))
                        File.Delete(localfilename);
                }
                HandelImage.data = ImagesPath;
                HandelImage.status = true;
                HandelImage.message = "";
                return Request.CreateResponse(HttpStatusCode.OK, HandelImage);
            }
            catch(Exception e)
            {
                HandelImage.data = null;
                HandelImage.status = false;
                HandelImage.message = e.Message;
                return Request.CreateResponse(HttpStatusCode.NotFound, HandelImage);
            }
          
        }

        //private int SavaFilePath (string localfile , string filepath)
        //{

        //    File.Move(localfile, filepath);
        //    var image = new Image()
        //    {
        //        Image_Path = filepath,
                
        //    };
        //    db.Images.Add(image);
        //    db.SaveChanges();
        //    return image.Img_ID;
        //}
    }
}
