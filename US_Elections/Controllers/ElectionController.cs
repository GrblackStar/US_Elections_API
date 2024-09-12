namespace US_Elections.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using US_Elections.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class ElectionController : ControllerBase
    {
        private readonly ElectionDataService _service;

        public ElectionController()
        {
            _service = new ElectionDataService();
        }

        // Endpoint to get all election years
        [HttpGet("years")]
        public IActionResult GetAllYears()
        {
            var years = _service.GetAllYears();
            return Ok(years);
        }

        // Endpoint to get election data by year
        [HttpGet("year/{year}")]
        public IActionResult GetElectionByYear(int year)
        {
            var election = _service.GetElectionByYear(year);
            if (election == null)
            {
                return NotFound();
            }
            return Ok(election);
        }

        // Endpoint to get all states
        [HttpGet("states")]
        public IActionResult GetAllStates()
        {
            var states = _service.GetAllStates();
            return Ok(states);
        }

        // Endpoint to get state information by abbreviation
        [HttpGet("state/{abbreviation}")]
        public IActionResult GetStateByAbbreviation(string abbreviation)
        {
            var state = _service.GetStateByAbbreviation(abbreviation);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
    }
}
