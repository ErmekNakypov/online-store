﻿namespace BLL.Exceptions;

public class NotFoundException : InvalidOperationException
{
    public NotFoundException() : base() { }

    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

    
    public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
}
