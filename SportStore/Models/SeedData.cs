using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace SportStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kajak",
                        Description = "Łódka przeznaczona dla jednej osoby",
                        Category = "Sporty wodne",
                        Price = 275
                    },
                    new Product
                    {
                        Name = "Kamizelka ratunkowa",
                        Description = "Chroni",
                        Category = "Sporty wodne",
                        Price = 48.95m
                    },
                    new Product
                    {
                        Name = "Piłka",
                        Description = "Zatwierdzone przez FIFA",
                        Category = "Piłka nożna",
                        Price = 19.50m
                    },
                    new Product
                    {
                        Name = "Flagi narożne",
                        Description = "Nadadzą Twojemu boisku profesjonalny wygląd",
                        Category = "Piłka nożna",
                        Price = 34.95m
                    },
                    new Product
                    {
                        Name = "Stadion",
                        Description = "Składany stadion",
                        Category = "Piłka nożna",
                        Price = 79500
                    },
                    new Product
                    {
                        Name = "Czapka",
                        Description = "Zwiększa efektywność mózgu",
                        Category = "Szachy",
                        Price = 16
                    },
                    new Product
                    {
                        Name = "Niestabilne krzesło",
                        Description = "Zmniejsza szanse przeciwnika",
                        Category = "Szachy",
                        Price = 29.95m
                    },
                    new Product
                    {
                        Name = "Ludzka szachownica",
                        Description = "Przyjemna gra",
                        Category = "Szachy",
                        Price = 75
                    },
                    new Product
                    {
                        Name = "Błyszczący król",
                        Description = "Figura",
                        Category = "Szachy",
                        Price = 1200
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
