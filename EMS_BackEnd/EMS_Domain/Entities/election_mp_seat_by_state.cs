using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Domain.Entities
{
	public class election_mp_seat_by_state
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int election_mp_seat_id { get; set; }

        [Required, ForeignKey("election_state")]
        public int election_state_id { get; set; }

        [Required, ForeignKey("election_year_to_date")]
        public int election_id { get; set; }

        [Required]
        public int election_mp_seat_no { get; set; }
    }
    public class election_mp_seat_lists
    {
        public int election_mp_seat_id { get; set; }
        public string election_name { get; set; } = string.Empty;
        public string election_state_name { get; set; } = string.Empty;
        public int election_mp_seat_no { get; set; }
        public bool election_seat_to_edit { get; set; }
    }
}

