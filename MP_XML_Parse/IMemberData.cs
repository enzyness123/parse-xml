using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_XML_Parse
{
    public interface IMemberData //Interface segregation -> Only name and personID is found here, other interfaces contain other data.
    {
        string Name { get; set; }
        string PersonID { get; set; }
    }
}
