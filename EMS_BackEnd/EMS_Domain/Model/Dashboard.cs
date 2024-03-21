using System;
using EMS_Domain.Entities;

namespace EMS_Domain.Model
{
	public class Dashboard
	{
		public string total_user_count { get; set; } = string.Empty;

        public string parties_count { get; set; } = string.Empty;

        public string candidate_count { get; set; } = string.Empty;

        public string awaiting_approval_count { get; set; } = string.Empty;

        public List<e_user>? approval_user { get; set; }
    }
    public class e_user
    {
        public int election_user_id { get; set; }
        public string election_voter_id { get; set; } = string.Empty;
        public string election_voter_name { get; set; } = string.Empty;
        public string election_state_name { get; set; } = string.Empty;
        public string election_city_name { get; set; } = string.Empty;
    }
}

