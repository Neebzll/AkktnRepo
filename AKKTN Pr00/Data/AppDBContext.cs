using Login_and_Registration.Models;
using Microsoft.EntityFrameworkCore;

namespace Login_and_Registration.Data;

public class AppDBContext: DbContext
{
    public AppDBContext(DbContextOptions options): base(options) 
    
    { 
    
    }
    public DbSet<admintbl> admintbls { get; set; }
    public DbSet<Company> companies { get; set; }
    public DbSet<Clients> clients { get; set; }
    public DbSet<CompanyTeam> companiesTeam { get; set; }
    public DbSet<Tasks> tasks { get; set; }
    public DbSet<assignedTasks> assignedTasks { get; set; }

}

