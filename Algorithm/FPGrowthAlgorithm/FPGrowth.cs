using Algorithm.Base;
using Algorithm.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.FPGrowthAlgorithm
{
    public class FPGrowth
    {
        private int min_conf;
        public FPTree fpTree;
        string fileOutputPath;
        List<List<string>> database;
        System.IO.StreamWriter file;

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
        public void GenerateFrequentItemSets()
        {
            List<Item> frequentItems = fpTree.FrequentItems;
            file.WriteLine("---------------------------------------------------------------------------------------------");
            file.WriteLine("\nDanh sách các tập thường xuyên phát sinh\nTên danh mục\t\t\t\tĐộ hỗ trợ\n------------------------------------------------------------------");
            foreach (Item anItem in frequentItems)
            {
                FP_Itemset anItemSet = new FP_Itemset();
                anItemSet.AddItem(anItem);
                Mine(fpTree, anItemSet);
            }
        }
        List<AssociationRule> associationRules = new List<AssociationRule>();
        private void PrintAssociateRule()
        {
            //Console.WriteLine("---------------------------------------------------------------------------------------------");
            file.WriteLine("---------------------------------------------------------------------------------------------");
            //Console.WriteLine("RESULTS");
            file.WriteLine("**************************************");
            //Console.WriteLine("--------------------");
            file.WriteLine("--------------------");
            associationRules = associationRules.OrderByDescending(m=>m.Support).ToList();
            int i = 1;
            foreach (var rule in associationRules)
            {
                //Console.WriteLine($"{string.Join(", ", rule.Rule.Item1)} --> {string.Join(", ", rule.Rule.Item2)}:  {rule.Support}");
                file.WriteLine($"{i}. [{string.Join(", ", rule.Rule.Item1)}] --> [{string.Join(", ", rule.Rule.Item2)}]:  {rule.Support}%");
                i++;
            }
        }
        private void Mine(FPTree fpTree, FP_Itemset anItemSet)
        {
            FPTree projectedTree;
            projectedTree = fpTree.Project(anItemSet.GetLastItem());
            foreach (Item anItem in projectedTree.FrequentItems)
            {
                FP_Itemset nextItemSet = anItemSet.Clone();
                nextItemSet.AddItem(anItem);
                List<string> freqItem = new List<string>();
                foreach (var item in nextItemSet._itemset)
                {
                    file.Write($" {string.Join(", ", item.nameItem)}");
                    freqItem.Add(item.nameItem);
                }

                int freqSup = GetGroupCountInItemSets(freqItem);
                file.WriteLine("\t\t\t\t\t" + freqSup);
                List<AssociationRule> rules = new List<AssociationRule>();
                rules = GenerateAssociationRules(freqItem);
                associationRules.AddRange(rules);
                Mine(projectedTree, nextItemSet);
            }
        }
        public int GetGroupCountInItemSets(List<string> group)
        {
            int temp = 0;
            for (int i = 0; i < this.database.Count; i++)
            {
                if (group.Except(this.database[i]).Count() == 0)
                {
                    temp++;
                }
            }
            return temp;
        }
        //chia cac tap thuong xuyen thanh cac tap con
        static Dictionary<TValue, TKey> ReverseDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            return dictionary.ToDictionary(x => x.Value, x => x.Key);
        }
        //chia cac tap thuong xuyen thanh cac tap con
        static Dictionary<string, string> GeneratePowerSet(List<string> set)
        {
            Dictionary<string, string> subrule = new Dictionary<string, string>();

            // Tạo các tập con không chứa một phần tử
            foreach (var item in set)
            {
                List<string> subset = new List<string>(set);
                subset.Remove(item);
                string value = string.Join(",", subset);
                subrule[item] = value;
            }

            // Đảo ngược từ điển và thêm vào subrule nếu chưa tồn tại
            Dictionary<string, string> other = ReverseDictionary(subrule);
            foreach (KeyValuePair<string, string> pair in other)
            {
                if (!subrule.ContainsKey(pair.Key))
                    subrule[pair.Key] = pair.Value;
            }

            return subrule;
        }
        //ghep cac tap con thanh luat ket hop
        private List<AssociationRule> GenerateAssociationRules(List<string> itemset)
        {
            //itemset = "a,b,c,d"
            List<AssociationRule> rules = new List<AssociationRule>();
            var subsets = GeneratePowerSet(itemset);
            int sup_XY = GetGroupCountInItemSets(itemset);
            foreach (var subset in subsets)
            {
                string[] X = subset.Key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> xx = X.ToList();
                int sup_X = 0;

                sup_X = GetGroupCountInItemSets(xx);
                double confX_Y = ((double)sup_XY / (double)sup_X) * 100;
                confX_Y = Math.Round(confX_Y, 2);
                if (confX_Y > this.min_conf)
                {
                    List<string> a = X.ToList();
                    rules.Add(new AssociationRule
                    {
                        Support = confX_Y,
                        Rule = new Tuple<List<string>, List<string>>(a, new List<string> { subset.Value })
                    });
                }

            }
            return rules;
        }
        public void RunAlgorithm(List<List<string>> data, float minSup, string path, int min_conf)
        {
            associationRules = new List<AssociationRule>();
            database = data;
            this.min_conf = min_conf;
            if (!CreateFileRead(path))
                Console.WriteLine("Path is empty");
            FPTree _fpTree = new FPTree(data, minSup);
            fpTree = _fpTree;
            file.WriteLine($"Độ hỗ trợ tối thiểu = {minSup}");
            file.WriteLine($"Độ tin cậy tối thiểu  = {this.min_conf}%\n\n");
            List<Item> frequentItems = fpTree.FrequentItems;
            file.WriteLine("\n---------------------------------------------------------------------------------------------");
            file.WriteLine("\nDanh sách các danh mục đơn thỏa mãn độ hỗ trợ tối thiểu\nTên danh mục\t\t\t\t\t\t\t\tĐộ hỗ trợ\n---------------------------------------------");
            foreach (Item anItem in frequentItems)
            {
                //Console.WriteLine("Frequent Item: " + anItem.nameItem);
                file.WriteLine(anItem.nameItem + "\t\t\t\t\t\t\t\t" + anItem.SupportCount);
            }
            GenerateFrequentItemSets();
            PrintAssociateRule();
            file.Close();
        }

        public bool CreateFileRead(string path)
        {
            if (path != null)
            {
                try
                {
                    string fileName = path + "_" + "FPGrowth.txt";
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));

                    file = new System.IO.StreamWriter(fileName);//open file for streaming
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
            return false;
        }
    }

}
