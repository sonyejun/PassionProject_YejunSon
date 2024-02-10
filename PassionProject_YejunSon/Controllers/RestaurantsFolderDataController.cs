using PassionProject_YejunSon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PassionProject_YejunSon.Controllers
{
    public class RestaurantsFolderDataController : ApiController
    {
        //utilizing the database connection
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all RestaurantsFolders in the system
        /// </summary>
        /// <returns>
        /// HEADER: 201(Ok)
        /// CONTENT: all RestaurantsFolders in the database
        /// </returns>
        /// <example>
        /// GET: api/RestaurantsFolderData/ListRestaurantsFolders/4
        /// </example>
        [HttpGet]
        public IHttpActionResult ListRestaurantsFolders(int id)
        {
            //sending a query to the database
            //select * from RestaurantsFolders...
            List<RestaurantsFolder> RestaurantsFolders = db.RestaurantsFolders.Where(a=>a.UserId==id).ToList();

            //push the results to the list of RestaurantsFolders to return
            return Ok(RestaurantsFolders);
        }
    }
}
