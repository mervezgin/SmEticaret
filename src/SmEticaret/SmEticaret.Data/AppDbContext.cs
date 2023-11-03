﻿using Microsoft.EntityFrameworkCore;
using SmEticaret.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmEticaret.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<CartItemEntity> CartItems { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
        public DbSet<ProductCommentEntity> ProductComments { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roles = new RoleEntity[]
            {
                new RoleEntity {Id = 1, Name = "Seller"},
                new RoleEntity {Id = 2, Name = "Buyer"}
            };

            modelBuilder.Entity<RoleEntity>().HasData(roles);

            modelBuilder.ApplyConfiguration(new CartEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new  ProductCommentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());


            base.OnModelCreating(modelBuilder);
        }

    }
}
