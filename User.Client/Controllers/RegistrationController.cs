using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using user.bussiness.Abstartion;
using user.bussiness.DTO;
using user.bussiness.Entity;

namespace User.Client.Controllers
{
    [ApiController]
    [Route("api/usersdashboard")]
    public class RegistrationController : Controller
    {
        private readonly IUserRepository _userRepository;   
        public RegistrationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public async Task< IActionResult> RegisterUser([FromBody] UserDTO userdto)
        {
            try
            {
                if (userdto == null)
                {
                    return Ok(new ResultDTO { Status = false, StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Resquest", data = null });
                }
                else
                {
                    Users user = new Users
                    {
                        UserName = userdto.UserName,
                        Email = userdto.Email,
                        City = userdto.City,
                        Region = userdto.Region,
                    };
                    await _userRepository.RegisterUser(user);
                    return Ok(new ResultDTO { Status = true, StatusCode = StatusCodes.Status200OK, Message = "RegisterUser Added successfully", data = user });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ResultDTO { Status = false, StatusCode = StatusCodes.Status500InternalServerError, Message = "Internal Server Error " + ex.Message, data = null });

            }
        }
    }
    
}

