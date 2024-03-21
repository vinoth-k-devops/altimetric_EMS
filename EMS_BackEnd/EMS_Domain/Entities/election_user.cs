using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Domain.Entities
{
	public class election_user
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int election_user_id { get; set; }

        [Required, MaxLength(10, ErrorMessage = "Voter Id can't greater than 10 characters.")]
        public string election_voter_id { get; set; } = string.Empty;

        [Required, MaxLength(30, ErrorMessage = "Voter Name can't greater than 30 characters.")]
        public string election_voter_name { get; set; } = string.Empty;

        [Required]
        public string election_address { get; set; } = string.Empty;

        [Required, DefaultValue("U")]
        public string election_user_type { get; set; } = "U";

        [Required, ForeignKey("election_state")]
        public int election_state_id { get; set; }

        [Required, ForeignKey("election_city")]
        public int election_city_id { get; set; }

        [Required, StringLength(9, ErrorMessage = "Password length should be 9.")]
        public string election_voter_password { get; set; } = string.Empty;

        [DefaultValue(false)]
        public bool election_user_approve_status { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

