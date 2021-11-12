using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                // save to db, register user
                var createdUser = await _userService.RegisterUser(model);
                // 201 created
                return Ok(createdUser);
            }
            //400
            return BadRequest("Please check the data you entered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel model)
        {
            var user = await _userService.ValidateUser(model.Email, model.Password);
            if (user == null)
            {
                // invalid un/PW
                return Unauthorized();
            }
            // UN/PW valid
            // create JWT and sent it to client(Angular), add claims info in the token
            return Ok(new {token = GenerateJWT(user)});
        }

        private string GenerateJWT(UserLoginResponseModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            };
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // get the secret key for signing the token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));
            
            // specify the algorithm to sign the token
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("ExpirationHours"));
            
            // creating the token System.IdentityModel.Tokens.Jwt
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _configuration["issuer"],
                Audience = _configuration["Audience"]
            };

            var encodedJwt = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(encodedJwt);
        }
            
        }
}