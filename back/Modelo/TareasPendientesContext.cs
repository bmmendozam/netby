using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace back
{
    public class TareasPendientesContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public TareasPendientesContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<TareaPendiente> TareasPendientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}

