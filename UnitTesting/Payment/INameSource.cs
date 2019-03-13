using System;
namespace Payment
{
    public interface INameSource
    {
        string CreateName(string firstName, string surname);
    }
}
