

using Microsoft.EntityFrameworkCore;

public class BancoDeDados : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseMySQL("server=localhost;port=3306;database=restaurante;user=root;password=positivo");
    }

    public DbSet<Funcionario> Funcionario { get; set; }
    public DbSet<Produto> Produto { get; set; }
    public DbSet<Fornecedor> Fornecedor { get; set; }
    public DbSet<Cliente> Cliente { get; set; }
    
}