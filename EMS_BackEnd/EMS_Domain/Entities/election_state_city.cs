using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Domain.Entities
{
	public class election_city
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int election_city_id { get; set; }

        [Required, MaxLength(20, ErrorMessage = "City Name can't greater than 20 characters.")]
        public string election_city_name { get; set; } = string.Empty;

        [Required, ForeignKey("election_state")]
        public int election_state_id { get; set; }

        [Required, DefaultValue(true)]
        public bool election_city_active { get; set; }
    }
}

