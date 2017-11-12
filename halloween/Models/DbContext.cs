using Microsoft.EntityFrameworkCore;

namespace halloween.Models
{
    public class Database : DbContext
    {
        public Database() { }
        public Database(DbContextOptions<Database> options) : base(options) { }

        //CONNECT TO OUR EXISTING MODEL(S)
        public DbSet<Greetings> Greetings { get; set; }
    }
}
