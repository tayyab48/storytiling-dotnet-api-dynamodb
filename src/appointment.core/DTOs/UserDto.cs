using System;
using System.ComponentModel.DataAnnotations;

namespace storytiling.core.DTOs
{
    public class UserDto
    {          
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }            
    }

    public class UserCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
    }

    public class UserSimplified
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }


}
