using assignment4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace BigDealsDigestMVC.API
{
    [Authorize]
    public class DealsController : ApiController
    {
        // GET api/<controller>
        public List<EditableProduct> Get()
        {
            using (Assignment4Context context = new Assignment4Context())
            {

                var isAdmin = this.User.IsInRole("Admin");

                var userId = ((ClaimsPrincipal)this.User).FindFirst(ClaimTypes.NameIdentifier).Value;
                var products = context.Products.Select(t => new EditableProduct { Id = t.Id, AddedDate = t.AddedDate, ApplicationUserId = t.ApplicationUserId, Payable=t.Payable, Description = t.Description, Name = t.Name, IsEditable = isAdmin }).ToList();
                if (!isAdmin)
                {
                    foreach (EditableProduct product in products)
                    {
                        if (product.ApplicationUserId == userId)
                            product.IsEditable = true;
                    }
                }
                return products;
            }
        }

        // GET api/<controller>/5
        public EditableProduct Get(int id)
        {
            var userId = ((ClaimsPrincipal)this.User).FindFirst(ClaimTypes.NameIdentifier).Value;
            using (Assignment4Context context = new Assignment4Context())
            {//add payable 
                var product = context.Products.Find(id);
                EditableProduct eProduct = new EditableProduct { AddedDate = product.AddedDate, ApplicationUserId = product.ApplicationUserId, Description = product.Description, Id = product.Id, Name = product.Name, Payable=product.Payable, IsEditable = false, Version = product.Timestamp };
                if (User.IsInRole("Admin") || (product.ApplicationUserId == userId))
                {
                    eProduct.IsEditable = true;
                }
                return eProduct;
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Product pProduct)
        {
            try
            {

                var userId = ((ClaimsPrincipal)this.User).FindFirst(ClaimTypes.NameIdentifier).Value;
                using (Assignment4Context context = new Assignment4Context())
                {
                    if (pProduct.Id == 0)
                    {
                        pProduct.AddedDate = DateTime.Now;
                        pProduct.ApplicationUserId = userId;
                        context.Products.Add(pProduct);
                        context.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Product Added." });
                    }
                    else
                    {
                        var product = context.Products.Find(pProduct.Id);
                        if (User.IsInRole("Admin") || (product.ApplicationUserId == userId))
                        {
                            product.Name = pProduct.Name;
                            product.Description = pProduct.Description;
                            product.Payable = pProduct.Payable;
                            context.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Product Updated." });
                        }

                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Product not added/updated." });

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Error Occurred. Scary Details:" + e.Message });
            }
        }



        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var userId = ((ClaimsPrincipal)this.User).FindFirst(ClaimTypes.NameIdentifier).Value;
                using (Assignment4Context context = new Assignment4Context())
                {
                    Product product = context.Products.Find(id);
                    if (User.IsInRole("Admin") || (product.ApplicationUserId == userId))
                    {
                        context.Products.Remove(product);
                        context.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Product Deleted." });

                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Product Not Deleted. Not authorized." });

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Error Occurred. Scary Details:" + e.Message });
            }
        }
    }
}