using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_XML_Parse
{
    public interface IFinancialData
    {
        string Donor { get; set; }
        decimal Amount { get; set; }
    }
}
