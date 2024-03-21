using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Domain.Entities
{
	public class election_state
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int election_state_id { get; set; }

        [Required, MaxLength(20, ErrorMessage = "State Name can't greater than 20 characters.")]
        public string election_state_name { get; set; } = string.Empty;

        [Required, DefaultValue(true)]
        public bool election_state_active { get; set; }

    }
}

