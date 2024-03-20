using API;

namespace Domain;

public class ApplicationStateService(Database database)
{
    private readonly Database _database = database;

    public void SetToManualProcessing(Application application)
    {
        if (application.State != ApplicationState.Approved)
        {
            throw new ValidationException("Application must be in approval state");
        }

        application.State = ApplicationState.ManualProcessing;
        _database.SaveChangesAsync();
    }
}
