using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class Presenter
    {
        private IView view;

        private IWebService service;

        public Presenter(IView view)
        {
            this.view = view;
            // Registers for real event
            this.view.Load += View_Load;
        }

        public Presenter(IView view, IWebService ws)
        {
            this.view = view;
            service = ws;
            view.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            service.LogInfo("view loaded");
        }
    }
}
