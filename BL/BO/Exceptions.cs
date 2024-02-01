namespace BO;

[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}

public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
    public BlDeletionImpossible(string message, Exception innerException)
                : base(message, innerException) { }
}

public class BlXMLFileLoadCreateException : Exception
{
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
    public BlXMLFileLoadCreateException(string message, Exception innerException)
                : base(message, innerException) { }
}

public class BlInformationIsntValid : Exception
{
    public BlInformationIsntValid(string? message) : base(message) { }
}

public class BlForbiddenAfterCreatingSchedule : Exception
{
    public BlForbiddenAfterCreatingSchedule(string? message) : base(message) { }
}

public class BlForbiddenToUpdate : Exception
{
    public BlForbiddenToUpdate(string? message) : base(message) { }
}
