using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Security.Policy;
using PassionProject_YejunSon.Models;
using PassionProject_YejunSon.Models.ViewModels;
using PassionProject_YejunSon.Migrations;

namespace PassionProject_YejunSon.Controllers
{
    public class UserController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static UserController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44360/api/");
        }

        // GET: User/List
        // Objecttive: a webpage that lists the users in our system
        public ActionResult List()
        {
            // GET {resource}/api/userdata/listusers
            // https://localhost:44360/api/UserData/ListUsers
            // use HTTP client to access information

            string url = "UserData/ListUsers";

            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<User> Users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;

            return View(Users);
        }

        // GET: User/New
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            // objective: add a new user into our system using the API
            // curl -H "Content-Tpye:application/json" -d @user.json https://localhost:44360/api/UserData/AddUser
            string url = "UserData/AddUser";

            string jsonpayload = jss.Serialize(user);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("error");
            }
        }

        
        // GET: User/Details/4
        public ActionResult Details(int id)
        { 
            DetailsUser ViewModel = new DetailsUser();

            //objective: communicate with our user data api to retrieve one user
            //curl https://localhost:44360/api/UserData/FindUser/{id}

            string url = "UserData/FindUser/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            User SelectedUser = response.Content.ReadAsAsync<User>().Result;

            ViewModel.SelectedUser = SelectedUser;

            //RestaurantsFolders
            url = "RestaurantsFolderData/ListRestaurantsFolders/"+id;
            response = client.GetAsync(url).Result;
            IEnumerable<RestaurantsFolderDto> RestaurantsFolders = response.Content.ReadAsAsync<IEnumerable<RestaurantsFolderDto>>().Result;

            ViewModel.RegisteredRestaurantsFolders = RestaurantsFolders;

            //Restaurants
            url = "RestaurantData/ListRestaurants/" + id;
            response = client.GetAsync(url).Result;

            IEnumerable<RestaurantDto> RegisteredRestaurants = response.Content.ReadAsAsync<IEnumerable<RestaurantDto>>().Result;

            ViewModel.RegisteredRestaurants = RegisteredRestaurants;

            return View(ViewModel);
        }
 
    }
}