﻿using System.ComponentModel.DataAnnotations;

namespace DrinkingPassion.Api.Dtos.Ingredients
{
    public class IngredientToAddDto
    {
        [Required]
        [Range(.1, 1000)]
        public double Amount { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
