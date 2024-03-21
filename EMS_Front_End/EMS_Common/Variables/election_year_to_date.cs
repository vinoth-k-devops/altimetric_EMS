using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EMS_Common.Variables
{
	public class election_year_to_date
	{
        public int election_id { get; set; }

        [Required, DisplayName("Election Name"), MaxLength(30, ErrorMessage = "Election Name can't greater than 30 characters.")]
        public string election_name { get; set; } = string.Empty;

        [Required, DisplayName("Election Start Date")]
        public DateTime election_start_date_time { get; set; }

        [Required, DisplayName("Election End Date")]
        public DateTime election_end_date_time { get; set; }

        [DefaultValue(1)]
        public int election_current_status { get; set; } = 1;
    }
}

