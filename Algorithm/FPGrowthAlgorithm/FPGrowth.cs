using Algorithm.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.FPGrowthAlgorithm
{
    public sealed class FPGrowth
    {
        private ItemSets ItemSet;
        private int support;
        private int confidence;
        private int min_support;
        Dictionary<string, int> frequentItems = new Dictionary<string, int>();
        public FPNode Root = new FPNode(null, 0, null);
        
        public FPGrowth(ItemSets itemSets, int support, int confidence)
        {
            this.ItemSet = itemSets;
            this.support = support > 100 ? 100 : (support < 0 ? 0 : support);
            this.confidence = confidence > 100 ? 100 : (confidence < 0 ? 0 : confidence);
            this.min_support = this.ItemSet.ListProduct.Count * support / 100;
        }
        public Dictionary<string, int> CountNumberOfItem()
        {
            Dictionary<string, int> value = new Dictionary<string, int>();
            for (int i = 0; i < this.ItemSet.ListProduct.Count; i++)
            {
                for (int j = 0; j < this.ItemSet.ListProduct[i].Items.Count; j++)
                {
                    string product = ItemSet.ListProduct[i].Items[j];
                    if (value.ContainsKey(product))
                        value[product]++;
                    else
                        value.Add(product, 1);
                }
            }
            return value;
        }
        public Dictionary<string, int> SortItemSet(Dictionary<string, int> productCounts)
        {
            List<KeyValuePair<string, int>> list = productCounts.ToList();

            // Sắp xếp danh sách theo giá trị (value)
            list.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            // Tạo một SortedDictionary từ danh sách đã sắp xếp
            Dictionary<string, int> sortedDictionary = new Dictionary<string, int>();
            foreach (var pair in list)
            {
                sortedDictionary.Add(pair.Key, pair.Value);
            }
            return sortedDictionary;
        }
        public Dictionary<string, int> RemoveValueLowerMinSup(Dictionary<string, int> products, int min_support)
        {
            return products.Where(pair => pair.Value >= min_support).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        public Dictionary<string[], int> RemoveValueLowerMinSup(Dictionary<string[], int> products, int min_support)
        {
            return products.Where(pair => pair.Value >= min_support).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        //public ItemSets SortByFList(Dictionary<string, int> productCounts)
        //{
        //    List<string> flist = new List<string>();
        //    foreach(string value in productCounts.Keys)
        //    {
        //        flist.Add(value);
        //    }
        //    ItemSets values = new ItemSets();
        //    int TID = 1;
        //    foreach (var item in ItemSet.ListProduct)
        //    {
        //        List<string> list = flist.Intersect(item.Items).ToList();
        //        values.CreateFrequentItemSet(new ItemSet(TID, list));
        //        TID++;
        //    }
        //    return values;
        //}
        public void PrintCounts(Dictionary<string, int> productCounts, string title)
        {
            Console.WriteLine(title);
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("Products\t\tCount");
            Console.WriteLine("--------------------");
            foreach (KeyValuePair<string, int> product in productCounts)
            {
                Console.WriteLine($"count({product.Key})\t{product.Value}");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }
       
        public void PrintDatabase(ItemSets itemSets)
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            foreach (var item in itemSets.ListProduct)
            {
                Console.WriteLine($"Id: {item.TID}, Items: [{string.Join(", ", item.Items)}]");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }
        public void CreateFPTree(ItemSets transaction, int min_sup, Dictionary<string, int> frequentSet)
        {
            foreach (var item in transaction.ListProduct)
            {
                var sortedTransaction = item.Items.Where(x => frequentSet.ContainsKey(x))
                                                    .OrderByDescending(x => frequentSet[x])
                                                    .ToList();
                InsertTree(sortedTransaction, Root);
            }
        }
        private void InsertTree(List<string> rlist, FPNode node)
        {
            if (rlist.Count == 0)
                return;

            string firstItem = rlist[0];
            FPNode child = node.Children.FirstOrDefault(x => x.item == firstItem);

            if (child != null)
            {
                child.frequent++;
            }
            else
            {
                child = new FPNode(firstItem, 1, node);
                node.Children.Add(child);
            }

            rlist.RemoveAt(0);
            InsertTree(rlist, child);
        }
        private void PrintNode(FPNode node, int level)
        {
            if (node == null)
                return;

            // In nút hiện tại
            string indent = new string(' ', level * 4);
            Console.WriteLine(indent + (node.item ?? "Root") + " (" + node.frequent + ")");

            // In các nút con
            foreach (var child in node.Children)
            {
                PrintNode(child, level + 1);
            }
        }
        public void PrintTree()
        {
            PrintNode(Root, 0);
        }
        
        public void RunAlgorithm(ItemSets itemset)
        {
            Dictionary<string, int> productCounts = this.CountNumberOfItem();
            productCounts = SortItemSet(productCounts);
            Console.WriteLine($"support(threshold) = %{this.support}");
            Console.WriteLine($"trust(threshold)  = %{this.confidence}");
            Console.WriteLine();
            //ItemSets itemSets = new ItemSets();
            //itemSets = SortByFList(productCounts);
            //PrintDatabase(itemSets);
            this.PrintCounts(productCounts, "Support Values");
            Console.WriteLine("Create FP tree");
            CreateFPTree(itemset, min_support, productCounts);

        }
    }
    
    public class FPNode
    {
        public string item { get; }
        public int frequent { get; set; }
        public FPNode Parent { get; set; }
        public List<FPNode> Children { get; }

        public FPNode(string name, int count, FPNode parent)
        {
            item = name;
            frequent = count;
            Parent = parent;
            Children = new List<FPNode>();
        }
    }
}
