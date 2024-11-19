using GenAlgo;
using Microsoft.EntityFrameworkCore;

public class AlgoContext : DbContext
{
    public DbSet<Arrangement> Arrangements { get; set; }
    public DbSet<Population> Populations { get; set; }
    public DbSet<Square> Sqaures { get; set; }

    public string DbPath { get; }
    public AlgoContext()
    {
        // var folder = Environment.SpecialFolder.LocalApplicationData;
        // var path = Environment.GetFolderPath(folder);
        var path = new DirectoryInfo(Environment.CurrentDirectory)?.Parent?.FullName;
        DbPath = Path.Join(path, "SavedAlgos.db");
        Console.WriteLine($"choosen db file path: {DbPath}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}

public class Population
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public List<Arrangement> Arrs { get; set; } = new();
}