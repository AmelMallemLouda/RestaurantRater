﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; } // The primary key

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
        public double Rating
        {
            get
            {
                double totalAverageRating = 0;

                //Add all Ratings
                foreach (var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }

                //get average from total
                return Ratings.Count > 0
                    ? Math.Round(totalAverageRating / Ratings.Count, 2)// If Ratings.Count>0
                    : 0;//If Ratings.Count not >0
            }
        }

        public bool isRecommended
        {
            get
            {
                return Rating > 8;
            }
        }


        //Avrage Rating
        //Average Cleanliness Rating
        //Average Envirpnment Rating
    }
}