using PassionProject_YejunSon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PassionProject_YejunSon.Controllers
{
    public class RestaurantDataController : ApiController
    {
        //utilizing the database connection
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Restaurants in the system
        /// </summary>
        /// <returns>
        /// HEADER: 201(Ok)
        /// CONTENT: all Restaurants in the database
        /// </returns>
        /// <example>
        /// GET: api/RestaurantData/ListRestaurants/4
        /// </example>
        [HttpGet]
        public IHttpActionResult ListRestaurants(int id)
        {
            //sending a query to the database
            //select * from Restaurants...
            List<Restaurant> Restaurants = db.Restaurants.Where(a => a.UserId == id).ToList();

            //push the results to the list of Restaurants to return
            return Ok(Restaurants);
        }
    }
}
