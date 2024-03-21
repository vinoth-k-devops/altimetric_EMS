using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EMS_Domain.Model
{
	public class LoginModel
	{
        [Required, DisplayName("User Name")]
        public string? UserName { get; set; }

        [Required, DisplayName("Password")]
        public string? Password { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

