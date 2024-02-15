using PassionProject_YejunSon.Migrations;
using PassionProject_YejunSon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

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
            List<Restaurant> Restaurants = db.Restaurants.Where(R => R.UserId == id).ToList();
            List<RestaurantDto> RestaurantDtos = new List<RestaurantDto>();

            Restaurants.ForEach(R => RestaurantDtos.Add(new RestaurantDto()
            {
                RestaurantId = R.RestaurantId,
                RestaurantName = R.RestaurantName,
                Location = R.Location,
                Rate = R.Rate,
                Description = R.Description,
                UserId = R.UserId
            }));
            //push the results to the list of Restaurants to return
            return Ok(RestaurantDtos);
        }

        /// <summary>
        /// Grthers informatioon about Restaurants related to a particular Folder
        /// </summary>
        /// <returns>
        /// HEADER: 200(Ok)
        /// CONTENT: Returns all restaurants in a database associated with a particular folder
        /// </returns>
        /// <example>
        /// GET: api/RestaurantData/ListRestaurantsforFolder/4
        /// </example>
        [HttpGet]
        public IHttpActionResult ListRestaurantsforFolder(int id)
        {
            //sending a query to the database
            //select * from Restaurants...
            List<Restaurant> Restaurants = db.Restaurants.Where(
                R => R.RestaurantsFolders.Any(
                    F=>F.RestaurantsFolderId == id
                )).ToList();

            List<RestaurantDto> RestaurantDtos = new List<RestaurantDto>();

            Restaurants.ForEach(R => RestaurantDtos.Add(new RestaurantDto()
            {
                RestaurantId = R.RestaurantId,
                RestaurantName = R.RestaurantName,
                Location = R.Location,
                Rate = R.Rate,
                Description = R.Description,
                UserId = R.UserId
            }));

            //push the results to the list of Restaurants to return
            return Ok(RestaurantDtos);
        }

        /// <summary>
        /// return a Restaurant to the system
        /// </summary>
        /// <returns>
        /// HEADER: 200(Ok)
        /// CONTENT: A Restaurant in the system matching up to the Restaurant id primary key
        /// or
        /// HEADER: 404(Not Found)
        /// </returns>
        /// <param name="id">The primary key of the Restaurant</param>
        /// <example>
        /// Get: api/RestaurantData/FindRestaurant/3/4
        /// </example>
        [ResponseType(typeof(Restaurant))]
        [HttpGet]
        public IHttpActionResult FindRestaurant(int id, int id2)
        {
            Restaurant Restaurant = db.Restaurants.Where(
                R => R.UserId == id && R.RestaurantId == id2)
            .FirstOrDefault();

            if (Restaurant == null)
            {
                return NotFound();
            }

            RestaurantDto RestaurantDto = new RestaurantDto()
            {
                RestaurantId = Restaurant.RestaurantId,
                RestaurantName = Restaurant.RestaurantName,
                Location = Restaurant.Location,
                Rate = Restaurant.Rate,
                Description = Restaurant.Description,
                UserId = Restaurant.UserId
            };
            return Ok(RestaurantDto);
        }

        /// <summary>
        /// Add a Restaurant to the system
        /// </summary>
        /// <param name="Restaurant">JSON FORM DATA of a Restaurant</param>
        /// <returns>
        /// HEADER: 201(Created)
        /// CONTENT: Restaurant ID, Restaurant Data
        /// or
        /// HEADER: 400(Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/RestaurantData/AddRestaurant
        /// </example>
        [ResponseType(typeof(Restaurant))]
        [HttpPost]
        public IHttpActionResult AddRestaurant(Restaurant Restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Restaurants.Add(Restaurant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Restaurant.UserId }, Restaurant);
        }

        /// <summary>
        /// Deletes a Restaurant from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Restaurant</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/RestaurantData/DeleteRestaurant/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Restaurant))]
        [HttpPost]
        public IHttpActionResult DeleteRestaurant(int id)
        {
            Restaurant Restaurant = db.Restaurants.Find(id);
            if (Restaurant == null)
            {
                return NotFound();
            }

            db.Restaurants.Remove(Restaurant);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Updates a particular Restaurant in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Restaurant ID primary key</param>
        /// <param name="Restaurant">JSON FORM DATA of an Restaurant</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/RestaurantData/UpdateRestaurant/5
        /// FORM DATA: Species JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateRestaurant(int id, Restaurant Restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != Restaurant.RestaurantId)
            {
                return BadRequest();
            }

            db.Entry(Restaurant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool RestaurantExists(int id)
        {
            return db.Restaurants.Count(e => e.RestaurantId == id) > 0;
        }
    }
}
