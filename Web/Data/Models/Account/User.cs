﻿using System.ComponentModel.DataAnnotations;
namespace Web.Data.Models.Account
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsDeleted { get; set; }


    }
}