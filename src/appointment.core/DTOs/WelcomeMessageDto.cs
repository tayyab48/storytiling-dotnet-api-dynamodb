using storytiling.core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace storytiling.core.DTOs
{
    public class WelcomeMessageDto
    {
       
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UserSimplified VideoFor { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public WelcomeMessageStatus Status { get; set; }
        public string StatusName { get; set; }
        public ContributionStats ContributionStats { get; set; } = null;  
        
    }

    public class WelcomeMessageCreateDto 
    {
        [Required]
        [StringLength(150, MinimumLength = 10)]         
        public string Title { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
        

        [Required]
        public UserSimplified VideoFor { get; set; }

        [NotMapped]
        public List<ContributorInviteCreateDto> ContributorInvites { get; set; }
    }

    public class WelcomeMessageUpdateDto
    {
        [Required]
        [StringLength(150, MinimumLength = 10)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        public UserSimplified VideoFor { get; set; }         

    }
}
