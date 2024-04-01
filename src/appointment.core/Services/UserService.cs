#region namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using storytiling.core.Contracts;
using storytiling.core.DTOs;
using storytiling.core.Interfaces;
using storytiling.core.Models;

#endregion namespaces

namespace storytiling.core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
                 
        public async Task<Response<UserCreateDto>> Create(UserCreateDto input)
        {
           
            var model = _mapper.Map<UserTable>(input);
            model.Id = Guid.NewGuid();
           

            //Retrieve all users for agent
            //var usersList = await _userRepository.GetUsersByEmail(userDto.Email);
            //if (usersList.ToList().Count > 0)
            //{
            //    // Validate against required date, start/end times.
            //    foreach (var item in usersList)
            //    {
            //        try
            //        {
            //            ValidateSameUsers(_mapper.Map<UserDto>(item),
            //            userDto.Date, userDto.StartTime, userDto.EndTime);
            //        }
            //        catch (Exception ex)
            //        {
            //            throw new Exception(ex.Message);
            //        }
            //    }
            //}

            await _userRepository.Save(model);
            return new Response<UserCreateDto>()
            {
                StatusCode = HttpStatusCode.OK,
                StatusMessage = $"User created successfully with id {model.Id}",
                Data = input
            };
        }
                               
        public async Task<Response<UserCreateDto>> Update(Guid id,UserCreateDto input)
        {
            var item = await _userRepository.GetById(id);

            if (item == null)
            {
                throw new Exception($"No user found with Id : {id}");
            }

             //// duplicate user check
            //var usersList = await _userRepository.GetUsersByEmail(userDto.Email);
            //if (usersList.ToList().Count > 0)
            //{
            //    throw new Exception(ex.Message);
            //}

            var model = _mapper.Map<UserCreateDto, UserTable>(input, item);

            await _userRepository.Save(model);

            return new Response<UserCreateDto>()
            {
                StatusCode = HttpStatusCode.OK,
                StatusMessage = $"User updated successfully for id {id}",
                Data = input
            };
        }

        public async Task<Response<string>> Delete(Guid id)
        {
            var userItem = await _userRepository.GetById(id);

            if (userItem == null)
            {
                throw new Exception($"No user found with Id : {id}");
            }

            await _userRepository.Delete(new UserTable() { Id = id });

            return new Response<string>()
            {
                StatusCode = HttpStatusCode.OK,
                StatusMessage = $"User deleted successfully for id {id}",
                Data = null
            };
        }

        public async Task<Response<List<UserDto>>> GetAll()
        {
            var userItems = await _userRepository.GetAll();
            return new Response<List<UserDto>>()
            {
                StatusCode = HttpStatusCode.OK,
                StatusMessage = $"Found {userItems.Count} user(s)",
                Data = userItems.Select(n => _mapper.Map<UserDto>(n)).ToList()
            };
        }

        public async Task<Response<UserDto>> GetById(Guid id)
        {
            var userItem = await _userRepository.GetById(id);

            if (userItem == null)
            {
                throw new Exception($"No user found with Id : {id}");
            }

            return new Response<UserDto>()
            {
                StatusCode = HttpStatusCode.OK,
                StatusMessage = $"Found user with Id : {id}",
                Data = _mapper.Map<UserDto>(userItem)
            };
        }

    }
}
