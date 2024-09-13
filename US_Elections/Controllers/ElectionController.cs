namespace US_Elections.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using US_Elections.Models;
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
        public ActionResult<IEnumerable<int>> GetAllYears()
        {
            IEnumerable<int> years = _service.GetAllYears();
            return Ok(years);
        }

        // Endpoint to get election data by year
        [HttpGet("year/{year}")]
        public ActionResult<Election> GetElectionByYear(int year)
        {
            Election election = _service.GetElectionByYear(year);
            if (election == null)
                return NotFound();

            List<Candidate> candidates = election.Candidates;
            foreach (var candidate in candidates)
            {
                candidate.ImageFull = $"{Request.Scheme}://{Request.Host}{candidate.Image}";
            }

            return Ok(election);
        }

        // Endpoint to get all states
        [HttpGet("states")]
        public ActionResult<IEnumerable<string>> GetAllStates()
        {
            IEnumerable<string> states = _service.GetAllStates();
            return Ok(states);
        }

        // Endpoint to get state information by abbreviation
        [HttpGet("state/{abbreviation}")]
        public ActionResult<State> GetStateByAbbreviation(string abbreviation)
        {
            State state = _service.GetStateByAbbreviation(abbreviation);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
    }
}
