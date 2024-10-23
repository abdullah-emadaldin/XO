using Microsoft.EntityFrameworkCore;
using ReposatoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XO.Core.Models;

namespace ReposatoryPatternWithUOW.EF
{
    public class AppDbContext:DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<EmailVerificationCode> EmailVerificationCodes { get; set; }
       // public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<IdentityTokenVerification> IdentityTokenVerifications { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Move> Moves { get; set; }







        public AppDbContext(DbContextOptions options) : base(options) { }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<RefreshToken>(x =>{
            //    x.HasKey(x => new { x.UserId, x.Token });
            //    x.HasOne(x=>x.User).WithMany(x=>x.RefreshTokens).HasForeignKey(x=>x.UserId);
            //    x.Property(w => w.Token).HasColumnType("varchar").HasMaxLength(44);
            //});
            modelBuilder.Entity<EmailVerificationCode>(x =>
            {
                x.HasKey(x => new { x.UserId, x.Code });
                x.Property(w => w.Code).HasMaxLength(10).HasColumnType("varchar");
            });

            modelBuilder.Entity<User>(x =>
            {
                x.HasMany(e => e.UserConnections).WithOne(e => e.User).HasForeignKey(f => f.UserId);
               // x.HasMany(e => e.RefreshTokens).WithOne(e => e.User).HasForeignKey(f => f.UserId);
                x.HasMany(e => e.Game).WithMany(e => e.Users).UsingEntity<UserGame>();
                x.Property(p => p.UserName).HasMaxLength(20);
                x.HasIndex(p => p.UserName).IsUnique();
                x.Property(p => p.Email).HasMaxLength(100).IsUnicode(false);
                x.Property(p => p.Password).HasMaxLength(100);
                x.HasIndex(p => p.Email);
                

            });

            modelBuilder.Entity<UserConnection>(x =>
            {
                x.Property(p => p.ConnectionId).HasMaxLength(255);
                x.HasKey(k =>  k.ConnectionId );
                x.HasOne(uc => uc.User)            // UserConnection has one User
                .WithMany(u => u.UserConnections) // User has many UserConnections
                .HasForeignKey(uc => uc.UserId)   // Foreign key is UserId
                .IsRequired(false);
            });


            modelBuilder.Entity<Game>(x =>
            {
                x.Property(x => x.Id).HasMaxLength(255);
                //x.HasMany(x => x.Messages).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId);

            });

            modelBuilder.Entity<IdentityTokenVerification>(x =>
            {

                x.HasOne(p => p.User).WithOne(p => p.IdentityTokenVerification).HasForeignKey<IdentityTokenVerification>(p => p.UserId);
                x.Property(p => p.Token).HasMaxLength(500).IsUnicode(false);
                x.HasKey(k => new { k.UserId, k.Token });
            });

            //modelBuilder.Entity<GamePlay>(x =>
            //x.HasKey(p => p.GameId));
            
        }
    }
}
