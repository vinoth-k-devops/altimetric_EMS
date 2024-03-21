using System.Security.Claims;
using EMS_Common;
using EMS_Domain.EMS;
using EMS_Domain.Model;
using EMS_Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EMS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly EMSContext _userContext;
        private readonly ITokenService _tokenService;
        private readonly JWTSettings _jwtSettings;

        public AuthController(EMSContext userContext, ITokenService tokenService, IOptions<JWTSettings> options)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _jwtSettings = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }
            var user = _userContext.election_users.FirstOrDefault(u =>
                (u.election_voter_id == loginModel.UserName) && (u.election_voter_password == loginModel.Password)
                && (u.election_user_approve_status == true));

            if (user is null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.election_voter_id),
                new Claim(ClaimTypes.Surname, user.election_voter_name),
                new Claim(ClaimTypes.Role, user.election_user_type)
            };

            string UName = user.election_voter_name;
            var accessToken = await _tokenService.GenerateAccessToken(_jwtSettings, claims);
            var refreshToken = await _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(7);
            _userContext.SaveChanges();

            return Ok(new AuthenticatedResponse
            {
                UName = UName,
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost]
        public async Task<IActionResult> Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiModel.AccessToken!;
            string refreshToken = tokenApiModel.RefreshToken!;

            var principal = await _tokenService.GetPrincipalFromExpiredToken(_jwtSettings, accessToken);

            var username = principal.Identity!.Name!; //this is mapped to the Name claim by default

            var user = _userContext.election_users.SingleOrDefault(u => u.election_voter_id == username);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime > DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = await _tokenService.GenerateAccessToken(_jwtSettings, principal.Claims);
            var newRefreshToken = await _tokenService.GenerateRefreshToken();
            var UName = user.election_voter_name;

            user.RefreshToken = newRefreshToken;
            _userContext.SaveChanges();

            return Ok(new AuthenticatedResponse()
            {
                UName = UName,
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<string>> Revoke()
        {
            var username = User.Identity!.Name;
            var user = await _userContext.election_users.FirstOrDefaultAsync(u => u.election_voter_id == username);

            if (user == null)
                return BadRequest();

            user.RefreshToken = null;
            _userContext.SaveChanges();

            return Common.LogOut;
        }
    }
}

