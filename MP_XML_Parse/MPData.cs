using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MP_XML_Parse;

namespace MP_XML_Parse
{
    public class MPData : IMemberData, IFinancialData, IMemberPicture, IPartyAffiliation
    {
        private string id;
        private string name;
        private string partyAffiliation;
        private string donor;
        private decimal amount;
        private Image picture;

        public string PersonID
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string PartyAffiliation
        {
            get { return partyAffiliation; }
            set { partyAffiliation = value; }
        }

        public string Donor
        {
            get { return donor; }
            set { donor = value; }
        }
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public Image MemberPicture
        {
            get { return picture; }
            set { picture = value; }
        }

        public List<IFinancialData> FinancialDataList { get; set; } = new List<IFinancialData>();
    }
}
