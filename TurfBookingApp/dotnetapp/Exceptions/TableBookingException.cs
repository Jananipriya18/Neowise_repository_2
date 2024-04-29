using System;

namespace dotnetapp.Exceptions
{

public class TableBookingException : Exception
{
    public TableBookingException(string message) : base(message)
    {
    }
}
}

