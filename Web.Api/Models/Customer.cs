﻿namespace Web.Api.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public DateTime DateOfBirth { get; set; }
    }
}
