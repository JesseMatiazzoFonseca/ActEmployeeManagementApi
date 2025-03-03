using Domain.Interfaces.Repository;
using Domain.Models.Request;
using Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiVersion("1")]
    public class LoginController : BaseController
    {
        private readonly ILoginService _loginService;
        public LoginController(ILogger<BaseController> logger, ILoginService loginService) : base(logger)
        {
            _loginService = loginService;
        }
        /// <summary>
        /// MÉTODO PARA REALIZAR O LOGIN
        /// </summary>
        /// <param name="request">OBJETO REQUEST LOGIN</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<UserResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseResponse<UserResponse>>> Login([Required][FromBody] LoginRequest request)
        {
            return await ExecuteAsync(async () => await _loginService.Login(request));
        }
    }
}
