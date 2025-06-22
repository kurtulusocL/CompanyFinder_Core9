﻿using System.ComponentModel.DataAnnotations;

namespace CompanyFinder.Core.Dtos.UserAuthDtos
{
    public class UserRegisterDto
    {
        [Required]
        public string NameSurname { get; set; }        

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }
        public DateTime Birthdate { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not same.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool IsAcceptedPolicies { get; set; }
    }
}
