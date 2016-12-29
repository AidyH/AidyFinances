using AF.Models;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace AF.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any account types
                if (!context.AccountTypes.Any())
                {
                    context.AccountTypes.AddRange(
                        new AccountType
                        {
                            Name = "Cash"
                        },

                        new AccountType
                        {
                            Name = "Bank Account"
                        },

                        new AccountType
                        {
                            Name = "Credit Card"
                        }
                    );
                }

                // Look for any currencies
                if (!context.Currencies.Any())
                {
                    context.Currencies.AddRange(
                        new Currency
                        {
                            Name = "British Pound",
                            Abbreviation = "GBP"
                        },

                        new Currency
                        {
                            Name = "US Dollar",
                            Abbreviation = "USD"
                        },

                        new Currency
                        {
                            Name = "Euro",
                            Abbreviation = "EUR"
                        }
                    );
                }
                context.SaveChanges();
            }
        }
    }
}