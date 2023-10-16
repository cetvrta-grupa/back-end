﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class PersonDto
    {
        //    public int Id { get; set; }
        //    public string Username { get; set; }
        //    public Role Role { get; set; }
        //    public bool IsActive { get; set; }
    
        public int Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
