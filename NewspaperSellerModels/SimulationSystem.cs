using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewspaperSellerModels.Enums;

namespace NewspaperSellerModels
{
    public class SimulationSystem
    {
        public SimulationSystem()
        {
            DayTypeDistributions = new List<DayTypeDistribution>();
            DemandDistributions = new List<DemandDistribution>();
            SimulationTable = new List<SimulationCase>();
            PerformanceMeasures = new PerformanceMeasures(); 
        }
        ///////////// INPUTS /////////////
        public int NumOfNewspapers { get; set; }
        public int NumOfRecords { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal ScrapPrice { get; set; }
        public decimal UnitProfit { get; set; }
        public List<DayTypeDistribution> DayTypeDistributions { get; set; }
        public List<DemandDistribution> DemandDistributions { get; set; }

        ///////////// OUTPUTS /////////////
        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }



        public void ReadAndSplitSections(string filePath)
        {
            var data = File.ReadAllText(filePath);
            var sections = data.Split(new string[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);


            foreach (var section in sections)
            {
                var lines = section.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                var key = lines[0].Trim();

                switch (key)
                {
                    case "NumOfNewspapers":
                        this.NumOfNewspapers = int.Parse(lines[1].Trim());
                        break;
                    case "NumOfRecords":
                        this.NumOfRecords = int.Parse(lines[1].Trim());
                        break;
                    case "PurchasePrice":
                        this.PurchasePrice = decimal.Parse(lines[1].Trim());
                        break;
                    case "SellingPrice":
                        this.SellingPrice = decimal.Parse(lines[1].Trim());
                        break;
                    case "ScrapPrice":
                        this.ScrapPrice = decimal.Parse(lines[1].Trim());
                        break;
                    case "DayTypeDistributions":
                        this.DayTypeDistributions = ParseDayTypeDistributions(lines);
                        break;
                    case "DemandDistributions":
                        this.DemandDistributions = ParseDemandDistributions(lines);
                        break;
                    default:
                        break;
                }
            }

            // Now, you have the simulationSystem object populated with the data
            // You can use this object as needed.
        }

        private List<DayTypeDistribution> ParseDayTypeDistributions(string[] lines)
        {
            var distributions = new List<DayTypeDistribution>();
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                for(int j = 0; j < 3; j++)
                {
                    decimal probability = decimal.Parse(parts[j].Trim());
                    var distribution = new DayTypeDistribution
                    {
                        Probability = probability,
                        DayType = (Enums.DayType)j
                        // You may need to set other properties based on your requirements
                    };
                    distributions.Add(distribution);
                }


            }

            return distributions;
        }

        private List<DemandDistribution> ParseDemandDistributions(string[] lines)
        {
            var distributions = new List<DemandDistribution>();

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                int demand = int.Parse(parts[0].Trim());
                decimal prob1 = decimal.Parse(parts[1].Trim());
                decimal prob2 = decimal.Parse(parts[2].Trim());
                decimal prob3 = decimal.Parse(parts[3].Trim());

                var distribution = new DemandDistribution
                {
                    Demand = demand,
                    DayTypeDistributions = new List<DayTypeDistribution>
            {
                new DayTypeDistribution { Probability = prob1,DayType = (Enums.DayType)0 },
                new DayTypeDistribution { Probability = prob2,DayType = (Enums.DayType)1 },
                new DayTypeDistribution { Probability = prob3,DayType = (Enums.DayType)2 }

            }
                };

                distributions.Add(distribution);
            }

            return distributions;
        }

    }
}
