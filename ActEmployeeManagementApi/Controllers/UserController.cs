using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [ApiVersion("1")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(ILogger<BaseController> logger, IUserService userService) : base(logger)
        {
            _userService = userService;
        }
        /// <summary>
        /// ESSE MÉTODO IRÁ ADICIONAR UM USUÁRIO
        /// </summary>
        /// <param name="codUsuario">CÓDIGO DO USUARIO</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "tokenDefault", Roles = "GESTOR, ADM")]
        [HttpPut("DisableUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<int>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BaseResponse<bool>> DisableUser([Required][FromQuery] int codUsuario)
        {
            return Execute(() =>
            {
                return _userService.DisableUser(codUsuario);
            });
        }
        /// <summary>
        /// ESSE MÉTODO IRÁ ADICIONAR UM USUÁRIO
        /// </summary>
        /// <param name="codUsuario">CÓDIGO DO USUARIO</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "tokenDefault", Roles = "ADM")]
        [HttpPatch("TransformManager")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BaseResponse<bool>> TransformManager([Required][FromQuery] int codUsuario)
        {
            return Execute(() =>
            {
                return _userService.TransformManager(codUsuario);
            });
        }
        /// <summary>
        /// ESSE MÉTODO IRÁ RETORNAR TODOS OS USUÁRIOS
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "tokenDefault", Roles = "GESTOR, ADM")]
        [HttpGet("ListUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IEnumerable<User>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseResponse<IEnumerable<Employee>>>> UpdateEmployee()
        {
            return await ExecuteAsync(_userService.GetAllUsersAsync);
        }

    }
}
