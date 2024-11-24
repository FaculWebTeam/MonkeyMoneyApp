using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MonkeyMoneyApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
    }
}
