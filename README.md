Pessimistic Lock (Kötümser Kilitleme)
--------------------------------------
"SELECT * FROM Personeller WITH (XLOCK)";

Optimistic Lock (İyimser Kilitmele)
--------------------------------------

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    [Timestamp]
    public uint Version { get; set; }
}
public class OkulDB : DbContext
{
    public OkulDB(DbContextOptions<OkulDB> dbContext) : base(dbContext) { }
    virtual public DbSet<Ders> Dersler { get; set; }
    virtual public DbSet<Ogrenci> Ogrenciler { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ogrenci>().Property(_ => _.RowVersion).IsRowVersion();
    }
}
