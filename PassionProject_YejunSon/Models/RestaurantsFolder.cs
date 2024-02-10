using PassionProject_YejunSon.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject_YejunSon.Models
{
    public class RestaurantsFolder
    {
        [Key]
        public int RestaurantsFolderId { get; set; }

        public string FolderName { get; set; }

        //A RestaurantsFolder belongs to one user
        //A user can have many RestaurantsFolders
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual User Users { get; set; }

        //A RestaurantsFolder can take of many Restaurants
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}