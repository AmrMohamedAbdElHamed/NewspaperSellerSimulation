using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewspaperSellerModels;
using NewspaperSellerTesting;

namespace NewspaperSellerSimulation
{
    public partial class Form1 : Form
    {
        Form2 form2;
        SimulationSystem system;
        Form4 form4;

        public Form1()
        {

            InitializeComponent();
            form2 = new Form2(system);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1_path.Text = openFileDialog.FileName;
            }
            if (textBox1_path.Text.Length != 0 && textBox1_path.Text.Contains(".txt"))
            {
                system = new SimulationSystem(textBox1_path.Text);
                fillDataInGUI();
            }
            else
            {
                textBox1_path.Text = "";
                MessageBox.Show("Enter file path");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            form2 = new Form2(system);
            form2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            form4 = new Form4(system);
            form4.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            system = new SimulationSystem(Path.GetFullPath("../..") + "/TestCases/TestCase1.txt");
            string testingResult = TestingManager.Test(system, Constants.FileNames.TestCase1);
            MessageBox.Show(testingResult);
            fillDataInGUI();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            system = new SimulationSystem(Path.GetFullPath("../..") + "/TestCases/TestCase2.txt");
            string testingResult = TestingManager.Test(system, Constants.FileNames.TestCase2);
            MessageBox.Show(testingResult);
            fillDataInGUI();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            system = new SimulationSystem(Path.GetFullPath("../..") + "/TestCases/TestCase3.txt");
            string testingResult = TestingManager.Test(system, Constants.FileNames.TestCase3);
            MessageBox.Show(testingResult);
            fillDataInGUI();
        }
        public void fillDataInGUI()
        {
            dataGridView1.Rows.Clear();
            dataGridView_ID.Rows.Clear();
            dataGridView_SD.Rows.Clear();

            dataGridView1.Rows.Add(system.NumOfNewspapers.ToString(),
            system.NumOfRecords.ToString(),
            system.PurchasePrice.ToString(),
            system.ScrapPrice.ToString(),
            system.SellingPrice.ToString());
            foreach (var row in system.DayTypeDistributions)
            {
                dataGridView_ID.Rows.Add(row.DayType.ToString(),
                    row.Probability.ToString(),
                    row.CummProbability.ToString(),
                    $"{row.MinRange}-{row.MaxRange}");
            }
            foreach (var row in system.DemandDistributions)
            {
                dataGridView_SD.Rows.Add(row.Demand.ToString(),
                   row.DayTypeDistributions[0].Probability.ToString(),
                    row.DayTypeDistributions[1].Probability.ToString(),
                    row.DayTypeDistributions[2].Probability.ToString(),
                     $"{row.DayTypeDistributions[0].MinRange}-{row.DayTypeDistributions[0].MaxRange}",
                     $"{row.DayTypeDistributions[1].MinRange}-{row.DayTypeDistributions[1].MaxRange}",
                     $"{row.DayTypeDistributions[2].MinRange}-{row.DayTypeDistributions[2].MaxRange}");
            }
        }
    }
}
