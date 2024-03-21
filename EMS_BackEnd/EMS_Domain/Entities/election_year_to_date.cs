using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_Domain.Entities
{
	public class election_year_to_date
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int election_id { get; set; }

		[Required, MaxLength(30, ErrorMessage = "Election Name can't greater than 30 characters.")]
		public string election_name { get; set; } = string.Empty;

		[Required]
		public DateTime election_start_date_time { get; set; }

		[Required]
        public DateTime election_end_date_time { get; set; }

        [DefaultValue(1)]
        public int election_current_status { get; set; }
	}
}

