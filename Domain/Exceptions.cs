using System.Net;

namespace API;

public class DomainException : Exception
{
    public DomainException(string? message, HttpStatusCode code) : base(message)
    {
        Code = code;
    }

    public HttpStatusCode Code { get; set; }
}

public class ResourceNotFoundException : DomainException
{
    public ResourceNotFoundException(Guid id) : base($"Resource with id {id} was not found", HttpStatusCode.NotFound)
    {
    }
}

public class ValidationException : DomainException
{
    public ValidationException(string message) : base(message, HttpStatusCode.UnprocessableEntity)
    {
    }
}
