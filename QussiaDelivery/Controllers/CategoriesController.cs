using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QussiaDelivery.Models;
using ClassLibraryQussia;
namespace QussiaDelivery.Controllers
{
    public class CategoriesController : ApiController
    {
        ModelFactory mf = new ModelFactory();
        //CategoryRepository cp = new CategoryRepository();
        QussiaDeliveryEntities db = new QussiaDeliveryEntities();
        // GET: api/Categories
        public HttpResponseMessage GetCategories()
        {
            
            
            HandelError<CategoryModel> handelError = new HandelError<CategoryModel>();
            var categories = db.Categories.ToList().Select(c => mf.create(c));
            if (categories.Count() > 0 )
            {
                handelError.data = categories;
                handelError.status = true;
                handelError.message = "";
                return Request.CreateResponse(HttpStatusCode.OK, handelError);
                
            }
            else
            {
                handelError.data = null;
                var message = string.Format("Don't found any categories");
                handelError.status = false;
                handelError.message = message;
              
                return Request.CreateResponse(HttpStatusCode.NotFound, handelError);
            }

           
             
        }

        // GET: api/Categories/5
        //[ResponseType(typeof(Category))]
        
        public IHttpActionResult GetCategory(int id)
        {
            HandelErrorObject<CategoryModel> handelError = new HandelErrorObject<CategoryModel>();
            CategoryModel category = db.Categories.Where(c => c.Cat_ID == id).ToList().Select(c => mf.create(c)).FirstOrDefault();
            if (category != null)
            {
                handelError.data = category;
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
    }
}
