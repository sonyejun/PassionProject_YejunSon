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
            List<RestaurantsFolder> RestaurantsFolders = db.RestaurantsFolders.Where(F=>F.UserId==id).ToList();
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
            Debug.WriteLine(restaurantsFolderDto.RestaurantsFolderId);
            Debug.WriteLine(restaurantsFolderDto.RestaurantsFolderId);
            Debug.WriteLine(restaurantsFolderDto.RestaurantsFolderId);
            return Ok(restaurantsFolderDto);
        }
    }
}
