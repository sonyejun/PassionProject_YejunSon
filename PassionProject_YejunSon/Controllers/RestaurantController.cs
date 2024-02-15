using PassionProject_YejunSon.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        // GET: Restaurant/Details/3/4
        public ActionResult Details(int id, int id2)
        {
            //objective: communicate with our user data api to retrieve one Folder
            //curl https://localhost:44360/api/RestaurantData/FindRestaurant/{id}/{restaurantid}

            string url = "RestaurantData/FindRestaurant/" + id + "/" + id2;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RestaurantDto RestaurantDto = response.Content.ReadAsAsync<RestaurantDto>().Result;
            Debug.WriteLine(RestaurantDto.RestaurantName);
            Debug.WriteLine(RestaurantDto.Location);
            return View(RestaurantDto);
        }

        // GET: Restaurant/New/3
        public ActionResult New(int id)
        {
            ViewBag.UserId = id;
            return View();
        }

        // Post: Restaurant/Create
        [HttpPost]
        public ActionResult Create(Restaurant Restaurant)
        {
            // objective: add a new user into our system using the API
            // curl -H "Content-Tpye:application/json" -d @user.json https://localhost:44360/api/RestaurantData/AddRestaurant
            string url = "RestaurantData/AddRestaurant";

            string jsonpayload = jss.Serialize(Restaurant);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", "User", new { id = Restaurant.UserId });
            }
            else
            {
                return RedirectToAction("error");
            }
        }

        // GET: Restaurant/DeleteConfirm/5/3
        public ActionResult DeleteConfirm(int id, int id2)
        {
            //objective: communicate with our user data api to retrieve one Restaurant
            //curl https://localhost:44360/api/RestaurantData/FindRestaurant/{id}/{restaurantid}
            string url = "RestaurantData/FindRestaurant/" + id + "/" + id2;
            HttpResponseMessage response = client.GetAsync(url).Result;

            RestaurantDto RestaurantDto = response.Content.ReadAsAsync<RestaurantDto>().Result;

            return View(RestaurantDto);
        }

        // POST: Restaurant/Delete/5/5
        [HttpPost]
        public ActionResult Delete(int id, int id2)
        {
            string url = "RestaurantData/DeleteRestaurant/" + id2;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", "User", new { id = id });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Restaurant/Edit/5/3
        public ActionResult Edit(int id, int id2)
        {
            //objective: communicate with our user data api to retrieve one Restaurant
            //curl https://localhost:44360/api/RestaurantData/FindRestaurant/{id}/{restaurantid}
            string url = "RestaurantData/FindRestaurant/" + id + "/" + id2;
            HttpResponseMessage response = client.GetAsync(url).Result;

            RestaurantDto RestaurantDto = response.Content.ReadAsAsync<RestaurantDto>().Result;

            return View(RestaurantDto);
        }

        // POST: Restaurant/Update/5
        [HttpPost]
        public ActionResult Update(int id, Restaurant Restaurant)
        {
            string url = "RestaurantData/UpdateRestaurant/" + id;
            string jsonpayload = jss.Serialize(Restaurant);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("details/"+ Restaurant.UserId + '/' + Restaurant.RestaurantId);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}