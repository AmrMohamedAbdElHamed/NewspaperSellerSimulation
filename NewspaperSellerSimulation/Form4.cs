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
            AverageWaitingTime.Text = system.PerformanceMeasures.AverageWaitingTime.ToString();
            MaxQueueLength.Text = system.PerformanceMeasures.MaxQueueLength.ToString();
            WaitingProbability.Text = system.PerformanceMeasures.WaitingProbability.ToString();
            int averageWaitingTime = 5;
            if (system.PerformanceMeasures.AverageWaitingTime>averageWaitingTime)
            {
                label2.Text = "Yes, becuase AverageWaitingTime bigger than "+ averageWaitingTime;
            }
            else
            {
                label2.Text = "No,  becuase AverageWaitingTime smaller than " + averageWaitingTime;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
