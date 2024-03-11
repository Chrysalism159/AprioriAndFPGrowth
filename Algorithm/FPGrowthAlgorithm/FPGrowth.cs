using Algorithm.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.FPGrowthAlgorithm
{
   public class FPGrowth
    {
        FPTree fpTree;
        string fileOutputPath;

        public FPGrowth()
        {
            fpTree = null;
        }
        public FPGrowth(FPTree tree, string fileOutputPath)
            : this()
        {
            fpTree = tree;
            this.fileOutputPath = fileOutputPath;
        }
        public int GenerateFrequentItemSets()
        {
            List<Item> frequentItems = fpTree.FrequentItems;
            int totalFrequentItemSets = frequentItems.Count;
            foreach (Item anItem in frequentItems)
            {
                Console.WriteLine("Frequent Item: " + anItem.nameItem);
                Itemset anItemSet = new Itemset();
                anItemSet.AddItem(anItem);
                //Console.WriteLine("Print itemset in GenFrequentItemset: ");
                //anItemSet.Print();
                totalFrequentItemSets += Mine(fpTree, anItemSet);
                //Console.WriteLine(totalFrequentItemSets + " itemsets for " + anItem.nameItem);
            }
            Console.WriteLine(totalFrequentItemSets);
            return totalFrequentItemSets;
        }

        private int Mine(FPTree fpTree, Itemset anItemSet)
        {
            int minedItemSets = 0;
            FPTree projectedTree;
            projectedTree = fpTree.Project(anItemSet.GetLastItem());
            minedItemSets = projectedTree.FrequentItems.Count;
            foreach (Item anItem in projectedTree.FrequentItems)
            {
                Itemset nextItemSet = anItemSet.Clone();
                nextItemSet.AddItem(anItem);
                Console.WriteLine("Print itemset in Mine: ");
                nextItemSet.Print();
                minedItemSets += Mine(projectedTree, nextItemSet);
            }
            return minedItemSets;
        }
        public int CreateFPTreeAndGenerateFrequentItemsets(List<List<string>> data, float minSup, string path)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            FPTree _fpTree = new FPTree(data, minSup);
            fpTree = _fpTree;
            int totalFrequentItemSets = GenerateFrequentItemSets();
            watch.Stop();
            WriteAggregatedResult(minSup, totalFrequentItemSets, (double)watch.ElapsedMilliseconds, path);
            return totalFrequentItemSets;
        }
        public void WriteAggregatedResult( float minimumSupport, int totalFrequentItemSets, double totalRunningTime_ms, string path)
        {
            
            System.IO.StreamWriter file;
            try
            {
                string fileName = path + "_" + minimumSupport.ToString() + "_aggregated.txt";
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));

                file = new System.IO.StreamWriter(fileName);//open file for streaming
                file.WriteLine("MinSup Itemset_NO Time(s)");
                file.WriteLine(minimumSupport.ToString() + " " + totalFrequentItemSets.ToString() + " " + (totalRunningTime_ms / 1000f).ToString());
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return;
            }
        }
    }
    
}
