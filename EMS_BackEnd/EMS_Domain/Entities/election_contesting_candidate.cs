using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Domain.Entities
{
	public class election_contesting_candidate
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int election_contest_id { get; set; }

        [Required, ForeignKey("election_year_to_date")]
        public int election_id { get; set; }

        [Required, ForeignKey("election_parties")]
        public int election_party_id { get; set; }

        [Required, ForeignKey("election_user")]
        public int election_user_id { get; set; }

        [Required, ForeignKey("election_state")]
        public int election_state_id { get; set; }

        [Required, ForeignKey("election_city")]
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

