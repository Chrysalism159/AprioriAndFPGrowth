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

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }





    //class Program
    //{
    //    static void Main()
    //    {
    //        char[] originalSet = { 'A', 'B', 'C', 'D', 'E' };

    //        Console.WriteLine("Các tập con:");
    //        for (int i = 2; i <= originalSet.Length; i++)
    //        {
    //            List<List<char>> subsets = GenerateSubsets(originalSet, i);
    //            foreach (var subset in subsets)
    //            {
    //                Console.WriteLine("{" + string.Join(",", subset) + "}");
    //            }
    //        }
    //    }


    //}
}
