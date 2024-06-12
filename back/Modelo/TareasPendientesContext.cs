using Microsoft.EntityFrameworkCore;

namespace back
{
    public class TareasPendientesContext : DbContext
    {
        public DbSet<TareaPendiente> TareasPendientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Bolivar_Mendoza;Trusted_Connection=True;");
        }
    }
}
