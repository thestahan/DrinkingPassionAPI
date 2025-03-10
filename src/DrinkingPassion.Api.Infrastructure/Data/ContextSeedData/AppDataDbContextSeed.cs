﻿using DrinkingPassion.Api.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkingPassion.Api.Infrastructure.Data.ContextSeedData
{
    public static class AppDataDbContextSeed
    {
        public static async Task SeedDataAsync(AppDbContext context)
        {
            if (!context.ProductTypes.Any())
            {
                var productTypes = new List<ProductType>
                {
                    new() {
                        Name = "Mocny alkohol"
                    },
                    new() {
                        Name = "Sok"
                    },
                    new() {
                        Name = "Likier"
                    },
                    new() {
                        Name = "Piwo"
                    },
                    new() {
                        Name = "Wino"
                    },
                    new() {
                        Name = "Bitter"
                    }
                };

                await context.AddRangeAsync(productTypes);

                await context.SaveChangesAsync();
            }

            if (!context.ProductUnits.Any())
            {
                var productUnits = new List<ProductUnit>
                {
                    new() {
                        Name = "Mililitr",
                        Abbreviation = "ml"
                    },
                    new() {
                        Name = "Łyżeczka"
                    },
                    new() {
                        Name = "Łyżka stołowa"
                    },
                    new() {
                        Name = "Gram",
                        Abbreviation = "g"
                    }
                };

                await context.AddRangeAsync(productUnits);

                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var gram = context.ProductUnits.Where(x => x.Name.Equals("gram", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var ml = context.ProductUnits.Where(x => x.Name.Equals("mililitr", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                var strongAlcohol = context.ProductTypes.Where(x => x.Name.Equals("mocny alkohol", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var liqueur = context.ProductTypes.Where(x => x.Name.Equals("likier", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var juice = context.ProductTypes.Where(x => x.Name.Equals("sok", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var wine = context.ProductTypes.Where(x => x.Name.Equals("wino", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var bitter = context.ProductTypes.Where(x => x.Name.Equals("bitter", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                var products = new List<Product>
                {
                    new() {
                        Name = "Tequila",
                        ProductType = strongAlcohol,
                        ProductUnit = ml
                    },
                    new() {
                        Name = "Wódka",
                        ProductType = strongAlcohol,
                        ProductUnit = ml
                    },
                    new() {
                        Name = "Wódka limonkowa",
                        ProductType = strongAlcohol,
                        ProductUnit = ml
                    },
                    new() {
                        Name = "Whisky",
                        ProductType = strongAlcohol,
                        ProductUnit = ml
                    },
                    new() {
                        Name = "Sok z cytryny",
                        ProductType = juice,
                        ProductUnit = ml
                    },
                    new() {
                        Name = "Sok z limonki",
                        ProductType = juice,
                        ProductUnit = ml
                    },
                    new() {
                        Name = "Triple sec",
                        ProductType = liqueur,
                        ProductUnit = ml
                    },
                    new() {
                        Name = "Gin",
                        ProductType = strongAlcohol,
                        ProductUnit = ml
                    },
                    new() {
                        Name = "Wermut",
                        ProductType = wine,
                        ProductUnit = ml
                    },
                    new() {
                        Name = "Campari",
                        ProductType = bitter,
                        ProductUnit = ml
                    }
                };

                await context.AddRangeAsync(products);

                await context.SaveChangesAsync();
            }

            if (!context.Cocktails.Any())
            {
                var tequila = context.Products.Where(x => x.Name.Equals("tequila", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var limeJuice = context.Products.Where(x => x.Name.Equals("sok z limonki", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var tripleSec = context.Products.Where(x => x.Name.Equals("triple sec", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var gin = context.Products.Where(x => x.Name.Equals("gin", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var vermouth = context.Products.Where(x => x.Name.Equals("wermut", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var campari = context.Products.Where(x => x.Name.Equals("campari", System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                var cocktails = new List<Cocktail>();

                var cocktail = new Cocktail
                {
                    Name = "Margarita",
                    BaseProduct = tequila,
                    Description = "Koktajl alkoholowy na bazie tequili, likieru typu triple sec i soku z limonki.",
                    Picture = "images/cocktails/margarita.jpg"
                };

                var ingredients = new List<Ingredient>
                {
                    new() {
                        Amount = 40,
                        Cocktail = cocktail,
                        Product = tequila,
                    },
                    new() {
                        Amount = 20,
                        Cocktail = cocktail,
                        Product = tripleSec,
                    },
                    new() {
                        Amount = 20,
                        Cocktail = cocktail,
                        Product = limeJuice,
                    }
                };

                cocktail.Ingredients = ingredients;
                cocktail.IngredientsCount = ingredients.Count;

                cocktails.Add(cocktail);

                cocktail = new Cocktail
                {
                    Name = "Negroni",
                    BaseProduct = gin,
                    Description = "Koktajl alkoholowy przygotowywany z trzech równych porcji ginu, słodkiego wermutu i gorzkiego likieru.",
                    Picture = "images/cocktails/negroni.jpg"
                };

                ingredients = new List<Ingredient>
                {
                    new() {
                        Amount = 30,
                        Cocktail = cocktail,
                        Product = gin,
                    },
                    new() {
                        Amount = 30,
                        Cocktail = cocktail,
                        Product = vermouth,
                    },
                    new() {
                        Amount = 30,
                        Cocktail = cocktail,
                        Product = campari,
                    }
                };

                cocktail.Ingredients = ingredients;
                cocktail.IngredientsCount = ingredients.Count;

                cocktails.Add(cocktail);

                await context.Cocktails.AddRangeAsync(cocktails);

                await context.SaveChangesAsync();
            }
        }
    }
}
