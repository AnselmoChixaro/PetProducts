using PetProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProducts.Context
{
    public static class DbInitializer
    {
        public static void Initialize(ProductsContext prodContext)
        {
            prodContext.Database.EnsureCreated();

            if (!prodContext.Category.Any())
            {
                var categories = new Category[]
                {
                    new Category{ CategoryName = "Dog Food"},
                    new Category{ CategoryName = "Toy"},
                    new Category{ CategoryName = "Dog Traning"},
                    new Category{ CategoryName = "Ambient Enrichment"},
                    new Category{ CategoryName = "Leash"},
                    new Category{ CategoryName = "Cloth"},
                    new Category{ CategoryName = "Muzzle"},
                };

                foreach (Category c in categories)
                {
                    prodContext.Category.Add(c);
                }

                prodContext.SaveChanges();
            }

            if (!prodContext.Brands.Any())
            {
                var brands = new ProductBrand[]
                {
                    new ProductBrand{ Brand = "Purina", Country="Brazil"},
                    new ProductBrand{ Brand = "Pedigree", Country="Brazil"},
                    new ProductBrand{ Brand = "Royal Canin", Country="USA"},
                    new ProductBrand{ Brand = "BioFresh", Country="USA"},
                    new ProductBrand{ Brand = "Luiz Zucculo Dog Equips", Country="USA"},
                };

                foreach (ProductBrand b in brands)
                {
                    prodContext.Brands.Add(b);
                }

                prodContext.SaveChanges();
            }

            if (!prodContext.Products.Any())
            {
                var produtcs = new Product[]
                {
                    new Product{ Name="Ração Royal Canin", Price = 250, Description = "Ração Super Premium Natural", CategoryID = 1, ProductBrandID = 3 },
                    new Product{ Name="Ração Pro Plan", Price = 140 , Description = "Ração Para Desempenho de Atleta", CategoryID = 1, ProductBrandID = 1},
                    new Product{ Name="Ração Premier", Price = 120 , Description = "Ração Para Cães com baixa atividade Calorica", CategoryID = 1, ProductBrandID = 1  },
                    new Product{ Name="Ração Golden", Price = 180 , Description = "Ração Para Cães com alta atividade Calorica", CategoryID = 1, ProductBrandID = 4  },
                    new Product{ Name="Ração Cibau", Price = 200 , Description = "Ração para em Gestação", CategoryID = 1, ProductBrandID = 1  },
                    new Product{ Name="Guia Unificada", Price = 100 , Description = "Guia para treinamento", CategoryID = 5, ProductBrandID = 5 },
                    new Product{ Name="Guida de Correção", Price = 80 , Description = "Coleira com sistema de correção", CategoryID = 5, ProductBrandID = 5 },
                    new Product{ Name="Focinheira", Price = 30 , Description = "Focinheira de plastico simples", CategoryID = 7, ProductBrandID = 5 },
                    new Product{ Name="Roupa Pós-Cirurgia", Price = 150 , Description = "Roupa para uso pos cirugia, protege os pontos.", CategoryID = 6, ProductBrandID = 5 }
                };

                foreach (Product p in produtcs)
                {
                    prodContext.Products.Add(p);
                }

                prodContext.SaveChanges();
            }
        }
    }
}
