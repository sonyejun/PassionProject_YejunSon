using PassionProject_YejunSon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PassionProject_YejunSon.Controllers
{
    public class RestaurantController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static RestaurantController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44360/api/");
        }
        // GET: Restaurant/Details
        public ActionResult Details(int id, int id2)
        {
            //objective: communicate with our user data api to retrieve one Folder
            //curl https://localhost:44360/api/RestaurantsFolder/FindRestaurantsFolder/{id}/{restaurantid}

            string url = "RestaurantData/FindRestaurant/" + id + "/" + id2;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RestaurantDto RestaurantDto = response.Content.ReadAsAsync<RestaurantDto>().Result;

            return View(RestaurantDto);
        }
    }
}