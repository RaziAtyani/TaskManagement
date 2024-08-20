﻿namespace TASK_2.models
{
    public enum UserRole
    {
        ProjectLeader,
        Member
    }

    public class Registration
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; } // User-selected role
    }
}
