﻿using TASK_2.models;

namespace TASK_2.DTOs
{
    public class RegistrationRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } 

    }
}
