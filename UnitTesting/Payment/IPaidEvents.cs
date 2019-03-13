using System;
namespace Payment
{
    public interface IPaidEvents
    {
        event EventHandler Paid;
        void RaiseEvent();
    }
}
