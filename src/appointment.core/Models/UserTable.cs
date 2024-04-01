using System;
using Amazon.DynamoDBv2.DataModel;
using storytiling.core.Enums;

namespace storytiling.core.Models
{
    [DynamoDBTable("dotnet-dev")]
    public class UserTable
    {
        [DynamoDBHashKey("pk")]
        public string Pk { get; set; } = nameof(PKs.Users);

        [DynamoDBRangeKey("sk")]
        public Guid Id { get; set; }

        [DynamoDBProperty("lsi")]   
        public string Name { get; set; }

        [DynamoDBProperty("email")]          
        public string Email { get; set; }

        [DynamoDBProperty("lsi_timestamp")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
