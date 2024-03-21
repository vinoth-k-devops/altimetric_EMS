using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Domain.Entities
{
	public class election_symbols
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int election_sym_id { get; set; }

        [Required, MaxLength(20, ErrorMessage = "Symbol Name can't greater than 20 characters.")]
        public string election_sym_name { get; set; } = string.Empty;

        [Required, MaxLength(200, ErrorMessage = "Symbol Font Name can't greater than 200 characters.")]
        public string election_sym_font_name { get; set; } = string.Empty;

        [Required, DefaultValue(true)]
        public bool election_sym_active { get; set; }
    }
}

