using Microsoft.EntityFrameworkCore;

namespace Domain;

public class ApplicationService
{
    private readonly Database _database;

    public ApplicationService(Database db)
    {
        _database = db;
    }

    public async Task<Application> Create(string code)
    {
        var application = new Application(Guid.NewGuid(), code);

        _database.Add(application);
        await _database.SaveChangesAsync();

        return application;
    }

    public async Task Delete(Application application)
    {
        _database.Remove(application);
        await _database.SaveChangesAsync();
    }

    public async Task<IEnumerable<Application>> GetByQuery(GetApplicationsQuery query)
    {
        var q = _database.Applications.AsQueryable();

        if (query.Id is not null)
        {
            q = q.Where(x => x.Id == query.Id);
        }

        if (query.Code is not null)
        {
            q = q.Where(x => x.Code == query.Code);
        }

        return await q.ToListAsync();
    }

    public async Task<Application?> GetById(Guid id) 
        => (await GetByQuery(new GetApplicationsQuery { Id = id })).SingleOrDefault();
}

public class GetApplicationsQuery
{
    public Guid? Id { get; init; }
    public string? Code { get; init; }
}
