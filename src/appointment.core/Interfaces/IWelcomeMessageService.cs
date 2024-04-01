using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using storytiling.core.Contracts;
using storytiling.core.DTOs;

namespace storytiling.core.Interfaces
{
    public interface IWelcomeMessageService
    {
        Task<Response<WelcomeMessageCreateDto>> Save(WelcomeMessageCreateDto input);           
        Task<Response<WelcomeMessageUpdateDto>> Update(Guid id, WelcomeMessageUpdateDto input);
        Task<Response<string>> Delete(Guid id);
        Task<Response<List<WelcomeMessageDto>>> GetAll();
        Task<Response<WelcomeMessageDto>> GetById(Guid id);
    }
}
