﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.ItemDTO
{
    public class CreateItemDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
