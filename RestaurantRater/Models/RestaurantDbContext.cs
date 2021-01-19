using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    public class RestaurantDbContext:DbContext  // will be creating DataBase through the DbContext class and make changes
    {
        //The Class base is DbContext
        public RestaurantDbContext():base("DefaultConnection")
        {

        }


        // The DbSet represent a collection of entities in the context
        public DbSet<Restaurant> Restaurants { get; set; } // The restaurant will have a table inside the database

        public DbSet <Rating> Ratings { get; set; }
    }
}