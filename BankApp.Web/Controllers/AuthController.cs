using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BankApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Domain.User> userManager;
        private readonly SignInManager<Domain.User> signInManager;

        public AuthController(UserManager<Domain.User> userManager, SignInManager<Domain.User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("token")]
        public async Task<ActionResult> GetToken(string username, string password)
        {
            var result = await signInManager.PasswordSignInAsync(username, password, true, false);

            if (result.Succeeded)
            {
                string securityKey = "this_is_our_supper_long_security_key_for_token_validation_project_2018_09_07$smesk.in";
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, "Admin", "true"));
                claims.Add(new Claim(ClaimTypes.Role, "Customer", "true"));
                claims.Add(new Claim("Id", username));

                var token = new JwtSecurityToken(
                        issuer: "smesk.in",
                        audience: "admins",
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: signingCredentials
                        , claims: claims
                    );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }

            return Unauthorized();
        }
    }
}