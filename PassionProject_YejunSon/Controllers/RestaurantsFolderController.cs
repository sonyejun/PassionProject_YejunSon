using PassionProject_YejunSon.Migrations;
using PassionProject_YejunSon.Models;
using PassionProject_YejunSon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
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
            //curl https://localhost:44360/api/RestaurantsFolderData/FindRestaurantsFolder/{id}/{RestaurantsFolderId}

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

        // GET: RestaurantsFolder/New/3
        public ActionResult New(int id)
        {
            //objective: communicate with our user data api to retrieve all user
            //curl https://localhost:44360/RestaurantData/ListRestaurants/{id}
            //Restaurants
            string url = "RestaurantData/ListRestaurants/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<RestaurantDto> Restaurants = response.Content.ReadAsAsync<IEnumerable<RestaurantDto>>().Result;

            ViewBag.UserId = id;
            return View(Restaurants);
        }

        // Post: RestaurantsFolder/Create
        [HttpPost]
        public ActionResult Create(RestaurantsFolder RestaurantsFolder, int[] RestaurantIds)
        {
            // objective: add a new user into our system using the API
            // curl -H "Content-Tpye:application/json" -d @user.json https://localhost:44360/api/RestaurantsFolderData/AddRestaurantsFolder
            string url = "RestaurantsFolderData/AddRestaurantsFolder";

            string jsonpayload = jss.Serialize(RestaurantsFolder);
            
            HttpContent content = new StringContent(jsonpayload);
            
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                string folderData = response.Content.ReadAsStringAsync().Result;
                RestaurantsFolderDto createdFolder = jss.Deserialize<RestaurantsFolderDto>(folderData);
                int createdFolderId = createdFolder.RestaurantsFolderId;

                url = "RestaurantsFolderData/AssociateFolderWithRestaurants/"+createdFolderId;
                jsonpayload = jss.Serialize(RestaurantIds);
                content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";
                response = client.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", "User", new { id = RestaurantsFolder.UserId });
                }
                else
                {
                    return RedirectToAction("error");
                }
            }
            else
            {
                return RedirectToAction("error");
            }
        }

        // GET: RestaurantsFolder/DeleteConfirm/5/3
        public ActionResult DeleteConfirm(int id, int id2)
        {
            DatailsRestaurantsFolder ViewModel = new DatailsRestaurantsFolder();

            //objective: communicate with our user data api to retrieve one Folder
            //curl https://localhost:44360/api/RestaurantsFolderData/FindRestaurantsFolder/{id}/{RestaurantsFolderId}

            string url = "RestaurantsFolderData/FindRestaurantsFolder/" + id + "/" + id2;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RestaurantsFolderDto SelectedFolder = response.Content.ReadAsAsync<RestaurantsFolderDto>().Result;

            ViewModel.SelectedFolder = SelectedFolder;

            //Show all Restaurants under the care of this Restaurant's Folder
            url = "RestaurantData/ListRestaurantsforFolder/" + id2;
            response = client.GetAsync(url).Result;
            IEnumerable<RestaurantDto> FolderRestaurants = response.Content.ReadAsAsync<IEnumerable<RestaurantDto>>().Result;

            ViewModel.FolderRestaurants = FolderRestaurants;
            return View(ViewModel);
        }

        // POST: RestaurantsFolder/Delete/5/5
        [HttpPost]
        public ActionResult Delete(int id, int id2)
        {
            string url = "RestaurantsFolderData/DeleteRestaurantsFolder/" + id2;
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

        // GET: RestaurantsFolder/Edit/5/3
        public ActionResult Edit(int id, int id2)
        {
            EditRestaurantsFolder ViewModel = new EditRestaurantsFolder();

            //objective: communicate with our user data api to retrieve one Folder
            //curl https://localhost:44360/api/RestaurantsFolderData/FindRestaurantsFolder/{id}/{RestaurantsFolderId}

            string url = "RestaurantsFolderData/FindRestaurantsFolder/" + id + "/" + id2;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RestaurantsFolderDto SelectedFolder = response.Content.ReadAsAsync<RestaurantsFolderDto>().Result;

            ViewModel.SelectedFolder = SelectedFolder;

            //Show all Restaurants under the care of this Restaurant's Folder
            url = "RestaurantData/ListRestaurantsforFolder/" + id2;
            response = client.GetAsync(url).Result;
            IEnumerable<RestaurantDto> FolderRestaurants = response.Content.ReadAsAsync<IEnumerable<RestaurantDto>>().Result;

            ViewModel.FolderRestaurants = FolderRestaurants;

            //Restaurants
            url = "RestaurantData/ListRestaurants/" + id;
            response = client.GetAsync(url).Result;

            IEnumerable<RestaurantDto> RegisteredRestaurants = response.Content.ReadAsAsync<IEnumerable<RestaurantDto>>().Result;

            ViewModel.RegisteredRestaurants = RegisteredRestaurants;

            return View(ViewModel);
        }

        // POST: RestaurantsFolder/Update/5
        [HttpPost]
        public ActionResult Update(int id, RestaurantsFolder RestaurantsFolder, int[] RestaurantIds)
        {
            string url = "RestaurantsFolderData/UpdateRestaurantsFolder/" + id;
            string jsonpayload = jss.Serialize(RestaurantsFolder);
            HttpContent content = new StringContent(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                string folderData = response.Content.ReadAsStringAsync().Result;
                RestaurantsFolderDto createdFolder = jss.Deserialize<RestaurantsFolderDto>(folderData);
                int createdFolderId = createdFolder.RestaurantsFolderId;

                url = "RestaurantsFolderData/UnAssociateFolderWithRestaurants/" + createdFolderId;
                response = client.PostAsync(url, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    url = "RestaurantsFolderData/AssociateFolderWithRestaurants/" + createdFolderId;
                    jsonpayload = jss.Serialize(RestaurantIds);
                    content = new StringContent(jsonpayload);
                    content.Headers.ContentType.MediaType = "application/json";
                    response = client.PostAsync(url, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Details/" + RestaurantsFolder.UserId+"/"+ RestaurantsFolder.RestaurantsFolderId);
                    }
                    else
                    {
                        return RedirectToAction("error");
                    }
                }
                else
                {
                    return RedirectToAction("error");
                }
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}