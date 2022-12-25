namespace SecretsSharing.Domain.Exceptions;

/// <summary>
/// Domain exception.
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public DomainException()
    { }

    /// <summary>
    /// Constructor.
    /// </summary>
    public DomainException(string message)
        : base(message)
    { }

    /// <summary>
    /// Constructor.
    /// </summary>
    public DomainException(string message, Exception inner)
        : base(message, inner)
    { }
}
