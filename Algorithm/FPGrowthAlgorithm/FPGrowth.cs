using Algorithm.Base;
using Algorithm.Code;
using System;
using System.Collections;
using System.Collections.Generic;
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
        Font font = new Font("Arial", 12, FontStyle.Bold);
        private const int Radius = 20;
        private const int Gap = 50;


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
                //Console.WriteLine("Frequent Item: " + anItem.nameItem);
                file.WriteLine("---------------------------------------------------------------------------------------------");
                file.WriteLine("\nTập thường xuyên phát sinh: " + anItem.nameItem + " -- Độ hỗ trợ: " + anItem.SupportCount);
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
        List<AssociationRule> associationRules = new List<AssociationRule>();
        private void PrintAssociateRule()
        {
            
            //Console.WriteLine("---------------------------------------------------------------------------------------------");
            file.WriteLine("---------------------------------------------------------------------------------------------");
            //Console.WriteLine("RESULTS");
            file.WriteLine("Dự đoán khả năng mua hàng của sản phẩm {A} khi mua sản phầm {B}");
            //Console.WriteLine("--------------------");
            file.WriteLine("--------------------");
            foreach (var rule in associationRules)
            {
                //Console.WriteLine($"{string.Join(", ", rule.Rule.Item1)} --> {string.Join(", ", rule.Rule.Item2)}:  {rule.Support}");
                file.WriteLine($"Khả năng mua hàng của [{string.Join(", ", rule.Rule.Item1)}] --> [{string.Join(", ", rule.Rule.Item2)}]:  {rule.Support}%");
            }
        }
        private int Mine(FPTree fpTree, Itemset anItemSet)
        {
            int minedItemSets = 0;
            FPTree projectedTree;
            projectedTree = fpTree.Project(anItemSet.GetLastItem());
            minedItemSets = projectedTree.FrequentItems.Count;
            file.WriteLine("\nTập thường xuyên khi duyệt nhánh: ");
            foreach (Item anItem in projectedTree.FrequentItems)
            {
                Itemset nextItemSet = anItemSet.Clone();
                nextItemSet.AddItem(anItem);
                List<string> freqItem = new List<string>();
                foreach (var item in nextItemSet._itemset)
                {
                    file.Write( item.nameItem + ", ");
                    freqItem.Add(item.nameItem);
                }
                
                int freqSup = GetGroupCountInItemSets(freqItem); 
                file.WriteLine(" -- Support: " + freqSup);
                nextItemSet.Print();
                List<AssociationRule> rules = new List<AssociationRule>();
                rules = GenerateAssociationRules(freqItem);
                associationRules.AddRange(rules);
                ////Console.WriteLine("---------------------------------------------------------------------------------------------");
                //file.WriteLine("---------------------------------------------------------------------------------------------");
                ////Console.WriteLine("RESULTS");
                //file.WriteLine("Dự đoán khả năng mua hàng của sản phẩm {A} khi mua sản phầm {B}");
                ////Console.WriteLine("--------------------");
                //file.WriteLine("--------------------");
                //foreach (var rule in rules)
                //{
                //    //Console.WriteLine($"{string.Join(", ", rule.Rule.Item1)} --> {string.Join(", ", rule.Rule.Item2)}:  {rule.Support}");
                //    file.WriteLine($"Khả năng mua hàng của [{string.Join(", ", rule.Rule.Item1)}] --> [{string.Join(", ", rule.Rule.Item2)}]:  {rule.Support}%");
                //}
                minedItemSets += Mine(projectedTree, nextItemSet);
            }
            return minedItemSets;
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
        static List<List<string>> GeneratePowerSet(List<string> set)
        {
            List<List<string>> subsets = new List<List<string>>();
            subsets.Add(new List<string>()); // Thêm tập rỗng
            foreach (var item in set)
            {
                int subsetsCount = subsets.Count;
                for (int i = 0; i < subsetsCount; i++)
                {
                    List<string> newSubset = new List<string>(subsets[i]);
                    newSubset.Add(item);
                    subsets.Add(newSubset);
                }
            }
            return subsets;
        }

        //ghep cac tap con thanh luat ket hop
        private List<AssociationRule> GenerateAssociationRules(List<string> itemset)
        {
            List<AssociationRule> rules = new List<AssociationRule>();
            var subsets = GeneratePowerSet(itemset);
            int sup_XY = GetGroupCountInItemSets(itemset);
            subsets.RemoveAt(0); // Loại bỏ tập rỗng
            subsets.Remove(itemset); // Loại bỏ tập hợp chính nó
            foreach (var subset in subsets)
            {
                string[] X = subset.ToArray();
                int sup_X = GetGroupCountInItemSets(subset);
                var remainingItems = itemset.Except(subset).ToList();
                foreach (var item in remainingItems)
                {
                    double confX_Y = ((double)sup_XY / (double)sup_X) * 100;
                    confX_Y = Math.Round(confX_Y, 2);
                    if (confX_Y > this.min_conf)
                    {
                        rules.Add(new AssociationRule
                        {
                            Support = confX_Y,
                            Rule = new Tuple<List<string>, List<string>>(subset, new List<string> { item })
                        });
                    }
                }
            }
            return rules;
        }
        public int RunAlgorithm(List<List<string>> data, float minSup, string path, int min_conf)
        {
            associationRules = new List<AssociationRule>();
            database = data;
            this.min_conf = min_conf;
            if (!CreateFileRead(path))
                Console.WriteLine("Path is empty");
            FPTree _fpTree = new FPTree(data, minSup);
            fpTree = _fpTree;
            int totalFrequentItemSets = GenerateFrequentItemSets();
            PrintAssociateRule();
            file.Close();
            return totalFrequentItemSets;
        }
        public void DrawTree(Node node, int left, int right, int top, Graphics g)
        {
            if (node == null) return;
            int x = (left + right) / 2;
            // Vẽ nút hiện tại
            g.FillEllipse(Brushes.White, x - Radius, top, 2 * Radius, 2 * Radius);
            g.DrawEllipse(Pens.Black, x - Radius, top, 2 * Radius, 2 * Radius);
            g.DrawString(node.NameNode + " (" + node.FpCount + ")", font, Brushes.Black, x - 20, top + 8);
            // Tính toán vị trí của các nút con
            int childCount = node.Children != null ? node.Children.Count : 0;
            int childGap = 200; // Khoảng cách cố định giữa các nút con
            int childLeft = x - (childCount - 1) * childGap / 2;
            int childRight = x + (childCount - 1) * childGap / 2;
            // Vẽ các liên kết tới nút con và gọi đệ quy cho mỗi nút con
            if (node.Children != null)
            {
                for (int i = 0; i < childCount; i++)
                {
                    int childX = childLeft + i * childGap;
                    int childY = top + Gap * 2; // Dịch chuyển xuống dưới một dòng so với nút cha
                    g.DrawLine(Pens.Black, x, top + Radius * 2, childX, childY);
                    DrawTree(node.Children[i], childX - Radius, childX + Radius, top + Gap * 2, g);
                }
            }
        }
        public bool CreateFileRead(  string path)
        {
            if(path!=null)
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
