using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MP_XML_Parse;

namespace MP_XML_Parse
{
    internal static class Program
    {
        private static IDocReader docReader;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RunDocReader(); //dependency injection -> not instantiating XML/JSON doc reader, using DocReader class instead.
            Application.Run(new Form1(docReader)); 
        }

        private static void RunDocReader()
        {
            DocReader docReaders = new DocReader(); //create instances of each DocReader (XML, JSON) for L/D
            docReader = docReaders.DocumentReader("XML");
        }

    }
}
