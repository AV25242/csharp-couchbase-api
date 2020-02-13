using Couchbase.Api.Models;
using Couchbase.Api.Repository;
using Microsoft.AspNetCore.Mvc;


namespace Couchbase.Api.Services
{
    [ApiController]
    [Route("[controller]")]
    public class BreweryService : Controller
    {
        private readonly BreweryRepository _repository;

        public BreweryService(BreweryRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Route("api/GetBrewery/{id}")]
        public IActionResult GetBrewery(string id)
        {
            var result = _repository.GetBrewery(id);

            return Ok(result);
        }     
        [HttpPost]
        [Route("api/AddBrewery")]
        public IActionResult AddBrewery([FromBody] Brewery brewery)
        {
            if (string.IsNullOrEmpty(brewery.Id))
            {
                return Ok("No Id for brewery.  No action taken.");
            }

            var result = _repository.AddBrewery(brewery.Id, brewery);

            return Ok(result);
        }

        [HttpPost]
        [Route("api/EditBrewery")]
        public IActionResult EditBrewery([FromBody] Brewery brewery)
        {
            if (string.IsNullOrEmpty(brewery.Id))
            {
                return Ok("No Id for brewery.  No action taken.");
            }

            var result = _repository.EditBrewery(brewery.Id, brewery);

            return Ok(result);
        }

        [HttpDelete]
        [Route("api/DeleteBrewery/{id}")]
        public IActionResult DeleteBrewery(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Ok("No Id for brewery.  No action taken.");
            }

            var result = _repository.DeleteBrewery(id);

            return Ok(result);
        }

     }
}
