using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_YejunSon.Models.ViewModels
{
    public class DetailsUser
    {
        public User SelectedUser { get; set; }
        public IEnumerable<RestaurantDto> RegisteredRestaurants{ get; set;}
        public IEnumerable<RestaurantsFolderDto> RegisteredRestaurantsFolders { get; set;}
    }
}