using Microsoft.EntityFrameworkCore;
using RabbitMQ.Entities;

namespace RabbitMQ.Contexts
{
    public class RabbitMQContext : DbContext
    {
        public RabbitMQContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
