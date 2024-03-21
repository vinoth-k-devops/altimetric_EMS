using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EMS_Web_App.Models
{
	public class election_state
	{
        public int election_state_id { get; set; }

        [Required, DisplayName("State Name")]
        public string election_state_name { get; set; } = string.Empty;

        [Required, DefaultValue(true), DisplayName("State IsActive")]
        public bool election_state_active { get; set; } = true;
    }
}

