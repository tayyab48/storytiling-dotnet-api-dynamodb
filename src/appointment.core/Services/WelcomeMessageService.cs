#region namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using storytiling.core.Contracts;
using storytiling.core.DTOs;
using storytiling.core.Enums;
using storytiling.core.Interfaces;
using storytiling.core.Models;

#endregion namespaces

namespace storytiling.core.Services
{
    public class WelcomeMessageService : IWelcomeMessageService
    {
        private readonly IWelcomeMessageRepository _welcomeMessageRepository;
        private readonly IContributorInviteRepository _contributorInviteRepository;
        private readonly IContributorInviteService _contributorInviteService;
        private readonly IMapper _mapper;

        public WelcomeMessageService(IWelcomeMessageRepository welcomeMessageRepository, IContributorInviteRepository contributorInviteRepository, IContributorInviteService contributorInviteService, IMapper mapper)
        {
            _welcomeMessageRepository = welcomeMessageRepository ?? throw new ArgumentNullException(nameof(welcomeMessageRepository));
            _contributorInviteRepository = contributorInviteRepository ?? throw new ArgumentNullException(nameof(contributorInviteRepository));
            _contributorInviteService = contributorInviteService ?? throw new ArgumentNullException(nameof(contributorInviteService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<WelcomeMessageCreateDto>> Save(WelcomeMessageCreateDto input)
        {                
            var model = _mapper.Map<WelcomeMessage>(input);
            model.Id = Guid.NewGuid();                

            await _welcomeMessageRepository.Save(model);
            if (input.ContributorInvites.Count > 0)
            {
                await _contributorInviteService.CreateMany(model.Id, input.ContributorInvites);
            }

            return new Response<WelcomeMessageCreateDto>()
            {
               StatusCode = HttpStatusCode.OK,
                StatusMessage = $"WelcomeMessage created successfully with id {model.Id}",
                Data = input
            };
        }
                
        public async Task<Response<WelcomeMessageUpdateDto>> Update(Guid id, WelcomeMessageUpdateDto input)
        {
            var item = await _welcomeMessageRepository.GetById(id);

            if (item == null)
            {
                throw new Exception($"No Welcome Message found with Id : {id}");
            }

            var model = _mapper.Map<WelcomeMessageUpdateDto, WelcomeMessage>(input, item);
          
            await _welcomeMessageRepository.Save(model);

            return new Response<WelcomeMessageUpdateDto>()
            {
               StatusCode = HttpStatusCode.OK,
                StatusMessage = $"Welcome Message updated successfully for id {model.Id}",
                Data = input
            };
        }

        public async Task<Response<string>> Delete(Guid id)
        {
            var item = await _welcomeMessageRepository.GetById(id);

            if (item == null)
            {
                throw new Exception($"No Welcome Message found with Id : {id}");
            }

            await _welcomeMessageRepository.Delete(new WelcomeMessage() { Id = id });

            return new Response<string>()
            {
               StatusCode = HttpStatusCode.OK,
                StatusMessage = $"Welcome Message deleted successfully for id {id}",
                Data = null
            };
        }

        public async Task<Response<List<WelcomeMessageDto>>> GetAll()
        {
            var items = await _welcomeMessageRepository.GetAll();
            return new Response<List<WelcomeMessageDto>>()
            {
               StatusCode = HttpStatusCode.OK,
                StatusMessage = $"Found {items.Count} Welcome Message(s)",
                Data = items.Select(n => _mapper.Map<WelcomeMessageDto>(n)).ToList()
            };
        }

        public async Task<Response<WelcomeMessageDto>> GetById(Guid id)
        {
            var item = await _welcomeMessageRepository.GetById(id);           

            if (item == null)
            {
                throw new Exception($"No Welcome Message found with Id : {id}");
            }
            var data = _mapper.Map<WelcomeMessageDto>(item);

            var stats = await _contributorInviteRepository.GetAllByMessageId(id);

            if (stats != null)
            {
                data.ContributionStats = new ContributionStats
                {
                    TotalCount = stats.Count,
                    Pending = stats.Count(x => x.Status == ContributorStatus.InProgress)
                }; 
            }

            return new Response<WelcomeMessageDto>()
            {
               StatusCode = HttpStatusCode.OK,
                StatusMessage = $"Found Welcome Message with Id : {id}",
                Data = data
            };
        }

    }
}
