using System;
namespace EMS_Web_App.Models
{
	public class ElectionInput
	{
        public int election_id { get; set; }
    }
    public class ElectionResult
    {
        public int candidate_id { get; set; }
        public string? candidate_name { get; set; }
        public string? party_name { get; set; }
        public string? symbol { get; set; }
        public int pollcount { get; set; }
        public bool election_winner { get; set; }
    }
}

