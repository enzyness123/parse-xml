using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_XML_Parse
{
    class JSONDocReader : IDocReader
    {
        public List<MPData> ReadAllData()
        {
            //############### placeholder ###############//
            List<MPData> mpDataList = new List<MPData>();
            MPData mpData = new MPData();

            mpData.PersonID = "this is a JSON PersonID";
            mpData.Name = "this is a JSON name";
            mpData.PartyAffiliation = "N/A";

            mpDataList.Add(mpData);
            return mpDataList;

        }
    }
}
