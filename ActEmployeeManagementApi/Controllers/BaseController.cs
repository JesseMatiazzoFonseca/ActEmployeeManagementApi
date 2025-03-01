using Domain.Exceptions;
using Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> logger;

        public BaseController(ILogger<BaseController> logger)
        {
            this.logger = logger;
        }

        [NonAction]
        private ObjectResult CreateErrorResponse<T>(string message)
        {
            return Ok(
                new BaseResponse<T>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = message
                });
        }

        [NonAction]
        private ObjectResult CreateSuccessResponse<T>(T data, string message = "")
        {
            return Ok(
                new BaseResponse<T>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = message,
                    Result = data
                });
        }

        [NonAction]
        private ObjectResult CreateNotFoundResponse<T>(string message = "")
        {
            return Ok(
                new BaseResponse<T>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = message
                });
        }

        [NonAction]
        public ActionResult Execute<T>(Func<T> action)
        {
            try
            {
                return CreateSuccessResponse<T>(action());
            }
            catch (AppException ex)
            {
                logger.LogWarning(ex, ex.Message);

                return CreateNotFoundResponse<T>(ex.Message);
            }
            catch (CustomException ex)
            {
                logger.LogWarning($@"
                        - Menssagem: {ex.Message};\n
                        - Payload: {ex.Payload};\n
                        - Erro: {ex.Error}.");
                return CreateErrorResponse<T>(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return CreateErrorResponse<T>(ex.Message);
            }
        }
        [NonAction]
        private async Task<ObjectResult> CreateErrorResponseAsync<T>(string message)
        {
            return await Task.FromResult(Ok(
                new BaseResponse<T>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = message
                }));
        }
        [NonAction]
        private async Task<ObjectResult> CreateSuccessResponseAsync<T>(T data, string message = "")
        {
            return await Task.FromResult(Ok(
                new BaseResponse<T>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = message,
                    Result = data
                }));
        }
        [NonAction]
        private async Task<ObjectResult> CreateNotFoundResponseAsync<T>(string message = "")
        {
            return await Task.FromResult(Ok(
                new BaseResponse<T>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = message
                }));
        }
        [NonAction]
        public async Task<ActionResult> ExecuteAsync<T>(Func<Task<T>> action)
        {

            try
            {
                return await CreateSuccessResponseAsync<T>(await action());
            }
            catch (AppException ex)
            {
                logger.LogWarning(ex, ex.Message);

                return await CreateNotFoundResponseAsync<T>(ex.Message);
            }
            catch (CustomException ex)
            {
                logger.LogWarning($@"
                        - Menssagem: {ex.Message};\n
                        - Payload: {ex.Payload};\n
                        - Erro: {ex.Error}.");

                return await CreateErrorResponseAsync<T>(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return await CreateErrorResponseAsync<T>(ex.Message);
            }
        }
    }
}
