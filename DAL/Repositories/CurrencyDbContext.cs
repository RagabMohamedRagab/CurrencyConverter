
using BLL.Dtos;
using BLL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public class CurrencyDbContext:IdentityDbContext
    {
        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options):base(options)
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<ExchangeHistory>  ExchangeHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-R34I8VP;Database=CurrencyAPI;Trusted_Connection=True;");
         
        }
        protected override void OnModelCreating(ModelBuilder builder)
        { 
           
            base.OnModelCreating(builder);
            builder.Entity<RegisterDTO>().Property(register => register.Name).IsRequired().HasMaxLength(256);
            builder.Entity<RegisterDTO>().HasNoKey();
            builder.Entity<RegisterDTO>().Property(register => register.Email).IsRequired().HasMaxLength(256);
            builder.Entity<RegisterDTO>().Property(register => register.ConfirmEmail).IsRequired().HasMaxLength(256);
            builder.Entity<RegisterDTO>().Property(register => register.Password).IsRequired().HasMaxLength(256);
            builder.Entity<RegisterDTO>().Property(register => register.ConfirmPassword).IsRequired().HasMaxLength(256);
            builder.Entity<RegisterDTO>().Property(register => register.Phone).IsRequired().HasMaxLength(256);
            builder.Entity<LoginDto>().Property(login => login.UserName).IsRequired().HasMaxLength(256);
            builder.Entity<LoginDto>().Property(login => login.Password).IsRequired().HasMaxLength(256);
            builder.Entity<LoginDto>().HasNoKey();
            builder.Ignore<RegisterDTO>();
            builder.Ignore<LoginDto>();
            builder.Entity<Currency>().HasKey(Pk => Pk.Id);
            builder.Entity<Currency>().Property(currency => currency.Id).ValueGeneratedOnAdd();
            builder.Entity<Currency>().Property(currency => currency.Name).HasMaxLength(120);  
            builder.Entity<Currency>().Property(currency => currency.Sign).HasMaxLength(3);
            builder.Entity<Currency>().Property(currency => currency.IsActive).HasDefaultValue(1);
            builder.Entity<ExchangeHistory>().HasKey(Pk => Pk.ID);
            builder.Entity<ExchangeHistory>().Property(p => p.ID).ValueGeneratedOnAdd();
        }








    }
}
