//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.Diagnostics;
//using System.IdentityModel.Tokens.Jwt;
//using System.Reflection.Metadata.Ecma335;
//using System.Text;
//using user.bussiness.Entity;

//namespace User.Client.Controllers
//{
//    public class LoginController : Controller
//    {
//        private IConfiguration _configuration;
//        public LoginController(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }
//        private Admin  AuthenticateAdmin(Admin admin) 
//        {
//            Admin _admin1 = null;
//            if (admin.AdminName == "admin" && admin.Password == "12345")
//            {
//                _admin1= new Admin { AdminName = "rishabh" };  
//            }
//            return _admin1;
//        } 

//        private string GenerateToken(Admin admin)
//        {
//            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
//            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],null,
//                expires:DateTime.Now.AddMinutes(1),
//                signingCredentials:credentials);
//            return new JwtSecurityTokenHandler().WriteToken(token);

//        }

//        [HttpPost("loggin")]
//        public IActionResult Logging([FromBody]Admin admin)
//        {
//            IActionResult response = Unauthorized();
//            var adm= AuthenticateAdmin(admin);
//            if(adm != null)
//            {
//                var token =GenerateToken(adm);
//                response = Ok(new {token=token});
//            }
//            return response;
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using user.bussiness.Entity;
using user.bussiness.Abstartion;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using user.bussiness.DTO;

namespace User.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;

        public LoginController(IAdminRepository adminRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
        }

        private string GenerateToken(Admin admin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
             (
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(15), // date of expirey
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("Adminlogin")]
        public async Task<IActionResult> Logging([FromBody] Admin admin)
        {
            try
            {
                IActionResult response = Unauthorized();
                var authenticatedAdmin = await _adminRepository.LoggingAdmin(admin);

                if (authenticatedAdmin != null)
                {

                    var token = GenerateToken(authenticatedAdmin);
                    response = Ok(new { Token = token });
                }

                return response;
            }
            catch(Exception ex) 
            { 
                return Ok(new ResultDTO { Status = false, StatusCode = StatusCodes.Status500InternalServerError, Message = "Internal Server Error " + ex.Message, data = null });
            }
        }

        [HttpPost("AdminRegister")]
        public async Task<IActionResult> Register([FromBody] AdminDTO admindto)
        {
            try
            {
                IActionResult response;
                Admin admin = new Admin
                {
                    AdminName = admindto.AdminName,
                    Password = admindto.Password
                };
                var registeredAdmin = await _adminRepository.RegisterAdmin(admin);

                if (registeredAdmin != null)
                {
                    //  var token = GenerateToken(registeredAdmin);
                  response = Ok(new ResultDTO { Status = true, StatusCode = StatusCodes.Status200OK, Message = "Registration successfully" , data = admin });
                }
                else
                {
                    response = Ok(new ResultDTO { Status = false, StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Request", data = null });
                }

               return response;
            }
            catch(Exception ex) 
            {
                return Ok(new ResultDTO { Status = false, StatusCode = StatusCodes.Status500InternalServerError, Message = "Internal Server Error " + ex.Message, data = null });
            }
        }
    }
}
