using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Request;
using Domain.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiVersion("1")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(ILogger<BaseController> logger, IEmployeeService employeeService) : base(logger)
        {
            _employeeService = employeeService;
        }
        /// <summary>
        /// ESSE MÉTODO IRÁ ADICIONAR UM FUNCIONÁRIO
        /// </summary>
        /// <param name="request">OBJETO DO FUNCIONÁRIO</param>
        /// <returns>CODIGO DO USUARIO</returns>
        [HttpPost("AddEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BaseResponse<int>> AddEmployee([Required][FromBody] EmployeeRequest request)
        {
            return Execute(() =>
            {
                return _employeeService.PostEmployee(request);
            });
        }
        /// <summary>
        /// ESSE MÉTODO IRÁ ATUALIZAR UM FUNCIONÁRIO
        /// </summary>
        /// <param name="request">OBJETO DO FUNCIONÁRIO</param>
        /// <returns>TRUE PARA SUCESSO NA ALTERAÇÃO</returns>
        [Authorize(AuthenticationSchemes = "tokenDefault")]
        [HttpPut("UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseResponse<bool>>> UpdateEmployee([Required][FromBody] EmployeeUpdateRequest request, [Required][FromQuery]int codUsuario)
        {
            return await ExecuteAsync(async () =>
            {
                return await _employeeService.PutEmployee(request, codUsuario);
            });
        }
        /// <summary>
        /// ESSE MÉTODO IRÁ RETORNAR UM FUNCIONÁRIO PELO CÓDIGO DO USUARIO
        /// </summary>
        [Authorize(AuthenticationSchemes = "tokenDefault")]
        [HttpGet("GetEmployeeByCodUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<EmployeeResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseResponse<EmployeeResponse>>> GetEmployeeByCodUsuario([Required][FromQuery] int codUsuario)
        {
            return await ExecuteAsync(async () =>
            {
                return await _employeeService.GetEmployeeByCodUsuario(codUsuario);
            });
        }
        /// <summary>
        /// ESSE MÉTODO IRÁ RETORNAR TODOS OS FUNCIONÁRIOS
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "tokenDefault", Roles = "GESTOR,ADM")]
        [HttpGet("GetEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IEnumerable<EmployeeResponse>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseResponse<IEnumerable<EmployeeResponse>>>> GetEmployees()
        {
            return await ExecuteAsync(_employeeService.GetEmployees);
        }
    }
}
