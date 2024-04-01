#region namespaces

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using storytiling.core.Enums;
using storytiling.core.Interfaces;
using storytiling.core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion namespaces

namespace storytiling.infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DynamoDBContext _context;

        public UserRepository(IAmazonDynamoDB context)
        {
            _context = new DynamoDBContext(context);
        }

        public async Task Save(UserTable model) =>
            await _context.SaveAsync(model);

        public async Task Delete(UserTable model) =>
            await _context.DeleteAsync(model);

        public async Task<List<UserTable>> GetAll()
        {
            return await _context.QueryAsync<UserTable>(nameof(PKs.Users)).GetRemainingAsync();          

        }
                

        public async Task<UserTable> GetById(Guid id) =>
            await _context.LoadAsync<UserTable>(nameof(PKs.Users), id);

        //public async Task<IEnumerable<UserTable>> GetByEmail(string email)
        //{
        //    return await _context.QueryAsync<UserTable>(email,
        //        new DynamoDBOperationConfig { IndexName = "lsi-index" }).GetRemainingAsync();
        //}

    }
}
