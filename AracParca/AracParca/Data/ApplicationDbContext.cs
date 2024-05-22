using AracParca.Models;
using Microsoft.EntityFrameworkCore;

namespace AracParca.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			
		}
	 public DbSet<Parca> Parcas { get; set; }
	 public DbSet<User> users { get; set; }
	
    }
}
