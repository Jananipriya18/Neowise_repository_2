using System;

namespace dotnetapp.Exceptions
{
    public class TrainBookingException : Exception
{
    public TrainBookingException(string message) : base(message)
    {
    }
}
}