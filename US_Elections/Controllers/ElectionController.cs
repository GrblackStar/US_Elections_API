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
        private StateService _stateService;

        public ElectionController()
        {
            _service = new ElectionDataService();
            _stateService = new StateService();
        }

        // get all election years
        [HttpGet("years")]
        public ActionResult<IEnumerable<int>> GetAllYears()
        {
            IEnumerable<int> years = _service.GetAllYears();
            return Ok(years);
        }

        // get election data by year
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

            List<State> states = election.States;
            foreach (var state in states)
            {
                state.StateName = _stateService.AbbreviationToName(state.S);
            }

            return Ok(election);
        }

        // get all states
        [HttpGet("states-abbreviation")]
        public ActionResult<IEnumerable<string>> GetAllStates()
        {
            IEnumerable<string> states = _service.GetAllStates();
            return Ok(states);
        }

        // get state information by abbreviation
        [HttpGet("state/{abbreviation}")]
        public ActionResult<State> GetStateByAbbreviation(string abbreviation)
        {
            State state = _service.GetStateByAbbreviation(abbreviation);
            if (state == null)
            {
                return NotFound();
            }
            state.StateName = _stateService.AbbreviationToName(state.S);
            return Ok(state);
        }

        // get the Democratic candidate by year
        [HttpGet("democratic-candidate/{year}")]
        public ActionResult<Candidate> GetDemocraticCandidateByYear(int year)
        {
            Candidate candidate = _service.GetCandidateByYearAndParty(year, "Democrat");
            if (candidate == null)
            {
                return NotFound();
            }
            candidate.ImageFull = $"{Request.Scheme}://{Request.Host}{candidate.Image}";
            return Ok(candidate);
        }

        // get the Republican candidate by year
        [HttpGet("republican-candidate/{year}")]
        public ActionResult<Candidate> GetRepublicanCandidateByYear(int year)
        {
            Candidate candidate = _service.GetCandidateByYearAndParty(year, "Republican");
            if (candidate == null)
            {
                return NotFound();
            }
            candidate.ImageFull = $"{Request.Scheme}://{Request.Host}{candidate.Image}";
            return Ok(candidate);
        }
    }
}
