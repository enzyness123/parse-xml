using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Net;
using System.Drawing;
using System.Text.RegularExpressions;
using MP_XML_Parse;
using System.Net.Http;

namespace MP_XML_Parse
{
    public class XMLDocReader : IDocReader
    {
        public List<MPData> ReadAllData()
        {
            //Single Responsibility -> Class only responsible for reading data from XML file.

            XDocument xml = XDocument.Load(@"publicwhip.xml");
            List<MPData> mpDataList = new List<MPData>();

            foreach (XElement regmem in xml.Root.Elements("regmem"))
            {
                MPData mpData = new MPData(); //creates new instance of MPData for each member.

                //extract membername, personid and party affiliation
                mpData.PersonID = regmem.Attribute("personid").Value;
                mpData.Name = regmem.Attribute("membername").Value;
                mpData.PartyAffiliation = "N/A";

                //download images from specified URLs, convert it to image object.
                string imageUrl = $"https://www.theyworkforyou.com/people-images/mps/{mpData.PersonID.Split('/').Last()}.jpg";
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(imageUrl);
                    using (MemoryStream stream = new MemoryStream(data))
                    {
                        mpData.MemberPicture = Image.FromStream(stream);
                    }
                }

                //list to store donors and amounts for each member
                List<IFinancialData> financialDatalist = new List<IFinancialData>();
                decimal totalAmount = 0;

                foreach (XElement category in regmem.Elements("category"))
                {
                    if (category.Attribute("name").Value == "Employment and earnings")
                    {
                        foreach (XElement record in category.Elements("record"))
                        {
                            foreach (XElement item in record.Elements("item"))
                            {
                                //splits the item values into parts
                                string[] itemParts = item.Value.Split(' ');
                                string donor = ""; //null & 0, how to change to pass tests?
                                decimal amount = 0;

                                string pattern1 = @"(\d+\.\d+)";
                                //string pattern2 = @"(\d+)";
                                string pattern3 = @"(\d{1,3}(,\d{3})*(\.\d+)?)"; //includes decimals, large numbers

                                for (int i = 0; i < itemParts.Length; i++)
                                {
                                    //checks keywords related to receiving money in XML doc
                                    if (itemParts[i] == "received" || itemParts[i] == "receive" || itemParts[i] == "allowance" || itemParts[i] == "payment") //not perfect (payment on.. [date])
                                    {
                                        Match match = Regex.Match(itemParts[i + 1], pattern3);
                                        Match match2 = Regex.Match(itemParts[i + 2], pattern3);

                                        if (match.Success) decimal.TryParse(match.Value, out amount); totalAmount += amount;
                                        if (match2.Success) decimal.TryParse(match2.Value, out amount); totalAmount += amount;
                                    }
                                    //extract donor name
                                    if (itemParts[i] == "from")
                                    {
                                        for (int j = i + 1; j < itemParts.Length; j++)
                                        {
                                            if (itemParts[j] == ",")
                                            {
                                                break;
                                            }
                                            donor += itemParts[j] + " ";
                                        }
                                    }
                                }
                                IFinancialData financialData = new MPData(); //financial data (donors, amounts) put into MPData
                                financialData.Donor = donor; //donor contains correct info, financialData.Donor is null. financialData.Donor becomes correct so why null in test
                                financialData.Amount = amount; //same, why is 0?

                                financialDatalist.Add(financialData); //adds financial data (donor, amounts) to the MPData.

                            }
                        }
                    }
                }
                mpData.FinancialDataList = financialDatalist;
                mpDataList.Add(mpData); //adds MPData instance to the list, to populate the table.
            }
            return mpDataList;
        }

    }
}
