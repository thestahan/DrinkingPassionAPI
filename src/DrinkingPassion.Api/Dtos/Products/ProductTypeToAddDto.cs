﻿using System.ComponentModel.DataAnnotations;

namespace DrinkingPassion.Api.Dtos.Products
{
    public class ProductTypeToAddDto
    {
        [StringLength(maximumLength: 60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
    }
}
