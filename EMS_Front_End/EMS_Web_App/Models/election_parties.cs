using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Web_App.Models
{
	public class election_parties
	{
        public int election_party_id { get; set; }

        [Required, DisplayName("Party Name"), MaxLength(40, ErrorMessage = "Party Name can't greater than 40 characters.")]
        public string election_party_name { get; set; } = string.Empty;

        [Required, DisplayName("Party Symbol")]
        public int election_sym_id { get; set; }

        [Required, DefaultValue(true)]
        public bool election_party_active { get; set; } = true;
    }
}

