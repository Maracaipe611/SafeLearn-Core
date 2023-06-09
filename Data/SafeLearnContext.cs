using Microsoft.EntityFrameworkCore;
using SafeLearn.Usuario;

namespace SafeLearn.Data
{
    public class SafeLearnContext : DbContext
    {
        public SafeLearnContext(DbContextOptions<SafeLearnContext> options) : base(options)
        {
        }

        public DbSet<UsuarioEntity> Usuarios { get; set; }

        // Adicione outras DbSet para as outras entidades, se necessário
    }
}
