using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using storytiling.core.Contracts;
using storytiling.core.DTOs;

namespace storytiling.core.Interfaces
{
    public interface IContributorInviteService
    {
        Task<Response<ContributorInviteCreateDto>> Create(Guid welcomeMessageId, ContributorInviteCreateDto input);
        Task<Response<string>> CreateMany(Guid welcomeMessageId, List<ContributorInviteCreateDto> input);
        Task<Response<ContributorInviteUpdateDto>> Update(Guid id, ContributorInviteUpdateDto input);
        Task<Response<string>> Delete(Guid id);
        Task<Response<List<ContributorInviteDto>>> GetAll(Guid welcomeMessageId);
        Task<Response<List<ContributorInviteDto>>> GetAllByMessageId(Guid welcomeMessageId);
        Task<Response<ContributorInviteDto>> GetById(Guid id);

    }
}
