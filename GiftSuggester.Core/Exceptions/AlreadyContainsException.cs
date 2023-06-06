namespace GiftSuggester.Core.Exceptions;

public class AlreadyContainsException : Exception
{
    public AlreadyContainsException(string message)
        : base(message)
    {
    }
}