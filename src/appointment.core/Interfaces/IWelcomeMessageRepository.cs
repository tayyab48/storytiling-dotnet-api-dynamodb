using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using storytiling.core.Models;

namespace storytiling.core.Interfaces
{
    public interface IWelcomeMessageRepository
    {
        public Task Save(WelcomeMessage model);
        public Task Delete(WelcomeMessage model);
        public Task<List<WelcomeMessage>> GetAll();
        public Task<WelcomeMessage> GetById(Guid id);
        public Task<IEnumerable<WelcomeMessage>> GetByTitle(string title);
    }
}
