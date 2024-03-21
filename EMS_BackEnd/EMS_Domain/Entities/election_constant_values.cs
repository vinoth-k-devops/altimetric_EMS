using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EMS_Domain.Entities
{
	public class election_constant_values
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int election_constant_id { get; set; }

		public int election_constant_key { get; set; } 

        public string election_constant_value { get; set; } = string.Empty;

		public int group_id { get; set; }   
	}
}

