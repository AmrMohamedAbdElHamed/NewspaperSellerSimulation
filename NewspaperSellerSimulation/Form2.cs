using NewspaperSellerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewspaperSellerSimulation
{
    public partial class Form2 : Form
    {
        SimulationSystem system;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(SimulationSystem system)
        {
            InitializeComponent();
            this.system = system;
        }
        

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView_SD.Rows.Clear();
            foreach (var item in system.SimulationTable)
            {
                dataGridView_SD.Rows.Add(
                    item.DayNo.ToString(),
                    item.RandomNewsDayType.ToString(),
                    item.NewsDayType.ToString(),
                    item.RandomDemand.ToString(),
                    item.Demand.ToString(),
                    item.SalesProfit.ToString(),
                    item.LostProfit.ToString(),
                    item.ScrapProfit.ToString(),
                    item.DailyNetProfit.ToString()
                    );
            }
        }

        
    }
}
