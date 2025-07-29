using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using TestApplication.API.LogicalAppIntegration;
using TestApplication.BLL.Interface;
using TestApplication.Models.Models;

namespace TestApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                var result = await _authService.RegisterAsync(dto);

                if (result == "Email is already registered.")
                    return BadRequest(new { response = result });

                SendEmailViaLogic triggerEmail = new SendEmailViaLogic();

                await triggerEmail.SendEmailViaLogicApp(dto);

                return Ok(new { response = result });
            }
            catch (Exception ex)
            {
                // You can inject and use a logger here to log the exception
                // _logger.LogError(ex, "Error during registration");

                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var token = await _authService.LoginAsync(dto);

                if (token == null)
                    return Unauthorized(new { error = "Invalid credentials" });

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // Optional: log the exception if using ILogger<AuthController>
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred.",
                    details = ex.Message // Consider removing this in production
                });
            }
        }

    }

}
