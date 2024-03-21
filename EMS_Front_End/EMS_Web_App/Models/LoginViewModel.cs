using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EMS_Web_App.Models
{
	public class LoginViewModel
	{
        [Required, DisplayName("User Name")]
        public string? UserName { get; set; } = string.Empty;

        [Required, DisplayName("Password")]
        public string? Password { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }

    public class Login_Register_ViewModel
    {
        public LoginViewModel? LoginViewModel { get; set; }
        public election_user? RegisterViewModel { get; set; }
    }
}

