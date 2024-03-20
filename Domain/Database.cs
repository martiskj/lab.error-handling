using Microsoft.EntityFrameworkCore;

namespace Domain;

public class Database : DbContext
{
    public Database(DbContextOptions options) : base(options)
    {
    }

    internal DbSet<Application> Applications { get; set; }
}

public class Application(Guid id, string code)
{
    public Guid Id { get; set; } = id;
    public string Code { get; set; } = code;
    public ApplicationState State { get; set; } = ApplicationState.Draft;
}

public enum ApplicationState {
    Draft,
    ManualProcessing,
    Approved,
    Rejected
}
