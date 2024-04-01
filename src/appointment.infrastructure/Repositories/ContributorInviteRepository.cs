#region namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.Internal;
using storytiling.core.Enums;
using storytiling.core.Interfaces;
using storytiling.core.Models;

#endregion namespaces

namespace storytiling.infrastructure.Repositories
{
    public class ContributorInviteRepository : IContributorInviteRepository
    {
        private readonly DynamoDBContext _context;
        
        public ContributorInviteRepository(IAmazonDynamoDB context)
        {
            _context = new DynamoDBContext(context);                
        }

        public async Task Save(ContributorInvite model) =>
            await _context.SaveAsync(model);

        public async Task SaveMany(List<ContributorInvite> models)
        {
            var batchWritter = _context.CreateBatchWrite<ContributorInvite>();
            batchWritter.AddPutItems(models);
            await batchWritter.ExecuteAsync();
        }

        public async Task Delete(ContributorInvite model) =>
            await _context.DeleteAsync(model);

        public async Task<List<ContributorInvite>> GetAll()
        {
            var search = _context.QueryAsync<ContributorInvite>(nameof(PKs.WcInvites));
            return await search.GetRemainingAsync();
        }
           

        public async Task<ContributorInvite> GetById(Guid id) =>
            await _context.LoadAsync<ContributorInvite>(nameof(PKs.WcInvites), id);

        public async Task<List<ContributorInvite>> GetAllByMessageId(Guid wm_id)
        {
            var queryFilter = new QueryFilter("pk", QueryOperator.Equal, nameof(PKs.WcInvites));
            queryFilter.AddCondition("lsi", ScanOperator.Equal, wm_id);

            var queryOperationConfig = new QueryOperationConfig
            {                     
                IndexName= "lsi-index",
                Filter = queryFilter
            };

           return await _context.FromQueryAsync<ContributorInvite>(queryOperationConfig).GetRemainingAsync();
         
        }

    }
}
