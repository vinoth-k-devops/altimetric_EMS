using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EMS_Web_App.Models
{
	public class election_symbols
	{
        public int election_sym_id { get; set; }

        [Required, DisplayName("Symbol Name"), MaxLength(20, ErrorMessage = "Symbol Name can't greater than 20 characters.")]
        public string election_sym_name { get; set; } = string.Empty;

        [Required, DisplayName("Symbol Font Name"), MaxLength(200, ErrorMessage = "Symbol Font Name can't greater than 200 characters.")]
        public string election_sym_font_name { get; set; } = string.Empty;

        [Required, DisplayName("Symbol IsActive"), DefaultValue(true)]
        public bool election_sym_active { get; set; } = true;
    }
}

