using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Web_App.Models
{
	public class election_city
	{
        public int election_city_id { get; set; }

        [Required, DisplayName("City Name"), MaxLength(20, ErrorMessage = "City Name can't greater than 20 characters.")]
        public string election_city_name { get; set; } = string.Empty;

        [Required, DisplayName("State")]
        public int election_state_id { get; set; }

        [Required, DisplayName("City IsActive"), DefaultValue(true)]
        public bool election_city_active { get; set; } = true;
    }
}

