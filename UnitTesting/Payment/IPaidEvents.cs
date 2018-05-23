using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment
{
    public interface IPaidEvents
    {
        event EventHandler Paid;
        void RaiseEvent();
    }
}
