﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.SchoolDTO
{
    public class CreateSchoolDTO
    {
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
