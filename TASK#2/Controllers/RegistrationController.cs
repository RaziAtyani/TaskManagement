using Microsoft.AspNetCore.Mvc;
using TASK_2.DTOs;
using TASK_2.Services;
using System.Threading.Tasks;
using TASK_2.Models;
using Microsoft.AspNetCore.Authorization;

namespace TASK_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequest)
        {
            var result = await _registrationService.RegisterAsync(registrationRequest);
            if (result.IsSuccess)
            {
                return Ok(new { message = "Registration successful.", data = result.Data });
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var result = _registrationService.Login(loginModel);
            if (result.IsSuccess)
            {
                return Ok(new { message = result.Message, token = result.Data });
            }
            return Unauthorized(result.Message);
        }



    }
}
