using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP_XML_Parse
{
    public interface IFormView
    {
        void LoadDocument(List<MPData> data); //to update update
    }
}
