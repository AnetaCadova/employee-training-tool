using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace employee_training_tool.Models
{
    public class Role : IdentityRole
    {
        public const string Admin = "Admin";
        public const string Newcomer = "Newcomer";
        public const string Mentor = "Mentor";
        public const string Viewer = "Viewer";

        public static IEnumerable<string> getRoles()
        {
            return new List<String>() {"Admin", "Newcomer", "Mentor", "Viewer"};
        }
    }
}