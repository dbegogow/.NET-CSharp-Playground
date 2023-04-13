namespace GlobalExceptionsHandling.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string msg)
        : base(msg)
    {
    }
}