using MP_XML_Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_XML_Parse
{
    public class DocReader
    {
        public IDocReader DocumentReader(string fileType) 
        {
            switch (fileType)
            {
                case "XML":
                    return new XMLDocReader();
                case "JSON":
                    return new JSONDocReader();
                default:
                    throw new ArgumentException("Invalid file type");
            }
        }
    }
}
