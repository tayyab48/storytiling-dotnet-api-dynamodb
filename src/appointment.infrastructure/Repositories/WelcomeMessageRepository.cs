#region namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using storytiling.core.Enums;
using storytiling.core.Interfaces;
using storytiling.core.Models;

#endregion namespaces

namespace storytiling.infrastructure.Repositories
{
    public class WelcomeMessageRepository : IWelcomeMessageRepository
    {
        private readonly DynamoDBContext _context;
       
        public WelcomeMessageRepository(IAmazonDynamoDB context)
        {
             _context = new DynamoDBContext(context);
          
        }

        public async Task Save(WelcomeMessage model) =>
            await _context.SaveAsync(model);

        public async Task Delete(WelcomeMessage model) =>
            await _context.DeleteAsync(model);

        public async Task<List<WelcomeMessage>> GetAll()
        {
            return await _context.QueryAsync<WelcomeMessage>(nameof(PKs.WcMessages)).GetRemainingAsync();             
        }
           
        public async Task<WelcomeMessage> GetById(Guid id) =>
            await _context.LoadAsync<WelcomeMessage>(nameof(PKs.WcMessages), id);

        public async Task<IEnumerable<WelcomeMessage>> GetByTitle(string title)
        {
            return await _context.QueryAsync<WelcomeMessage>(title,
                new DynamoDBOperationConfig { IndexName = "title-index" }).GetRemainingAsync();
        }

    }
}
