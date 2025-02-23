﻿using SQLite;

using System.Linq;


namespace ECommerceApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Email { get; set; }

        public string Password { get; set; }
        public string Name { get; set; }
    }
}
