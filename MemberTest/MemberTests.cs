using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP_XML_Parse;
using System.Drawing;
using System.Xml.Linq;
using Moq;

namespace MemberTests
{
    [TestClass]
    public class MemberTests
    {
        //Mock mockView = new Mock<IFormView>();
        //Mock mockModel = new Mock<IDocReader>();
        private List<MPData> GetActualMemberData()
        {
            XMLDocReader xmlReader = new XMLDocReader();
            return xmlReader.ReadAllData(); //need to read data from XMLDocReader to get actual values from program run, to compare against "expected values".
        }
        private List<MPData> GetExpectedMemberData() //this is the Act, stub code.
        {
            List<MPData> expectedData = new List<MPData>();

            string[] personIds = {
        "uk.org.publicwhip/person/10001",
        "uk.org.publicwhip/person/25034",
        "uk.org.publicwhip/person/24878",
        "uk.org.publicwhip/person/25661",
        "uk.org.publicwhip/person/11929",
        "uk.org.publicwhip/person/25579",
        "uk.org.publicwhip/person/25813"
    };

            string[] names = {
        "Diane Abbott",
        "Bim Afolami",
        "Imran Ahmad Khan",
        "Tahir Ali",
        "Lucy Allan",
        "Rosana Allin-Khan",
        "Fleur Anderson"
    };

            string[] donors = {
        "the Guardian, Kings Place, 90 York Way, London N1 9GU, for articles:", //Diane Abbott has multiple donors. Many Donor space are null in program. [explains why sometimes broken]
        "DMG Media Ltd/Associated Newspapers Ltd, Northcliffe House, 2 Derry St, Kensington, London W8 5TT, for an article written for the Mail on Sunday on 24 October 2020. Hours: 3 hrs. (Registered 22 December 2020)",
        "Bindmans LLP (law firm), 236 Gray’s Inn Road, London WC1X 8HB, for participation in a fact finding panel into the detention of Prince Mohammed Bin Nayef and Prince Ahmed Abdelaziz Al Saud. Hours: approx. 50 hrs. (Registered 09 February 2021) ",
        "Birmingham City Council, Victoria Square, Birmingham B1 1BB: ",
        "YouGov, 50 Featherstone Street, London EC1Y 8RT, for a survey. Hours: 1 hr. (Registered 09 March 2021) ",
        "St George's Hospital NHS Trust, Blackshaw Road, London SW17 0QT, for my work as a doctor: ",
        "Ipsos MORI, 3 Thomas More Square, London E1W 1YW, for a survey. Hours: 1 hr. Feed paid direct to charity. (Registered 25 February 2021) "
    };

            decimal[] amount = {
        19808.80m,
        2500m,
        3000m,
        35908m,
        140m,
        12569.56m,
        889.79m
    };

            string[] partyAffiliations = { "N/A", "N/A", "N/A", "N/A", "N/A", "N/A", "N/A" };


            for (int i = 0; i < personIds.Length; i++)
            {
                MPData expectedData1 = new MPData
                {
                    PersonID = personIds[i],
                    Name = names[i],
                    Donor = donors[i],
                    Amount = amount[i],
                    PartyAffiliation = partyAffiliations[i]
                };

                // Add the MPData object to the list
                expectedData.Add(expectedData1);
            }

            return expectedData;
        }

        [TestMethod]
        public void MPData_ValidXMLFile_ValidateXMLFile()
        {
            // Arrange
            string xmlFilePath = @"publicwhip.xml"; 

            // Act
            XDocument xml = XDocument.Load(xmlFilePath);

            // Assert
            Assert.IsNotNull(xml, "XML document is null");
        }

        [TestMethod]
        public void MPData_ValidMemberCount_ReturnMemberCount()
        {
            //Arrange
            List<MPData> actualMemberData = GetActualMemberData(); //simulates a program run, inserting actual values.

            //Act
            List<MPData> expectedMemberData = GetExpectedMemberData(); //gets expected data

            //Assert
            Assert.IsNotNull(actualMemberData);
            Assert.AreEqual(expectedMemberData.Count, actualMemberData.Count); 
        }

        [TestMethod]
        public void MPData_CheckIfMemberImageIsNull_ReturnDataType()
        {
            // Arrange
            List<MPData> actualMemberData = GetActualMemberData();

            // Assert
            Assert.IsNotNull(actualMemberData[0].MemberPicture);
            Assert.IsInstanceOfType(actualMemberData[0].MemberPicture, typeof(Image));
        }

        [TestMethod]
        public void MPData_CheckIfMemberNameIsNull_ReturnDataType()
        {
            // Arrange
            List<MPData> actualMemberData = GetActualMemberData();

            // Assert
            Assert.IsNotNull(actualMemberData[0].Name);
            Assert.IsInstanceOfType(actualMemberData[0].Name, typeof(String));
        }

        [TestMethod]
        public void MPData_CheckIfMemberIDIsNull_ReturnDataType()
        {
            // Arrange
            List<MPData> actualMemberData = GetActualMemberData();

            // Assert
            Assert.IsNotNull(actualMemberData[0].PersonID);
            Assert.IsInstanceOfType(actualMemberData[0].PersonID, typeof(String));
        }

        [TestMethod]
        public void MPData_CheckIfPartyAffiliationIsNull_ReturnDataType()
        {
            // Arrange
            List<MPData> actualMemberData = GetActualMemberData();

            // Assert
            Assert.IsNotNull(actualMemberData[0].PartyAffiliation);
            Assert.IsInstanceOfType(actualMemberData[0].PartyAffiliation, typeof(String));
        }

        [TestMethod]
        public void MPData_CheckIfMembersDonorsAreNull_ReturnDataType()
        {
            // Arrange
            List<MPData> actualMemberData = GetActualMemberData();

            // Assert
            Assert.IsNotNull(actualMemberData[0].FinancialDataList[0].Donor);
            Assert.IsInstanceOfType(actualMemberData[0].FinancialDataList[0].Donor, typeof(String));
        }

        [TestMethod]
        public void MPData_CheckIfMemberDonationAmountsAreNull_ReturnDataType()
        {
            // Arrange
            List<MPData> actualMemberData = GetActualMemberData();

            // Assert
            Assert.IsNotNull(actualMemberData[0].FinancialDataList[0].Amount);
            Assert.IsInstanceOfType(actualMemberData[0].FinancialDataList[0].Amount, typeof(decimal));
        }


        [TestMethod]
        public void ReadAllData_ValidateMemberName_ReturnValidMemberName()
        {
            //Arrange
            List<MPData> actualMemberData = GetActualMemberData();

            //Act
            List<MPData> expectedMemberData = GetExpectedMemberData();

            //Assert
            Assert.AreEqual(expectedMemberData[1].Name, actualMemberData[3].Name);
        }

        [TestMethod]
        public void ReadAllData_ValidateMemberID_ReturnValidMemberID()
        {
            //Arrange
            List<MPData> actualMemberData = GetActualMemberData();

            //Act
            List<MPData> expectedMemberData = GetExpectedMemberData();

            //Assert
            Assert.AreEqual(expectedMemberData[4].PersonID, actualMemberData[4].PersonID);

        }

        [TestMethod]
        public void ReadAllData_ValidatePartyAffiliation_ReturnValidPartyAffiliation()
        {
            //Arrange
            List<MPData> actualMemberData = GetActualMemberData();

            //Act
            List<MPData> expectedMemberData = GetExpectedMemberData();

            //Assert
            Assert.AreEqual(expectedMemberData[3].PartyAffiliation, actualMemberData[3].PartyAffiliation);

        }

        [TestMethod]
        public void ReadAllData_ValidateMemberDonors_ReturnValidMemberDonors()
        {   //Arrange
            List<MPData> actualMemberData = GetActualMemberData();

            //Act
            List<MPData> expectedMemberData = GetExpectedMemberData();

            //Assert
            Assert.AreEqual(expectedMemberData[0].Donor.Trim(), actualMemberData[0].FinancialDataList[0].Donor.Trim());

        }

        [TestMethod]
        public void ReadAllData_ValidateMemberDonations_ReturnValidMemberDonations()
        {   //Arrange
            List<MPData> actualMemberData = GetActualMemberData();
            List<MPData> expectedMemberData = GetExpectedMemberData();

            //Act
            decimal totalDonation = 0;
            foreach (IFinancialData financialData in actualMemberData[0].FinancialDataList)
            {
                totalDonation += financialData.Amount;
            }

            // Assert
            Assert.AreEqual(expectedMemberData[0].Amount, totalDonation);

        }
    }
}