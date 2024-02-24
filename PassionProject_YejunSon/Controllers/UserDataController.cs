using PassionProject_YejunSon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Diagnostics;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace PassionProject_YejunSon.Controllers
{
    public class UserDataController : ApiController
    {
        //utilizing the database connection
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all users in the system
        /// </summary>
        /// <returns>
        /// HEADER: 201(Ok)
        /// CONTENT: all users in the database
        /// </returns>
        /// <example>
        /// GET: api/UserData/ListUsers
        /// </example>
        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult ListUsers()
        {
            //sending a query to the database
            //select * from Users...
            List<User> Users = db.Users.ToList();


            return Ok(Users);
        }

        /// <summary>
        /// Adds a user to the system
        /// </summary>
        /// <param name="user">JSON FORM DATA of a user</param>
        /// <returns>
        /// HEADER: 201(Created)
        /// CONTENT: USER ID, USER Data
        /// or
        /// HEADER: 400(Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/UserData/AddUser
        /// </example>
        [ResponseType(typeof(User))]
        [HttpPost]
        public IHttpActionResult AddUser(User user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        /// <summary>
        /// return a user to the system
        /// </summary>
        /// <returns>
        /// HEADER: 200(Ok)
        /// CONTENT: A user in the system matching up to the user id primary key
        /// or
        /// HEADER: 404(Not Found)
        /// </returns>
        /// <param name="id">The primary key of the user</param>
        /// <example>
        /// Get: api/UserData/FindUser/4
        /// </example>
        [ResponseType(typeof(User))]
        [HttpGet]
        public IHttpActionResult FindUser(int id)
        {
            User user = db.Users.Find(id);
           
            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Updates a particular User in the system with POST Data input
        /// </summary>
        /// <param name="id">Users the User ID primary key</param>
        /// <param name="User">JSON FORM DATA of a User</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/UserData/UpdateUser/5
        /// FORM DATA: User JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateUser(int id, User User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != User.UserId)
            {
                return BadRequest();
            }

            db.Entry(User).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        /// <summary>
        /// Deletes a User from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the User</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/UserData/DeleteUser/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(User))]
        [HttpPost]
        public IHttpActionResult DeleteUser(int id)
        {
            User User = db.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }

            db.Users.Remove(User);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}
