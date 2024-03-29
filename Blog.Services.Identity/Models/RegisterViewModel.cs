﻿using System.ComponentModel.DataAnnotations;

namespace Blog.Services.Identity.Models
{
    public class RegisterViewModel
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }  //Для удобства тестирования, но поле должно НЕ ДОПУСКАТЬ значения Null

        [Required]
        public string RoleName { get; set; } = Configuration.Client;

    }
}
