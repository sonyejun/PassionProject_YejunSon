using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject_YejunSon.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        //A user can take care of many restaurants
        public ICollection<Restaurant> Restaurants { get; set; }

        //A user can take care of many restaurantsFolders
        public ICollection<RestaurantsFolder> RestaurantsFolders { get; set; }

        internal bool Any(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}