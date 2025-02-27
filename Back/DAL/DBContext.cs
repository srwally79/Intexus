using DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL{
    public class AppDBContext : DbContext{
        public DbSet<TaskDTO> Tasks {get; set;}
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options){}
    }
}