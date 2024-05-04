using MP_XML_Parse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP_XML_Parse
{
    public partial class Form1 : Form, IFormView
    {
        private readonly FormPresenter _presenter;
        private IDocReader docReader; //Form1 depends on IDocReader interface (abstraction) rather than specific implementations. XML and JSON doc readers can be used interchangeable where IDocReader is used.
        //this is inline with Liskov Substitution -> objects of XML/JSON readers can be substituted for objects of IDocReader.
        public Form1(IDocReader docReader) //dependency injection
        {
            InitializeComponent();
            this.docReader = docReader; //uses Program Class for either XML or JSON instantiation.
            _presenter = new FormPresenter(this, docReader); //creates new presenter, passing the view to Form1 to update UI. presenter gains access to model using docReader.
            LoadData(); //LoadData method of presenter.
        }

        public void LoadDocument(List<MPData> data) //displays data
        {
            DataTable reportData = new DataTable();
            reportData.Columns.Add("Picture", typeof(Image));
            reportData.Columns.Add("Name", typeof(string));
            reportData.Columns.Add("Party affiliation", typeof(string));
            reportData.Columns.Add("Donor", typeof(string));
            reportData.Columns.Add("Amount", typeof(decimal));

            foreach (MPData mpData in data)
            {
                Image image = mpData.MemberPicture;
                string name = mpData.Name;
                string partyAffiliation = mpData.PartyAffiliation;

                bool isFirstRow = true; //checks if it is the first row in table
                decimal totalAmount = 0;

                foreach (IFinancialData financialData in mpData.FinancialDataList)
                {
                    //only want the image, name and party affiliation to appear once, not multiple times.
                    reportData.Rows.Add(isFirstRow ? image : null, isFirstRow ? name : "", isFirstRow ? partyAffiliation : "", financialData.Donor, financialData.Amount);

                    isFirstRow = false; //sets false after first row (foreach)

                    totalAmount += financialData.Amount;
                }

                if (mpData.FinancialDataList.Count > 0) //whilst there is 1 or more "donors"/"amounts", a row with the total is added.
                {
                    reportData.Rows.Add(DBNull.Value, "", "", "Total", totalAmount);
                }
            }

            dataGridView1.DataSource = reportData;
            dataGridView1.RowPrePaint += Resize_DataGridView;
        }

        private void Resize_DataGridView(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //checks current row, if picture row.
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                var pictureCell = dataGridView1.Rows[e.RowIndex].Cells["Picture"];

                //adjust height of the picture row if there is an image.
                if (pictureCell != null && pictureCell.Value != null && pictureCell.Value is Image)
                {
                    dataGridView1.Rows[e.RowIndex].Height = 80;
                }
            }
        }
        private void LoadData() //allows LoadData method from presenter to be called in Form1.
        {
            _presenter.LoadData();
        }
    }
}

//Single responsibility --> XMLDocReader adheres to this, focused on reading data from only XML files.
//Open-closed --> IDocReader is open for extension, such as XMLDocReader or JSONDocReader without modifying existing code.
//Liskov substitution --> Use of IDocReader in Form1 class, instantiated with either XMLDocReader or JSONDocReader. Objects of the DocReader classes can be substituted for objects of IDocReader.
//Interface segregation --> IFinancialData, IMemberData, IMemberPicture has specific sets of properties related to its purpose.
//Dependency inversion --> Form1 class depends on IDocReader interface (abstraction) rather than concrete implementations such as XMLDocReader.
