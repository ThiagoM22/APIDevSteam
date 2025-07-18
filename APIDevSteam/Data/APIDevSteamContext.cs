﻿using APIDevSteam.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace APIDevSteam.Data
{
    public class APIDevSteamContext : IdentityDbContext<Usuario>
    {
        public APIDevSteamContext(DbContextOptions<APIDevSteamContext> options)
            : base(options)
        {
        }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<JogoCategoria> JogosCategorias { get; set; }
        public DbSet<JogoMidia> JogosMidias { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<ItemCarrinho> ItensCarrinhos { get; set; }
        public DbSet<Cupom> Cupons { get; set; }
        public DbSet<CupomCarrinho> CuponsCarrinhos { get; set; }
        public DbSet<Cartao> Cartaoes { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }









        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Jogo>().ToTable("Jogos");
            builder.Entity<Categoria>().ToTable("Categorias");
            builder.Entity<JogoCategoria>().ToTable("JogosCategorias");
            builder.Entity<JogoMidia>().ToTable("JogosMidias");

            builder.Entity<Carrinho>().ToTable("Carrinhos");
            builder.Entity<ItemCarrinho>().ToTable("ItensCarrinhos");
            builder.Entity<Cupom>().ToTable("Cupons");
            builder.Entity<CupomCarrinho>().ToTable("CuponsCarrinhos");
            builder.Entity<Cartao>().ToTable("Cartaoes");
            builder.Entity<Pagamento>().ToTable("Pagamentos");


            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<APIDevSteam.Models.Cartao> Cartao { get; set; } = default!;
        public DbSet<APIDevSteam.Models.Pagamento> Pagamento { get; set; } = default!;


    }
}
