using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoschSpot.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BoschSpot.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
        public DbSet<ContenderModel> Contender { get; set; }
        public DbSet<GroupsModel> Group { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<RarityModel> Rarity { get; set; }
        public DbSet<SpotModel> Spot { get; set; }
        public DbSet<AccountModel> Account { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<AccountModel>()
                .ToTable("Accounts")
                .HasComment("Accounts of the website")
                .HasMany(a => a.Spots)
                .WithOne(s => s.Account)
                .HasForeignKey(s => s.AccountID)
                .HasConstraintName("ForeignKey_Account_Spot");
            
            builder.Entity<ContenderModel>()
                .ToTable("Contenders")
                .HasComment("The entities that can be spotted in the game")
                .HasMany(c => c.Spots)
                .WithOne(s => s.Contender)
                .HasForeignKey(s => s.ContenderID)
                .HasConstraintName("ForeignKey_Contender_Spot");

            builder.Entity<RarityModel>()
                .ToTable("Rarities")
                .HasComment("Rarity of the contenders")
                .HasMany<ContenderModel>(r => r.Contenders)
                .WithOne(c => c.Rarity)
                .HasForeignKey(c => c.RarityID)
                .HasConstraintName("ForeignKey_Rarity_Contender");

            builder.Entity<GroupsModel>()
                .ToTable("Groups")
                .HasComment("Groups that a contender can be part of")
                .HasMany<ContenderModel>(g => g.Contenders)
                .WithOne(c => c.Groups)
                .HasForeignKey(c => c.GroupID)
                .HasConstraintName("ForeignKey_Group_Contender");

            builder.Entity<CategoryModel>()
                .ToTable("Categories")
                .HasComment("The different types of categories that a group can be part of")
                .HasMany<GroupsModel>(c => c.Groups)
                .WithOne(g => g.Category)
                .HasForeignKey(g => g.CategoryID)
                .HasConstraintName("ForeignKey_Category_Group");
        }
    }
}
