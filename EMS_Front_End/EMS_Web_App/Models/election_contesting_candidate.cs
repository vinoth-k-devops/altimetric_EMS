using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Web_App.Models
{
    public class election_contesting_candidate
    {
        public int election_contest_id { get; set; }

        [Required, DisplayName("Election Name")]
        public int election_id { get; set; }

        [Required, DisplayName("Party Name")]
        public int election_party_id { get; set; }

        [Required, DisplayName("Candidate Name")]
        public int election_user_id { get; set; }

        [Required, DisplayName("Election State")]
        public int election_state_id { get; set; }

        [Required, DisplayName("Election City")]
        public int election_city_id { get; set; }
    }
    public class election_contesting_candidate_list
    {
        public int election_contest_id { get; set; }
        public string election_name { get; set; } = string.Empty;
        public string election_party_name { get; set; } = string.Empty;
        public string election_voter_name { get; set; } = string.Empty;
        public string election_state_name { get; set; } = string.Empty;
        public string election_city_name { get; set; } = string.Empty;
        public bool election_canditure_edit { get; set; } = false;

    }
}

