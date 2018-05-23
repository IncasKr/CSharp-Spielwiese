using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment
{
    public class PaidEvents : IPaidEvents
    {
        #region IWithEvents Members  
        public event System.EventHandler Paid;

        public void RaiseEvent()
        {
            Paid?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
