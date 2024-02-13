using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_YejunSon.Models.ViewModels
{
    public class DatailsRestaurantsFolder
    {
        public RestaurantsFolderDto SelectedFolder { get; set; }
        public IEnumerable<RestaurantDto> FolderRestaurants { get; set;}
    }
}