using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIAuth.BasicAuth;
using WebAPIAuth.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user")]
    public class EmployeesController : ControllerBase
    {
        //GET: api/<EmployeesController>
        public readonly IConfiguration _configuration;
        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Login(Users userdetail)
        {
            var result = ValidateUser.GetUser(userdetail.Email, userdetail.Password);
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            if (result != null)
            {
                var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Email, result.Email),
                        new Claim(ClaimTypes.Role,result.Roles)

                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                   jwt.Issuer,
                   jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signIn
                );
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }

        [HttpGet]
        public IEnumerable<Employees> Get()
        {
            return Employees.GetAllEmployees();
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public Employees Get(int id)
        {
            return Employees.GetEmployeeById(id);
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public Employees Put([FromBody] Employees employee)
        {
            return Employees.UpdateEmployee(employee);
        }
    }
}
