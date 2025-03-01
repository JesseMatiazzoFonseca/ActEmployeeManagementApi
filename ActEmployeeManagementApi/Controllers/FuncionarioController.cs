using Domain.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [ApiVersion("1")]
    [Authorize(AuthenticationSchemes = "tokenDefault")]
    public class FuncionarioController : BaseController
    {
        public FuncionarioController(ILogger<BaseController> logger) : base(logger)
        {
        }
        /// <summary>
        /// ESSE MÉTODO IRA ENVIAR PARA A EMPRESA 2BEBOT A URL QUE VAI RECEBER OS EVENTOS DO E-MAIL/WHATSAPP
        /// </summary>
        [HttpPost("AddEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BaseResponse<object>> AddEmployee([Required][FromQuery] object urlEvents)
        {
            return Execute(() =>
            {
                return new { };
            });
        }
        /// <summary>
        /// ESSE MÉTODO IRA ENVIAR PARA A EMPRESA 2BEBOT A URL QUE VAI RECEBER OS EVENTOS DO E-MAIL/WHATSAPP
        /// </summary>
        [HttpPut("UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BaseResponse<object>> UpdateEmployee([Required][FromQuery] object urlEvents)
        {
            return Execute(() =>
            {
                return new { };
            });
        }
        /// <summary>
        /// ESSE MÉTODO IRA ENVIAR PARA A EMPRESA 2BEBOT A URL QUE VAI RECEBER OS EVENTOS DO E-MAIL/WHATSAPP
        /// </summary>
        [HttpPut("DisableEmployee/{idEmployee}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BaseResponse<object>> DisableEmployee([Required][FromQuery] int idEmployee)
        {
            return Execute(() =>
            {
                return new { };
            });
        }
        /// <summary>
        /// ESSE MÉTODO IRA ENVIAR PARA A EMPRESA 2BEBOT A URL QUE VAI RECEBER OS EVENTOS DO E-MAIL/WHATSAPP
        /// </summary>
        [HttpGet("GetEmployee/{idEmployee}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseResponse<object>>> GetEmployee([Required][FromQuery] int idEmployee)
        {
            return await ExecuteAsync(async () =>
            {
                return await Task.Run(() => new { });
            });
        }
    }
}
