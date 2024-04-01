#region namespaces

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using storytiling.core.Contracts;
using storytiling.core.DTOs;
using storytiling.core.Interfaces;
using System.Net;


#endregion namespaces

namespace storytiling.api.Controllers
{
    
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WelcomeMessageController : ControllerBase
    {
        private readonly IWelcomeMessageService _welcomeMessageService;

        public WelcomeMessageController(IWelcomeMessageService welcomeMessageService)
        {
            _welcomeMessageService = welcomeMessageService ?? throw new ArgumentNullException(nameof(welcomeMessageService));
        }

        /// <summary>
        /// Create Welcome Message.
        /// </summary>
        /// <param name="input">WelcomeMessageDto</param>
        /// <returns>WelcomeMessageDto</returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] WelcomeMessageCreateDto input)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(new BadRequestResponse(ModelState));
            //}

           
            if (input == null)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "Request can NOT be null or empty."
                });
            }

            
            try
            {
                var response = await _welcomeMessageService.Save(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Update Welcome Message.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input">WelcomeMessageDto</param>
        /// <returns>WelcomeMessageDto</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<string>>> Update(Guid id, [FromBody] WelcomeMessageUpdateDto input)
        {
            if (input == null || id.Equals(Guid.Empty))
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "Request data can NOT be null or empty."
                });
            }
            try
            {
                var response = await _welcomeMessageService.Update(id, input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete Welcome Message.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "Invalid Welcome Message Id"
                });
            }
            try
            {
                var response = await _welcomeMessageService.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Retrieve all Welcome Message.
        /// </summary>
        /// <returns>ListOfWelcomeMessageDTO</returns>
        [HttpGet]
        public async Task<ActionResult<Response<string>>> Get()
        {
            try
            {
                var response = await _welcomeMessageService.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(new Response<string>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Retrieve Welcome Message by id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>WelcomeMessageDTO</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<string>>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new Response<string>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "Invalid Welcome Message Id"
                });
            }
            try
            {
                var response = await _welcomeMessageService.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

    }
}
