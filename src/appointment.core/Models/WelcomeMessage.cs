using System;
using Amazon.DynamoDBv2.DataModel;
using storytiling.core.DTOs;
using storytiling.core.Enums;

namespace storytiling.core.Models
{
    [DynamoDBTable("dotnet-dev")]
    public class WelcomeMessage
    {
        [DynamoDBHashKey("pk")]
        public string Pk { get; set; } = nameof(PKs.WcMessages);

        [DynamoDBRangeKey("sk")]
        public Guid Id { get; set; }

        [DynamoDBProperty("lsi")]
        public string Title { get; set; }

        [DynamoDBProperty("description")]
        public string Description { get; set; }

        [DynamoDBProperty("videoFor")]
        public UserSimplified VideoFor { get; set; }

        [DynamoDBProperty("filePath")]
        public string FilePath { get; set; } = string.Empty;

        [DynamoDBProperty("lsi_timestamp")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [DynamoDBProperty("status")]
        public WelcomeMessageStatus Status { get; set; } = WelcomeMessageStatus.Draft;
    }


    
}
