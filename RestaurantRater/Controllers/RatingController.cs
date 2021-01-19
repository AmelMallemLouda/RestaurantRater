using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RatingController : ApiController
    {

        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        public CancellationToken RestaurantId { get; private set; }

        //Create new ratings

        //Post api/rating
        [HttpPost]

        public async Task<IHttpActionResult> CreateRating([FromBody] Rating model)
        {
            // Check if model is null
            if (model is null)
                return BadRequest("Your request body cannot be empty,");


            //Check if ModelState is Invalid

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Find the restaurant by the model.Restaurant and see that it exists

            var restaurantEntity = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurantEntity is null)
                return BadRequest($"The target restaurant with the ID of{model.RestaurantId} does not exist ");
            //Create the Reting

            //Add to the rating table
           // _context.Ratings.Add(model);

            //add to the Restaurant Entity
            restaurantEntity.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
                return Ok($"You rated restaurant {restaurantEntity.Name} successfully!");
            return InternalServerError();
        }
        //Get a rating by its ID
        //Get All Ratings
        //Get All Ratings for a specific restaurant by restaurant ID

        //Update a Rating
        //Delete a Rating
    }
}
