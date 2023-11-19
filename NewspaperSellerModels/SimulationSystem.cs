using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }public SimulationSystem(string path)
        {
            DayTypeDistributions = new List<DayTypeDistribution>();
            DemandDistributions = new List<DemandDistribution>();
            SimulationTable = new List<SimulationCase>();
            PerformanceMeasures = new PerformanceMeasures();
            ReadAndSplitSections(path);
            calcCummProb(DayTypeDistributions);
            calcCummProb_demand(DemandDistributions);
            fillTable();
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

        ///////////// fillTable /////////////
        public void fillTable()
        {
            Random rand = new Random();
            for (int i = 1; i < NumOfRecords + 1; i++)
            {
                SimulationCase simulationCase = new SimulationCase();
                simulationCase.DayNo = i;
                simulationCase.RandomNewsDayType = rand.Next(1, 101);
                simulationCase.NewsDayType = getDayType(simulationCase.RandomNewsDayType);
                simulationCase.RandomDemand = rand.Next(1, 101);
                simulationCase.Demand = getdemand(simulationCase.RandomDemand, simulationCase.NewsDayType);
                simulationCase.SalesProfit = calcTotalSalesProfit(simulationCase.Demand);
                simulationCase.DailyCost = calcTotalCost();
                if (simulationCase.Demand > NumOfNewspapers)
                {
                    simulationCase.LostProfit = calcTotalLostProfit(simulationCase.Demand);
                    calcDaysWithMoreDemand();

                }
                else if (simulationCase.Demand < NumOfNewspapers)
                {
                    simulationCase.ScrapProfit = calcTotalScrapProfit(simulationCase.Demand);
                    calcDaysWithUnsoldPapers();
                }
                simulationCase.DailyNetProfit = calcDalyProfit(simulationCase.DailyCost,simulationCase.SalesProfit,simulationCase.LostProfit,simulationCase.ScrapProfit);
                SimulationTable.Add(simulationCase);
            }
            calcTotalNetProfit();
        }

        ///////////// Read data from file /////////////
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
                for (int j = 0; j < 3; j++)
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

        ///////////// CummProbability /////////////
        public void calcCummProb_demand(List<DemandDistribution> DemandDistributions)
        {
            decimal total_Good = 0;
            decimal total_Fair = 0;
            decimal total_Poor = 0;
            for (int j = 0; j < DemandDistributions.Count; j++)
            {
                foreach (var item in DemandDistributions[j].DayTypeDistributions)
                {
                    if (item.DayType == Enums.DayType.Good && total_Good != 1)
                    {
                        item.MinRange = Convert.ToInt32((total_Good * 100) + 1);
                        total_Good += item.Probability;
                        item.CummProbability = total_Good;
                        item.MaxRange = Convert.ToInt32(item.CummProbability * 100);
                    }
                    if (item.DayType == Enums.DayType.Fair && total_Fair != 1)
                    {
                        item.MinRange = Convert.ToInt32((total_Fair * 100) + 1);
                        total_Fair += item.Probability;
                        item.CummProbability = total_Fair;
                        item.MaxRange = Convert.ToInt32(item.CummProbability * 100);
                    }
                    if (item.DayType == Enums.DayType.Poor && total_Poor != 1)
                    {
                        item.MinRange = Convert.ToInt32((total_Poor * 100) + 1);
                        total_Poor += item.Probability;
                        item.CummProbability = total_Poor;
                        item.MaxRange = Convert.ToInt32(item.CummProbability * 100);
                    }
                }
            }
        }
        public void calcCummProb(List<DayTypeDistribution> timeDistributions)
        {
            decimal total = 0;
            foreach (DayTypeDistribution time in timeDistributions)
            {
                time.MinRange = Convert.ToInt32((total * 100) + 1);
                total += time.Probability;
                time.CummProbability = total;
                time.MaxRange = Convert.ToInt32(time.CummProbability * 100);
            }

        }

        ///////////// Get day type from random number /////////////
        public Enums.DayType getDayType(int randomNum)
        {
            for (int j = 0; j < DayTypeDistributions.Count; j++)
            {
                if (DayTypeDistributions[j].MinRange <= randomNum &&
                    DayTypeDistributions[j].MaxRange >= randomNum)
                {
                    return DayTypeDistributions[j].DayType;

                }
            }
            return 0;
        }

        ///////////// Get demand number from random number /////////////
        public int getdemand(int randomNum, Enums.DayType dayType)
        {
            for (int j = 0; j < DemandDistributions.Count; j++)
            {

                if (DemandDistributions[j].DayTypeDistributions[Convert.ToInt16(dayType)].MinRange <= randomNum &&
                    DemandDistributions[j].DayTypeDistributions[Convert.ToInt16(dayType)].MaxRange >= randomNum)
                {
                    return DemandDistributions[j].Demand;

                }
            }
            return 0;
        }

        /////////////  /////////////
        public decimal calcTotalCost()
        {
            decimal cost;

            cost = NumOfNewspapers * PurchasePrice;
            PerformanceMeasures.TotalCost += cost;
            return cost;

        }
        /////////////  /////////////
        public decimal calcDalyProfit(decimal dailyCost, decimal salesProfit, decimal lostProfit, decimal scrapProfit)
        {
            return salesProfit - dailyCost - lostProfit + scrapProfit;
        }
        public decimal calcTotalLostProfit(int demand)
        {

            decimal profit = (demand-NumOfNewspapers) * (SellingPrice - PurchasePrice);

            PerformanceMeasures.TotalLostProfit += profit;
            return profit;
        }
        public decimal calcTotalScrapProfit(int demand)
        {
            decimal ScrapProfit;
            ScrapProfit = (NumOfNewspapers - demand) * ScrapPrice;
            PerformanceMeasures.TotalScrapProfit += ScrapProfit;
            return ScrapProfit;

        }
        public void calcTotalNetProfit()
        {

            PerformanceMeasures.TotalNetProfit = PerformanceMeasures.TotalSalesProfit - PerformanceMeasures.TotalCost - PerformanceMeasures.TotalLostProfit + PerformanceMeasures.TotalScrapProfit;
        }
        /////////////  PerformanceMeasures /////////////
        public void calcDaysWithMoreDemand()
        {

            PerformanceMeasures.DaysWithMoreDemand++;

        }
        public void calcDaysWithUnsoldPapers()
        {


            PerformanceMeasures.DaysWithUnsoldPapers++;

        }
        public decimal calcTotalSalesProfit(int Demand)
        {
            decimal SalesProfit;
            decimal check;
            check = Math.Min(NumOfNewspapers, Demand);
            SalesProfit = check * SellingPrice;
            PerformanceMeasures.TotalSalesProfit += SalesProfit;
            return SalesProfit;

        }
    }
}
