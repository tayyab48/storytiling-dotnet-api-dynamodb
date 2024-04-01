using Amazon.DynamoDBv2.DataModel;
using storytiling.core.DTOs;
using storytiling.core.Enums;
using System;

namespace storytiling.core.Models
{
    [DynamoDBTable("dotnet-dev")]
    public class ContributorInvite
    {
        [DynamoDBHashKey("pk")]
        public string Pk { get; set; } = nameof(PKs.WcInvites);


        [DynamoDBRangeKey("sk")]
        public Guid Id { get; set; }

      
        [DynamoDBProperty("lsi")]       
        public Guid WelcomeMessageId { get; set; }

       
        [DynamoDBProperty("Contributor")]
        public UserSimplified Contributor { get; set; }

        [DynamoDBProperty("filePath")]
        public string FilePath { get; set; } = string.Empty;

        [DynamoDBProperty("lsi_timestamp")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [DynamoDBProperty("lastModified")]
        public DateTime LastModified { get; set; } = DateTime.Now;
                                         
        [DynamoDBProperty("status")]
        public ContributorStatus Status { get; set; } = ContributorStatus.InProgress;
        }
    }

