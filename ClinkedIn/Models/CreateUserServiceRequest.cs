﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class CreateUserServiceRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
    }
}
