using Microsoft.EntityFrameworkCore;
using CasaMateriaisDeConstrucao.Models;

namespace CasaMateriaisDeConstrucao.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }

    }
}