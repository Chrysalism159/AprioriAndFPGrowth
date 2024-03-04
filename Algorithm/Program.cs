using Algorithm.Code;
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
            Stopwatch stopwatch = Stopwatch.StartNew();
            ItemSets set = new ItemSets();
            set.CreateFrequentItemSet(new ItemSet(1, new List<string>() { "Bread", "Milk", "Egg" }));
            set.CreateFrequentItemSet(new ItemSet(2, new List<string>() { "Bread", "Coca", "Egg", "Candy" }));
            set.CreateFrequentItemSet(new ItemSet(3, new List<string>() { "Coca", "Egg", "Milk", "Bread", "Candy" }));
            set.CreateFrequentItemSet(new ItemSet(4, new List<string>() { "Coca", "Egg", "Candy" }));
            set.CreateFrequentItemSet(new ItemSet(5, new List<string>() { "Egg", "Bread", "Milk", "Candy" }));
            set.Lock();

            //Trainin paramaters (set, support(eşik), trust(eşik))
            Apriori apriori = new Apriori(set, 60, 75);
            apriori.Train();
            stopwatch.Stop();

            // In thời gian thực hiện
            Console.WriteLine($"Program running time: {stopwatch.Elapsed}");
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
