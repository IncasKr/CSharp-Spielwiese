using System;

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
