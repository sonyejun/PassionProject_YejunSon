using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject_YejunSon.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        public string RestaurantName { get; set; }

        public string Location { get; set; }

        public int Rate { get; set; }

        public string Description { get; set; }

        //A restaurant belongs to one user
        //A user can have many restaurants
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual User Users { get; set; }

        //A Restaurant can take of many RestaurantsFolders
        public ICollection<RestaurantsFolder> RestaurantsFolders { get; set; }

    }

    public class RestaurantDto
    {
        public int RestaurantId { get; set; }
        public int RestaurantName { get; set; }
        public int UserId { get; set; }

    }
}