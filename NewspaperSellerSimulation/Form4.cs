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
    public partial class Form4 : Form
    {
        SimulationSystem system;

        public Form4()
        {
            InitializeComponent();

        }
        public Form4(SimulationSystem system)
        {
            InitializeComponent();
            this.system = system;
            Total_Sales.Text = system.PerformanceMeasures.TotalSalesProfit.ToString();
            TotalCostofNewspapers.Text = system.PerformanceMeasures.TotalCost.ToString();
            TotalLostProfitfromExcessDemand.Text = system.PerformanceMeasures.TotalLostProfit.ToString();
            TotalSalvagefromsaleofScrappapers.Text = system.PerformanceMeasures.TotalScrapProfit.ToString();
            NetProfit.Text = system.PerformanceMeasures.TotalNetProfit.ToString();
            Numberofdayshavingexcessdemand.Text = system.PerformanceMeasures.DaysWithMoreDemand.ToString();
            Numberofdayshavingunsoldpapers.Text = system.PerformanceMeasures.DaysWithUnsoldPapers.ToString();
        }

      
    }
}
