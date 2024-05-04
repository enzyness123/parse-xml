using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_XML_Parse
{
    public interface IDocReader //Open/closed principle here -> open for extension, closed for modification. Extension happens through XMLDocReader, JSONDocReader.
    {
        List<MPData> ReadAllData(); //contract method. use to read data from XML, database, JSON, ect (open for extension)
    }
}
