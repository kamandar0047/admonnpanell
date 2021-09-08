﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Front_Back.Models
{
    public class Slide
    {
        public int Id { get; set; }
        [Required,StringLength(255)]
        public string Image { get; set; }
        [NotMapped,Required]
        public IFormFile Photo { get; set; }
    }
}
