﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pj.Models;

namespace pj.DataAccess.Data
{
    public class MyAppDatabaseContext : IdentityDbContext<IdentityUser>
    {
        public MyAppDatabaseContext(DbContextOptions<MyAppDatabaseContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHead> OrderHeads { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<Category>()
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>().HasData(
                  new Product
                  {
                      Id = 1,
                      Title = "Fortune of Time",
                      Author = "Billy Spark",
                      Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                      ISBN = "SWD9999001",
                      ListPrice = 99,
                      Price = 90,
                      Price50 = 85,
                      Price100 = 80,
                      CategoryID =1,
                      ImageUrl=""
                  },
                new Product
                {
                    Id = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryID = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryID = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Abby Muscles",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS33333333301",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryID = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",

                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryID = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "F0T00000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryID = 2,
                    ImageUrl = ""
                }
                );
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "SFX Coop",
                    StreetAddress = "3/2 abc xyz",
                    City = "Colalombua",
                    State = "Texa",
                    PostalCode = "AAA123",
                    PhoneNumber = "012312312"
                },
                new Company
                {
                    Id = 2,
                    Name = "KMS Coop",
                    StreetAddress = "532 ax",
                    City = "Xiaxia",
                    State = "Texa",
                    PostalCode = "BBB345",
                    PhoneNumber = "0111111233"
                },
                new Company
                {
                    Id = 3,
                    Name = "ABC Coop",
                    StreetAddress = "6/3 351s as",
                    City = "That city",
                    State = "That state",
                    PostalCode = "123BBB",
                    PhoneNumber = "00123233"
                },
                new Company
                {
                    Id = 4,
                    Name = "Last Coop",
                    StreetAddress = "3/2 abc xyz",
                    City = "AAAA",
                    State = "BBBBBB",
                    PostalCode = "15325",
                    PhoneNumber = "1551251252"
                }
               );
        }


    }
}
