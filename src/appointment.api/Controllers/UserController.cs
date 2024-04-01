#region namespaces

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using storytiling.core.Contracts;
using storytiling.core.DTOs;
using storytiling.core.Interfaces;
using System.Collections.Generic;
using System.Net;

#endregion namespaces

namespace storytiling.api.Controllers
{        
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="input">UserDto</param>
        /// <returns>UserDto</returns>
        [HttpPost]
        public async Task<ActionResult<Response<string>>> Create([FromBody] UserCreateDto input)
        {
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
                var response = await _userService.Create(input);
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
        /// Update user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input">UserDto</param>
        /// <returns>UserDto</returns>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Response<string>>> Update(Guid id, [FromBody] UserCreateDto input)
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
                var response = await _userService.Update(id, input);
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
        /// Delete user.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Response<string>>> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "User id can NOT be null or empty."
                });
            }
            try
            {
                var response = await _userService.Delete(id);
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
        /// Retrieve all users.
        /// </summary>
        /// <returns>ListOfUserDTO</returns>
        [HttpGet]
        public async Task<ActionResult<Response<string>>> Get()
        {
            try
            {
                var response = await _userService.GetAll();
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
        /// Retrieve user by id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>UserDTO</returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Response<string>>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new Response<string>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "User id can NOT be null or empty."
                });
            }
            try
            {
                var response = await _userService.GetById(id);
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
