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
    public class ContributorInviteController : ControllerBase
    {
        private readonly IContributorInviteService _contributorInviteService;

        public ContributorInviteController(IContributorInviteService contributorInviteService)
        {
            _contributorInviteService = contributorInviteService ?? throw new ArgumentNullException(nameof(contributorInviteService));
        }

        /// <summary>
        /// Create Contributor Invite.
        /// </summary>
        /// <param name="welcomeMessageId"></param>
        /// <param name="input">ContributorInviteDto</param>
        /// <returns>ContributorInviteDto</returns>
        [HttpPost("{welcomeMessageId:Guid}")]
        public async Task<ActionResult<Response<string>>> Create(Guid welcomeMessageId, [FromBody] ContributorInviteCreateDto input)
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
                var response = await _contributorInviteService.Create(welcomeMessageId, input);
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

        ///// <summary>
        ///// Creates list of Contributor Invite with given input array.
        ///// </summary>
        ///// <param name="welcomeMessageId"></param>
        ///// <param name="input">List of ContributorInviteDto</param>
        ///// <returns>ContributorInviteDtoList</returns>
        //[HttpPost("{welcomeMessageId:Guid}/import")]
        //public async Task<ActionResult<Response<string>>> CreateWithList(Guid welcomeMessageId, [FromBody] List<ContributorInviteCreateDto> input)
        //{
        //    if (input == null || input.Count == 0)
        //    {
        //        return BadRequest(new Response<string>()
        //        {
        //            StatusCode = "400",
        //            StatusMessage = "Request can NOT be null or empty."
        //        });
        //    }
        //    try
        //    {
        //        var response = await _contributorInviteService.CreateWithList(welcomeMessageId, input);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new Response<string>()
        //        {
        //            StatusCode = "400",
        //            StatusMessage = ex.Message
        //        });
        //    }
        //}

        /// <summary>
        /// Update Contributor Invite.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input">ContributorInviteDto</param>
        /// <returns>ContributorInviteDto</returns>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Response<string>>> Update(Guid id, [FromBody] ContributorInviteUpdateDto input)
        {
            if (input == null)
            {
                return BadRequest(new Response<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "Request data can NOT be null or empty."
                });
            }
            try
            {
                var response = await _contributorInviteService.Update(id, input);
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
        /// Delete Contributor Invite.
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
                    StatusMessage = "Contributor Invite id can NOT be null or empty."
                });
            }
            try
            {
                var response = await _contributorInviteService.Delete(id);
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
        /// Retrieve all Contributor Invite.
        /// </summary>
        /// <returns>ListOfContributorInviteDTO</returns>
        [HttpGet("{welcomeMessageId:Guid}")]
        public async Task<ActionResult<Response<string>>> Get(Guid welcomeMessageId)
        {
            try
            {
                var response = await _contributorInviteService.GetAllByMessageId(welcomeMessageId);
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
          

    }
}
