using storytiling.core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace storytiling.core.DTOs
{
    public class ContributorInviteDto
    {              
        public Guid Id { get; set; }
        public Guid WelcomeMessageId { get; set; }
        public UserSimplified Contributor { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public ContributorStatus Status { get; set; }
        public string StatusName { get; set; }
    }

    public class ContributorInviteCreateDto
    {   
        [Required]
        public UserSimplified Contributor { get; set; }          
       
    }

    public class ContributorInviteUpdateDto
    {                            
        [Required]
        public string FilePath { get; set; }
    }

    public class ContributionStats
    {
        public int? Pending { get; set; } = 0;
        public int? TotalCount { get; set; } = 0;
    }


}
