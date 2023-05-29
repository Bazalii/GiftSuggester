namespace GiftSuggester.Core.Exceptions;

public class ServiceNotRegisteredException : Exception
{
    public ServiceNotRegisteredException(string message)
        : base(message)
    {
    }
}