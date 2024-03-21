using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Domain.Entities
{
	public class election_poll_data
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int election_poll_id { get; set; }

        [Required, ForeignKey("election_year_to_date"), Column(Order = 0)]
        public int election_id { get; set; }

        [Required, ForeignKey("election_user"), Column(Order = 1)]
        public int election_user_id { get; set; }

        [Required, ForeignKey("election_parties"), Column(Order = 2)]
        public int election_party_id { get; set; }

        [Required, ForeignKey("election_state"), Column(Order = 3)]
        public int election_state_id { get; set; }

        [Required, ForeignKey("election_city"), Column(Order = 4)]
        public int election_city_id { get; set; }
    }
}

