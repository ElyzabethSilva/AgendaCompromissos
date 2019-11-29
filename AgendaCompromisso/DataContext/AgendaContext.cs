using AgendaCompromissos.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaCompromissos.DataContexts {
    public class AgendaContext : DbContext {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base (options) {

        }

        public DbSet<Consultas> Consulta { get; set; }

    }
}