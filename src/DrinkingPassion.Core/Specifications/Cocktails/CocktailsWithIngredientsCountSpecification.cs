﻿using Core.Entities;
using Core.Models;
using System.Linq;

namespace Core.Specifications.Cocktails
{
    public class CocktailsWithIngredientsCountSpecification : BaseSpecification<Cocktail>
    {
        public CocktailsWithIngredientsCountSpecification(CocktailSpecParams cocktailParams, bool isPrivate, string authorId = "")
            : base(x =>
                (string.IsNullOrEmpty(cocktailParams.CocktailName) || x.Name.ToLower().Contains(cocktailParams.CocktailName)) &&
                (!cocktailParams.ProductId.HasValue || x.BaseProductId == cocktailParams.ProductId) &&
                (string.IsNullOrEmpty(authorId) || x.AuthorId == authorId) &&
                (cocktailParams.IngredientsExactCount == 0 || x.IngredientsCount == cocktailParams.IngredientsExactCount) &&
                (cocktailParams.IngredientsList.Count == 0 || x.Ingredients.Count(i => cocktailParams.IngredientsList.Contains(i.ProductId)) == cocktailParams.IngredientsList.Count) &&
                x.IsPrivate == isPrivate
            )
        {
            AddOrderBy(x => x.Name);
            AddInclude(x => x.BaseProduct);
            ApplyPaging(cocktailParams.PageSize * (cocktailParams.PageIndex - 1), cocktailParams.PageSize);

            if (!string.IsNullOrEmpty(cocktailParams.Sort))
            {
                switch (cocktailParams.Sort)
                {
                    case CocktailsSort.NameDesc:
                        AddOrderByDescending(x => x.Name);
                        break;
                    case CocktailsSort.IngredientsAsc:
                        AddOrderBy(x => x.IngredientsCount);
                        break;
                    case CocktailsSort.IngredientsDesc:
                        AddOrderByDescending(x => x.IngredientsCount);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }
    }
}
