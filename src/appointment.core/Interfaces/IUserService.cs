using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using storytiling.core.Contracts;
using storytiling.core.DTOs;

namespace storytiling.core.Interfaces
{
    public interface IUserService
    {
        Task<Response<UserCreateDto>> Create(UserCreateDto input);          
        Task<Response<UserCreateDto>> Update(Guid id, UserCreateDto input);
        Task<Response<string>> Delete(Guid id);
        Task<Response<List<UserDto>>> GetAll();
        Task<Response<UserDto>> GetById(Guid id);
    }
}
