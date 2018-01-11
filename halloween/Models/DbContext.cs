using Microsoft.EntityFrameworkCore;

namespace egreeting.Models
{
    public class Database : DbContext
    {
        public Database() { }
        public Database(DbContextOptions<Database> options) : base(options) { }

        //CONNECT TO OUR EXISTING MODEL(S)
        public DbSet<Greetings> Greetings { get; set; }
    }
}
