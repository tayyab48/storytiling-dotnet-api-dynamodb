using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using storytiling.core.Models;

namespace storytiling.core.Interfaces
{
    public interface IUserRepository
    {
        public Task Save(UserTable model);
        public Task Delete(UserTable model);
        public Task<List<UserTable>> GetAll();
        public Task<UserTable> GetById(Guid id);
        //public Task<IEnumerable<UserTable>> GetByEmail(string email);
    }
}
