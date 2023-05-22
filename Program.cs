using System.Globalization;
using TradeReport.Model;
using TradeReport.ReportGenerator;

internal class Program
{

    private static void Main(string[] args)
    {
        // Load trades from CSV file
        string filename = "trades.csv";
        string programDirectory = Directory.GetCurrentDirectory();
        // Get the full path to the CSV file
        string csvFilePath = Path.Combine(programDirectory, filename);
        TradeRepository tradeRepository = new TradeRepository(csvFilePath);
        List<Trade> trades = tradeRepository.LoadTrades();
        Console.WriteLine("============== ProgramUsage ==============");
        Console.WriteLine("If the first input is 'test', it will perform self unit test.");
        Console.WriteLine("First args is the Product Type (case sensitive): FWD, BF, IRS, CDS, OPT. Please type 'null' if not sepcify.");
        Console.WriteLine("Secoond args is the Broker Name (case sensitive): Broker A, Broker B, Broker C. Please type 'null' if not sepcify.");
        Console.WriteLine("Third args is the Trade date in the format yyyy-MM-dd or yyyyMMdd. Please type 'null' if not sepcify.");
        Console.WriteLine("==========================================");


        // Generate reports
        ReportGenerator reportGenerator = new ReportGenerator(trades);
        if (args.Length == 0)
        {
            Console.WriteLine("Please input args for reporting");
        }
        else if (args.Length == 1 && args[0].ToUpper() == "TEST")
        {
            // Generate report for a sepcific conditions
            string fxForwardReport = reportGenerator.GenerateReport("FWD", "Broker A", new DateTime(2020, 4, 8));
            Console.WriteLine("FX Forward and Broker A Report:");
            Console.WriteLine(fxForwardReport);

            // Generate report for a specific product
            string CDSReport = reportGenerator.GenerateReport("CDS", null, DateTime.MinValue);
            Console.WriteLine("CDS Report:");
            Console.WriteLine(CDSReport);

            // Generate report for all trades
            string allTradesReport = reportGenerator.GenerateReport(null, null, DateTime.MinValue);
            Console.WriteLine("All Trades Report:");
            Console.WriteLine(allTradesReport);

            // Generate report for a specific broker
            string brokerReport = reportGenerator.GenerateReport(null, "Broker B", DateTime.MinValue);
            Console.WriteLine("Broker Report:");
            Console.WriteLine(brokerReport);

            // Generate report for a specific trade date - 20200409
            string tradeDateReport = reportGenerator.GenerateReport(null, null, new DateTime(2020, 4, 09));
            Console.WriteLine("Trade Date Report:");
            Console.WriteLine(tradeDateReport);
        }
        else
        {
            string productType = args[0].ToLower() == "null" ? null : args[0];
            string brokerName = (args.Length < 2 || args[1].ToLower() == "null" || string.IsNullOrEmpty(args[1])) ? null : args[1].ToUpper();
            string tradeDateString = args.Length > 2 ? args[2] : null;

            DateTime tradeDate;
            if (string.IsNullOrEmpty(tradeDateString) || tradeDateString == "null")
            {
                tradeDate = DateTime.MinValue;
            }
            else if (!DateTime.TryParse(tradeDateString, out tradeDate) && !DateTime.TryParseExact(tradeDateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out tradeDate))
            {
                Console.WriteLine("Invalid trade date format. Please provide the trade date in the format yyyy-MM-dd or yyyyMMdd.");
                return;
            }

            string Report = reportGenerator.GenerateReport(productType, brokerName, tradeDate);
            Console.WriteLine("Product type: " + (string.IsNullOrEmpty(productType) ? "N/A" : productType));
            Console.WriteLine("Broker name: " + (string.IsNullOrEmpty(brokerName) ? "N/A" : brokerName));
            Console.WriteLine("Trade date: " + (tradeDate == DateTime.MinValue ? "N/A" : tradeDate.ToString("yyyy-MM-dd")));
            Console.WriteLine(Report);
        }
        Console.WriteLine("==========================================");
        Console.WriteLine("The program ends, please enter any key to close this window.");
        Console.WriteLine("==========================================");
        Console.ReadLine();
    }
}