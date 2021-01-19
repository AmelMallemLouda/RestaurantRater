using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        //Post(create)
        //api/Restaurant
        [HttpPost]//Specifies that it's a Poste method
        public async Task<IHttpActionResult> CreateRestaurant([FromBody]Restaurant model)//The HttpActionResult will encapsulate all the response types provided by the apiController class.
        {

            if (model is null)
            {
                return BadRequest("Your request body cannot be empty");

            }

            //If thr model is valid
            if (ModelState.IsValid)
            {
                //Store the model in the database
                _context.Restaurants.Add(model);

               int changeCount=await _context.SaveChangesAsync();// How many Changes were made
                return Ok("Your restaurant was created");
            }
            //The model is not valid,go ahead and reject it
            return BadRequest(ModelState);
        } 

        //Get All
        //api/Restaurant

        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurant = await _context.Restaurants.ToListAsync();
            return Ok(restaurant);
        }

        //Get By ID'
        //api/Restaurant/{id}
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)

        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if(restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }

        //Put (Update)
        //api/restaurant/{id}
        [HttpPut]
       
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri]int id,[FromBody] Restaurant updatedRestaurant)
        {
            //Chech the Ids if they match
            if(id != updatedRestaurant?.Id)//if the UpdatedRestaurant is not null(?)then chech if the ids match
            {
                return BadRequest("Ids do not match");
            }

            //Check the modelState
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Find the restaurant in the database

            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            //if the restaurant does not exist then do something 
            if (restaurant is null)
                return NotFound();

            //Update the Propreties 

            restaurant.Name = updatedRestaurant.Name;
            restaurant.Address = updatedRestaurant.Address;
           

            //Save the changes
            await _context.SaveChangesAsync();
            return Ok("The restaurant was updated");

        }


        //Delete
        //api/Restaurant/{id}
        [HttpDelete]
        public async Task<IHttpActionResult>DeleteRestault([FromUri]int Id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(Id);
            if (restaurant is null)
                return NotFound();

            _context.Restaurants.Remove(restaurant);
            if(await _context.SaveChangesAsync() == 1)
            {
                return Ok("The restaurant was deletd.");
            }
            return InternalServerError();

        }
    }

}
