using MvcWebApiTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using HtmlAgilityPack;


namespace MvcWebApiTest.Controllers
{
    [RoutePrefix("api/v1/reactions")]
    public class ProductsController : ApiController
    {
        Product[] products = new Product[] 
        { 
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 }, 
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M }, 
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M } 
        };

        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return products;
        //}

        //[System.Web.Mvc.HttpGet]
        //public HttpResponseMessage Get()
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, products);
        //}

        [Route("")]
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return products;
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [Route("crawler")]
        [HttpGet]
        public HttpResponseMessage Crawler()
        {
            String Rstring;
            WebRequest myWebRequest;
            WebResponse myWebResponse;
            String URL = "http://www.londonrentmyhouse.com";

            myWebRequest = WebRequest.Create(URL);
            myWebResponse = myWebRequest.GetResponse();//Returns a response from an Internet resource

            Stream streamResponse = myWebResponse.GetResponseStream();//return the data stream from the internet
            //and save it in the stream

            StreamReader sreader = new StreamReader(streamResponse);//reads the data stream
            Rstring = sreader.ReadToEnd();//reads it to the end
            String Links = GetImages(Rstring);//gets the links only

            streamResponse.Close();
            sreader.Close();
            myWebResponse.Close();

            
            
            
            
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }


        private String GetImages(String Rstring)
        {
            String sString = "";
            HtmlDocument d = new HtmlDocument();
            List<string> images = new List<string>();

            d.LoadHtml(Rstring);

            foreach(HtmlNode img in d.DocumentNode.Descendants().Where(n => n.Name == "img")) {

                images.Add(img.OuterHtml);

            }





            return sString;
        }


    }
}
