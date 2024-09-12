namespace US_Elections.Services
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using US_Elections.Models;

    public class ElectionDataService
    {
        private readonly List<Election> _elections;

        public ElectionDataService()
        {
            var json = File.ReadAllText("Data/electionData.json");
            _elections = JsonConvert.DeserializeObject<List<Election>>(json);
        }

        public IEnumerable<int> GetAllYears() => _elections.Select(e => e.Year);

        public Election GetElectionByYear(int year) => _elections.FirstOrDefault(e => e.Year == year);

        public IEnumerable<string> GetAllStates() => _elections.SelectMany(e => e.States).Select(s => s.S).Distinct();

        public State GetStateByAbbreviation(string abbreviation) =>
            _elections.SelectMany(e => e.States).FirstOrDefault(s => s.S.Equals(abbreviation, StringComparison.OrdinalIgnoreCase));
    }
}
