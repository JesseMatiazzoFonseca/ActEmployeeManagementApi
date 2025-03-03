using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Request;
using Domain.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        [Authorize(AuthenticationSchemes = "tokenDefault", Roles = "ADM,GESTOR")]
        [HttpPut("UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BaseResponse<bool>> UpdateEmployee([Required][FromBody] EmployeeRequest request)
        {
            return Execute(() =>
            {
                return _employeeService.PutEmployee(request);
            });
        }
        /// <summary>
        /// ESSE MÉTODO IRÁ RETORNAR TODOS OS FUNCIONÁRIOS
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "tokenDefault", Roles = "GESTOR,ADM")]
        [HttpGet("GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseResponse<IEnumerable<Employee>>>> GetEmployee()
        {
            return await ExecuteAsync(_employeeService.GetEmployees);
        }
    }
}
