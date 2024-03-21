using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Domain.Entities
{
	public class election_parties
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int election_party_id { get; set; }

        [Required, MaxLength(40, ErrorMessage = "Party Name can't greater than 40 characters.")]
        public string election_party_name { get; set; } = string.Empty;

        [Required, ForeignKey("election_symbols")]
        public int election_sym_id { get; set; }

        [Required, DefaultValue(true)]
        public bool election_party_active { get; set; }
    }
}

