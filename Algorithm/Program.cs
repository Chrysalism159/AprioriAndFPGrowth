using Algorithm.Code;
using Algorithm.FPGrowthAlgorithm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch AprioriTime = Stopwatch.StartNew();
            ItemSets set = new ItemSets();
            set.CreateFrequentItemSet(new ItemSet(1, new List<string>() { "A", "B", "C","D" }));
            set.CreateFrequentItemSet(new ItemSet(2, new List<string>() { "A", "B", "D", "E" }));
            set.CreateFrequentItemSet(new ItemSet(3, new List<string>() { "A", "C", "E" }));
            set.CreateFrequentItemSet(new ItemSet(4, new List<string>() { "A", "C", "D","E" }));
            set.CreateFrequentItemSet(new ItemSet(5, new List<string>() { "A", "B", "C", "D","E" }));
            set.CreateFrequentItemSet(new ItemSet(5, new List<string>() { "A",  "C", "D", "E" }));
            set.Lock();

            //Trainin paramaters (set, support(eşik), trust(eşik))
            Apriori apriori = new Apriori(set, 60, 75);
            
            apriori.Train();
            
            
            AprioriTime.Stop();

            // In thời gian thực hiện
            Console.WriteLine($"Apriori Algorithm running time: {AprioriTime.Elapsed}");
            Stopwatch FPGrowthTime = Stopwatch.StartNew();
            Console.WriteLine("---------------------------------------------------------------------------------------------" +
                "\n---------------------------------------------------------------------------------------------\n" +
                "---------------------------------------------------------------------------------------------\n" +
                "---------------------------------------------------------------------------------------------");
            Console.WriteLine("\n\nFPGrowth");
            ItemSets FPData = new ItemSets();
            FPData.CreateFrequentItemSet(new ItemSet(1, new List<string>() { "A", "B", "C", "D" }));
            FPData.CreateFrequentItemSet(new ItemSet(2, new List<string>() { "A", "B", "D", "E" }));
            FPData.CreateFrequentItemSet(new ItemSet(3, new List<string>() { "A", "C", "E" }));
            FPData.CreateFrequentItemSet(new ItemSet(4, new List<string>() { "A", "C", "D", "E" }));
            FPData.CreateFrequentItemSet(new ItemSet(5, new List<string>() { "A", "B", "C", "D", "E" }));
            FPData.CreateFrequentItemSet(new ItemSet(5, new List<string>() { "A", "C", "D", "E" }));
            FPData.Lock();
            FPGrowth fpgrowth = new FPGrowth(FPData, 60, 75);
            fpgrowth.RunAlgorithm(FPData);
            fpgrowth.PrintTree();
            FPGrowthTime.Stop();
            Console.WriteLine($"FP-Growth Algorithm running time: {FPGrowthTime.Elapsed}");
            Console.Read();
        }
    }
    //internal static class Program
    //{
    //    /// <summary>
    //    /// The main entry point for the application.
    //    /// </summary>
    //    [STAThread]
    //    static void Main()
    //    {
    //        Application.EnableVisualStyles();
    //        Application.SetCompatibleTextRenderingDefault(false);
    //        Application.Run(new Form1());
    //    }
    //}
}
