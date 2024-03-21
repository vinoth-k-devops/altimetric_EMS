using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Domain.Entities
{
	public class election_result
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int election_result_id { get; set; }

        [Required, ForeignKey("election_year_to_date")]
        public int election_id { get; set; }

        [Required, ForeignKey("election_state")]
        public int election_state_id { get; set; }

        [Required, ForeignKey("election_city")]
        public int election_city_id { get; set; }

        [Required, ForeignKey("election_parties")]
        public int election_party_id { get; set; }

        [Required]
        public int election_poll_count { get; set; }

        [Required, DefaultValue(false)]
        public bool election_winner_by_state_wise { get; set; }
    }
}

