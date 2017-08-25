using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class SomeView : IView
    {
        public event EventHandler Load;

        public void TriggerLoad(object o, EventArgs args)
        {
            Load(o, args);

        }
    }
}
