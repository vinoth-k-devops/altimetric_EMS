using System;
namespace EMS_Domain.Model
{
	public class UserDashBoard
	{
		public bool showPollCount { get; set; } = false;
		public int pollCount { get; set; } = 0;
		public bool PolledByUser { get; set; } = false;
		public List<VotingInfo>? votings { get; set; }
    }
	public class VotingInfo
    {
        public int candidate_id { get; set; }
        public string? candidate_name { get; set; }
        public string? party_name { get; set; }
        public string? symbol { get; set; }
    }
}

