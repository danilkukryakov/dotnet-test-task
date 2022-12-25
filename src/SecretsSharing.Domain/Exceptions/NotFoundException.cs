namespace SecretsSharing.Domain.Exceptions;

/// <summary>
/// Not found exception.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public NotFoundException()
    { }

    /// <summary>
    /// Constructor.
    /// </summary>
    public NotFoundException(string message)
        : base(message)
    { }

    /// <summary>
    /// Constructor.
    /// </summary>
    public NotFoundException(string message, Exception inner)
        : base(message, inner)
    { }
}
