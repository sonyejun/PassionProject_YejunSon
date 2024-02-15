using PassionProject_YejunSon.Migrations;
using PassionProject_YejunSon.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

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
            List<RestaurantsFolder> RestaurantsFolders = db.RestaurantsFolders.Where(F => F.UserId == id).ToList();
            List<RestaurantsFolderDto> RestaurantsFolderDtos = new List<RestaurantsFolderDto>();

            RestaurantsFolders.ForEach(F => RestaurantsFolderDtos.Add(new RestaurantsFolderDto()
            {
                RestaurantsFolderId = F.RestaurantsFolderId,
                FolderName = F.FolderName,
                UserId = F.UserId
            }));
            //push the results to the list of RestaurantsFolders to return
            return Ok(RestaurantsFolderDtos);
        }

        /// <summary>
        /// return a folder to the system
        /// </summary>
        /// <returns>
        /// HEADER: 200(Ok)
        /// Retrieve a folder from the system matching the primary key (RestaurantsFolderId) and UserId
        /// or
        /// HEADER: 404(Not Found)
        /// </returns>
        /// <param name="id">The primary key of the folder</param>
        /// <example>
        /// Get: api/RestaurantsFolderData/FindRestaurantsFolder/4/5
        /// </example>
        [ResponseType(typeof(RestaurantsFolder))]
        [HttpGet]
        public IHttpActionResult FindRestaurantsFolder(int id, int id2)
        {
            RestaurantsFolder RestaurantsFolder = db.RestaurantsFolders
            .Where(folder => folder.RestaurantsFolderId == id2 && folder.UserId == id)
            .FirstOrDefault();

            if (RestaurantsFolder == null)
            {
                return NotFound();
            }

            RestaurantsFolderDto restaurantsFolderDto = new RestaurantsFolderDto()
            {
                RestaurantsFolderId = RestaurantsFolder.RestaurantsFolderId,
                FolderName = RestaurantsFolder.FolderName,
                UserId = RestaurantsFolder.UserId
            };

            return Ok(restaurantsFolderDto);
        }

        /// <summary>
        /// Adds a RestaurantsFolder to the system
        /// </summary>
        /// <param name="RestaurantsFolder">JSON FORM DATA of a RestaurantsFolder</param>
        /// <returns>
        /// HEADER: 201(Created)
        /// CONTENT: RestaurantsFolderID, RestaurantsFolder Data
        /// or
        /// HEADER: 400(Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/RestaurantsFolderData/AddRestaurantsFolder
        /// </example>
        [ResponseType(typeof(RestaurantsFolder))]
        [HttpPost]
        public IHttpActionResult AddRestaurantsFolder(RestaurantsFolder RestaurantsFolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Debug.WriteLine(RestaurantsFolder);
            db.RestaurantsFolders.Add(RestaurantsFolder);
            db.SaveChanges();
            Debug.WriteLine(RestaurantsFolder.RestaurantsFolderId);
            return CreatedAtRoute("DefaultApi", new { id = RestaurantsFolder.RestaurantsFolderId }, RestaurantsFolder);
        }

        /// <summary>
        /// Associates a particular RestaurantsFolder with a particular Restaurants
        /// </summary>
        /// <param name="folderid">The RestaurantsFolderID primary key</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: Restaurantids
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST api/RestaurantsFolderData/AssociateFolderWithRestaurants/3
        /// </example>
        [HttpPost]
        [System.Web.Http.Route("api/RestaurantsFolderData/AssociateFolderWithRestaurants/{folderid}")]
        public IHttpActionResult AssociateFolderWithRestaurants(int folderid, [FromBody] int[] restaurantids)
        {
            RestaurantsFolder SelectedFolder = db.RestaurantsFolders.Include(F => F.Restaurants).Where(F => F.RestaurantsFolderId == folderid).FirstOrDefault();
            if (SelectedFolder == null)
            {
                return NotFound();
            };

            if(restaurantids != null)
            {
                foreach (var restaurantid in restaurantids)
                {
                    Restaurant SelectedRestaurant = db.Restaurants.Find(restaurantid);
                    if (SelectedRestaurant == null)
                    {
                        return NotFound();
                    }
                    //SQL equivalent:
                    //insert into RestaurantsFolderRestaurants (RestaurantsFolderId, RestaurantsId) values ({RestaurantsFolderId},{RestaurantsId})
                    SelectedFolder.Restaurants.Add(SelectedRestaurant);
                }
            }
            

            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Removes an association between a particular RestaurantsFolder with a particular Restaurants
        /// </summary>
        /// <param name="folderid">The RestaurantsFolderID primary key</param>
        /// <returns>
        /// CONTENT: Restaurantids
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST api/AnimalData/AssociateAnimalWithKeeper/9/1
        /// </example>
        [HttpPost]
        [Route("api/RestaurantsFolderData/UnAssociateFolderWithRestaurants/{folderid}")]
        public IHttpActionResult UnAssociateFolderWithRestaurants(int folderid)
        {
            RestaurantsFolder SelectedFolder = db.RestaurantsFolders.Include(F => F.Restaurants).Where(F => F.RestaurantsFolderId == folderid).FirstOrDefault();
            if (SelectedFolder == null)
            {
                return NotFound();
            };
            var restaurantsToRemove = new List<Restaurant>(SelectedFolder.Restaurants);
            foreach (var restaurant in restaurantsToRemove)
            {
                //SQL equivalent:
                //Delete data from the Restaurants folder where all Restaurants folderId is folderId in the Restaurants folder
                SelectedFolder.Restaurants.Remove(restaurant);
            }
            
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Deletes a RestaurantsFolder from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the RestaurantsFolder</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/RestaurantsFolderData/DeleteRestaurantsFolder/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Restaurant))]
        [HttpPost]
        public IHttpActionResult DeleteRestaurantsFolder(int id)
        {
            RestaurantsFolder RestaurantsFolder = db.RestaurantsFolders.Find(id);
            if (RestaurantsFolder == null)
            {
                return NotFound();
            }

            db.RestaurantsFolders.Remove(RestaurantsFolder);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Updates a particular RestaurantsFolder in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the RestaurantsFolder ID primary key</param>
        /// <param name="RestaurantsFolder">JSON FORM DATA of an RestaurantsFolder</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/RestaurantsFolderData/UpdateRestaurantsFolder/5
        /// FORM DATA: Species JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateRestaurantsFolder(int id, RestaurantsFolder RestaurantsFolder)
        {
            Debug.WriteLine("111111111111");
            Debug.WriteLine(id);
            Debug.WriteLine(RestaurantsFolder.RestaurantsFolderId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != RestaurantsFolder.RestaurantsFolderId)
            {
                return BadRequest();
            }
            
            
            Debug.WriteLine("2222222222222");
            db.Entry(RestaurantsFolder).State = EntityState.Modified;
            Debug.WriteLine("333333333333");
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

            return CreatedAtRoute("DefaultApi", new { id = RestaurantsFolder.RestaurantsFolderId }, RestaurantsFolder);
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
