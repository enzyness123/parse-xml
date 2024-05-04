using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_XML_Parse
{
    public class FormPresenter //acts as intermediary between view and mode.
    {
        private readonly IFormView _view; //view represented by IFormView interface. contract for view (Form1).
        private readonly IDocReader _model; //model represented by IDocReader interface. contract for reading data. document reader classes that implment the reading is encapsulated.

        public FormPresenter(IFormView view, IDocReader model)
        {
            _view = view;
            _model = model;
        }

        public void LoadData() //retrieves data from model, updates view using LoadDocument.
        {
            List<MPData> data = _model.ReadAllData(); //method from model interface to read data
            _view.LoadDocument(data); //method from view interface to update the view.
        }
    }
}
