using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using storytiling.core.Models;

namespace storytiling.core.Interfaces
{
    public interface IContributorInviteRepository
    {
        public Task Save(ContributorInvite model);
        public Task SaveMany(List<ContributorInvite> models);
        public Task Delete(ContributorInvite model);
        public Task<List<ContributorInvite>> GetAll();
        public Task<ContributorInvite> GetById(Guid id);
        public Task<List<ContributorInvite>> GetAllByMessageId(Guid wm_id);
    }
}
