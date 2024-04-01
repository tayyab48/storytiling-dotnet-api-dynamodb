#region namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
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
    public class ContributorInviteService : IContributorInviteService
    {
        private readonly IContributorInviteRepository _contributorInviteRepository;
        private readonly IWelcomeMessageRepository _welcomeMessageRepository;
        private readonly IMapper _mapper;

        public ContributorInviteService(IContributorInviteRepository contributorInviteRepository, IWelcomeMessageRepository welcomeMessageRepository, IMapper mapper)
        {
            _contributorInviteRepository = contributorInviteRepository ?? throw new ArgumentNullException(nameof(contributorInviteRepository));
            _welcomeMessageRepository = welcomeMessageRepository ?? throw new ArgumentNullException(nameof(welcomeMessageRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<ContributorInviteCreateDto>> Create(Guid welcomeMessageId, ContributorInviteCreateDto input)
        {
          
            var model = _mapper.Map<ContributorInvite>(input);
            model.Id = Guid.NewGuid();
            model.WelcomeMessageId = welcomeMessageId;              
            model.LastModified = DateTime.Now;

            await _contributorInviteRepository.Save(model);
            return new Response<ContributorInviteCreateDto>()
            {
                StatusCode = HttpStatusCode.OK,
                StatusMessage = $"ContributorInvite created successfully with id {model.Id}",
                Data = input
            };
        }

        public async Task<Response<string>> CreateMany(Guid welcomeMessageId, List<ContributorInviteCreateDto> input)
        {
            var modelList = new List<ContributorInvite>();
            foreach (var item in input)
            {
                var model = _mapper.Map<ContributorInvite>(item);
                model.Id = Guid.NewGuid();
                model.WelcomeMessageId = welcomeMessageId;
                model.LastModified = DateTime.Now;

                modelList.Add(model);
            }

            await _contributorInviteRepository.SaveMany(modelList);

            return new Response<string>()
            {
                StatusCode = HttpStatusCode.OK,
                StatusMessage = $"Invite saved successfully for id {welcomeMessageId}",
                Data = null
            };

        }

        public async Task<Response<ContributorInviteUpdateDto>> Update(Guid id, ContributorInviteUpdateDto input)
        {
            var contributorInviteItem = await _contributorInviteRepository.GetById(id);

            if (contributorInviteItem == null)
            {
                throw new Exception($"No Invite found with Id : {id}");
            }

            var contributorInviteModel = _mapper.Map<ContributorInviteUpdateDto, ContributorInvite>(input, contributorInviteItem);  
            
            contributorInviteModel.LastModified = DateTime.Now;
            contributorInviteModel.Status = ContributorStatus.Done;

            await _contributorInviteRepository.Save(contributorInviteModel);             
           
             await ChangeWelcomeMessageStatus(contributorInviteModel.WelcomeMessageId);

            return new Response<ContributorInviteUpdateDto>()
            {
                StatusCode = HttpStatusCode.OK,
                StatusMessage = $"Invite updated successfully for id {id}",
                Data = input
            };
        }

        private async Task ChangeWelcomeMessageStatus(Guid welcomeMessageId)
        {
            var contributorList = await _contributorInviteRepository.GetAllByMessageId(welcomeMessageId);
            if (contributorList == null) return;
            // To Find if any contributor has not recorded message for new employee
            var pendingContributors = contributorList.FindIndex(contributor => contributor.Status == ContributorStatus.InProgress);
            // If All the contributor has recorded their messages then update the welcome Message Status to ReadyToShare.
            if (pendingContributors <= 0)
            {
                // change Status of this welcomeMessage to ReadyToShare
                var welcomeMessage = await _welcomeMessageRepository.GetById(welcomeMessageId);
                if(welcomeMessage == null) { return; }
                welcomeMessage.Status = WelcomeMessageStatus.ReadyToShare;
                await _welcomeMessageRepository.Save(welcomeMessage);
            }
        }

        public async Task<Response<string>> Delete(Guid id)
        {
            var contributorInviteItem = await _contributorInviteRepository.GetById(id);

            if (contributorInviteItem == null)
            {
                throw new Exception($"No Invite found with Id : {id}");
            }

            await _contributorInviteRepository.Delete(new ContributorInvite() { Id = id });

            return new Response<string>()
            {
                StatusCode = HttpStatusCode.OK,
                StatusMessage = $"Invite deleted successfully for id {id}",
                Data = null
            };
        }

        public async Task<Response<List<ContributorInviteDto>>> GetAll(Guid welcomeMessageId)
        {
            var contributorInviteItems = await _contributorInviteRepository.GetAllByMessageId(welcomeMessageId);
            return new Response<List<ContributorInviteDto>>()
            {
                StatusCode = HttpStatusCode.OK,
                StatusMessage = $"Found {contributorInviteItems.Count} Invite(s)",
                Data = contributorInviteItems.Select(n => _mapper.Map<ContributorInviteDto>(n)).ToList()
            };
        }

        public async Task<Response<List<ContributorInviteDto>>> GetAllByMessageId(Guid welcomeMessageId)
        {
            var contributorInviteItems = await _contributorInviteRepository.GetAllByMessageId(welcomeMessageId);
            return new Response<List<ContributorInviteDto>>()
            {
                StatusCode = HttpStatusCode.OK,
                StatusMessage = $"Found {contributorInviteItems.Count} Invite(s)",
                Data = contributorInviteItems.Select(n => _mapper.Map<ContributorInviteDto>(n)).ToList()
            };
        }

        public async Task<Response<ContributorInviteDto>> GetById(Guid id)
        {
            var contributorInviteItem = await _contributorInviteRepository.GetById(id);

            if (contributorInviteItem == null)
            {
                throw new Exception($"No Invite found with Id : {id}");
            }

            return new Response<ContributorInviteDto>()
            {
                StatusCode = HttpStatusCode.OK,
                StatusMessage = $"Found Invite with Id : {id}",
                Data = _mapper.Map<ContributorInviteDto>(contributorInviteItem)
            };
        }

    }
}
