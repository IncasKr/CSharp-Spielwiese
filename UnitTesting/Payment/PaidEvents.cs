using System;

namespace Payment
{
    public class PaidEvents : IPaidEvents
    {
        #region IWithEvents Members  
        public event EventHandler Paid;

        public void RaiseEvent()
        {
            Paid?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
