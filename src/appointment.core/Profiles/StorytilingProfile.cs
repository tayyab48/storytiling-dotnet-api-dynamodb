using AutoMapper;
using storytiling.core.DTOs;
using storytiling.core.Enums;
using storytiling.core.Models;
using System.Collections.Generic;

namespace storytiling.core.Profiles
{
    public class StorytilingProfile : Profile
    {
        public StorytilingProfile()
        {
            CreateMap<UserDto, UserTable>();
            CreateMap<UserCreateDto, UserTable>();
            CreateMap<UserTable, UserDto>();

            CreateMap<WelcomeMessageDto, WelcomeMessage>();
            CreateMap<WelcomeMessageCreateDto, WelcomeMessage>();            
                
            CreateMap<WelcomeMessageUpdateDto, WelcomeMessage>();

            CreateMap<WelcomeMessage, WelcomeMessageDto>()
               .ForMember(x => x.StatusName, cd => cd.MapFrom(map => map.Status == WelcomeMessageStatus.Draft ? nameof(WelcomeMessageStatus.Draft): map.Status == WelcomeMessageStatus.InProgress ? nameof(WelcomeMessageStatus.InProgress) : nameof(WelcomeMessageStatus.ReadyToShare)));

            // "Missing type map configuration or unsupported mapping.Mapping types:UserSimplified -> ContributorInvite
            // storytiling.core.DTOs.UserSimplified -> storytiling.core.Models.ContributorInvite",

            CreateMap<ContributorInviteDto, ContributorInvite>();
            CreateMap<ContributorInviteCreateDto, ContributorInvite>();            
            CreateMap<ContributorInviteUpdateDto, ContributorInvite>();
           //  CreateMap<UserSimplified, ContributorInvite>();

            CreateMap<ContributorInvite, ContributorInviteDto>()
                 .ForMember(x => x.StatusName, cd => cd.MapFrom(map => map.Status == ContributorStatus.InProgress ? nameof(ContributorStatus.InProgress) : nameof(ContributorStatus.Done)));
        }
    }
}
