using MultiQueueModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiQueueSimulation
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
            foreach (var item in system.SimulationTable)
            {
                dataGridView_SD.Rows.Add(
                    item.CustomerNumber,
                    item.RandomInterArrival,
                    item.InterArrival,
                    item.ArrivalTime,
                    item.RandomService,
                    item.AssignedServer.ID,
                    item.StartTime,
                    item.ServiceTime,
                    item.EndTime,
                    item.TimeInQueue);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView_SD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
