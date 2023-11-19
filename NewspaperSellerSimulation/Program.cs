using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewspaperSellerModels;
using NewspaperSellerTesting;

namespace NewspaperSellerSimulation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SimulationSystem simulationSystem = new SimulationSystem();
            simulationSystem.ReadAndSplitSections("D:/computerSciences/Smaster7/Modling&Sumoltion/Task2/NewspaperSellerSimulation_Students/NewspaperSellerSimulation/TestCases/TestCase1.txt");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
