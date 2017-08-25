using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class Presenter
    {
        IView view;

        public Presenter(IView view)
        {
            this.view = view;
            // Registers for real event
            this.view.Load += new EventHandler(View_Load);
        }

        void View_Load(object sender, EventArgs e)
        {
            throw new Exception("Not implemented.");
        }
    }
}
