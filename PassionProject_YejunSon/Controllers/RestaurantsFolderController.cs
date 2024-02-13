using PassionProject_YejunSon.Models;
using PassionProject_YejunSon.Models.ViewModels;
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
    public class RestaurantsFolderController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static RestaurantsFolderController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44360/api/");
        }

        // GET: RestaurantsFolder/Details/3/7
        public ActionResult Details(int id, int id2)
        {
            DatailsRestaurantsFolder ViewModel = new DatailsRestaurantsFolder();

            //objective: communicate with our user data api to retrieve one Folder
            //curl https://localhost:44360/api/RestaurantsFolder/FindRestaurantsFolder/{id}/{RestaurantsFolderId}

            string url = "RestaurantsFolderData/FindRestaurantsFolder/" + id + "/" + id2;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RestaurantsFolderDto SelectedFolder = response.Content.ReadAsAsync<RestaurantsFolderDto>().Result;

            ViewModel.SelectedFolder = SelectedFolder;

            //Show all Restaurants under the care of this Restaurant's Folder
            url = "RestaurantData/ListRestaurantsforFolder/" + id2;
            response = client.GetAsync(url).Result;
            IEnumerable<RestaurantDto> FolderRestaurants = response.Content.ReadAsAsync< IEnumerable<RestaurantDto>>().Result;

            ViewModel.FolderRestaurants = FolderRestaurants;
            return View(ViewModel);
        }
    }
}