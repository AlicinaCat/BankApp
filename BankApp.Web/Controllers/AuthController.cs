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
        [HttpPost("token")]
        public async Task<ActionResult> GetToken()
        {
            //security key
            string securityKey = "this_is_our_supper_long_security_key_for_token_validation_project_2018_09_07$smesk.in";
            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //signing credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //add claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Admin", "true"));
            claims.Add(new Claim(ClaimTypes.Role, "Customer", "true"));
            claims.Add(new Claim("Id", "110"));

            //create token
            var token = new JwtSecurityToken(
                    issuer: "smesk.in",
                    audience: "admins",
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCredentials
                    , claims: claims
                );

            //return token
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}