using Algorithm.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public sealed class Apriori
    {
        private ItemSets ItemSet;
        private int support;
        private int confidence;
        private int min_support;
        public Apriori(ItemSets itemSets, int support, int confidence) {
            this.ItemSet = itemSets;
            this.support = support > 100 ? 100 : (support < 0 ? 0 : support);
            this.confidence = confidence > 100 ? 100 : (confidence < 0 ? 0 : confidence);
            this.min_support = this.ItemSet.ListProduct.Count * support / 100;
        }
        public Dictionary<string, int> CountNumberOfItem()
        {
            Dictionary<string, int> value = new Dictionary<string, int>();
            for(int i=0;i<this.ItemSet.ListProduct.Count;i++)
            {
                for(int j = 0; j < this.ItemSet.ListProduct[i].Items.Count; j++)
                {
                    string product = ItemSet.ListProduct[i].Items[j];
                    if(value.ContainsKey(product))
                        value[product]++;
                    else
                        value.Add(product, 1);
                }
            }
            return value;
        }
        public Dictionary<string, int> RemoveValueLowerMinSup(Dictionary<string, int> products)
        {
            return products.Where(pair => pair.Value >= this.min_support).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        public Dictionary<string[], int> RemoveValueLowerMinSup(Dictionary<string[], int> products)
        {
            return products.Where(pair => pair.Value >= this.min_support).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        public int GetGroupCountInItemSets(string[] group)
        {
            int temp = 0;
            for (int i = 0; i < this.ItemSet.ListProduct.Count; i++)
            {
                if (group.Except(this.ItemSet.ListProduct[i].Items).Count() == 0)
                {
                    temp++;
                }
            }
            return temp;
        }
        public Dictionary<string[], int> GroupProduct(Dictionary<string, int> products)
        {
            Dictionary<string[], int> value = new Dictionary<string[], int>();
            foreach(KeyValuePair<string, int> main in products)
            {
                string mainKey = main.Key;
                foreach(KeyValuePair<string, int> sub in products)
                {
                    string subKey = sub.Key;
                    if(!mainKey.Equals(subKey))
                    {
                        string[] group1 = new string[] {mainKey, subKey };
                        string[] group2 = group1.Reverse().ToArray();
                        if(!value.ContainsKey(group1) && !value.ContainsKey(group2))
                        {
                            value.Add(group1, GetGroupCountInItemSets(group1));
                        }
                    }
                }
            }
            return value;
        }
        public void PrintGroups(Dictionary<string[], int> productCounts, string title)
        {
            Console.WriteLine(title);
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("Product\t\tCount");
            Console.WriteLine("--------------------");
            foreach (KeyValuePair<string[], int> product in productCounts)
            {
                Console.WriteLine($"{string.Join(",", product.Key)}\t{product.Value}");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }
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
        private void PrintFinalValues(Dictionary<string[], int> group)
        {

            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("RESULTS");
            Console.WriteLine("--------------------");
            int index = 1;
            foreach (KeyValuePair<string[], int> product in group)
            {
                string[] keys = product.Key;

                for (int i = -1; i < keys.Length; i++)
                {
                    Console.Write($"RESULT {index} : ");

                    string[] ins;
                    string[] outs;

                    if (i == -1)
                    {
                        ins = new string[] { keys[0], keys[1] };
                        outs = keys.Except(ins).ToArray();
                        PrintThresholdRule(keys, ins, outs);
                        index++;
                        Console.WriteLine();
                        continue;
                    }

                    ins = new string[] { keys[i] };
                    outs = keys.Except(ins).ToArray();

                    PrintThresholdRule(keys, ins, outs);

                    index++;
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }

        private void PrintThresholdRule(string[] keys, string[] ins, string[] outs)
        {
            int XYZ = GetGroupCountInItemSets(keys);
            int N = GetGroupCountInItemSets(ins);
            double result = (double)XYZ / (double)N * 100;
            Console.Write($"trust({string.Join(",", ins)} -> {string.Join(",", outs)})");
            Console.Write($"The probability of being [{string.Join(",", outs)}] on the product set [{string.Join(",", ins)}] \t%{result}");
        }

        private Dictionary<string[], int> MergeGroupProducts(Dictionary<string[], int> grouped)
        {
            Dictionary<string[], int> temp = new Dictionary<string[], int>(new ArrayComparer());
            List<string> datas = new List<string>();
            foreach (KeyValuePair<string[], int> main in grouped)
            {
                string[] keys = main.Key;
                for (int i = 0; i < keys.Length; i++)
                {
                    if (!datas.Contains(keys[i]))
                    {
                        datas.Add(keys[i]);
                    }
                }
            }
            temp.Add(datas.ToArray(), GetGroupCountInItemSets(datas.ToArray()));
            return temp;
        }
        public void Train()
        {
            Dictionary<string, int> productCounts = this.CountNumberOfItem();

            Console.WriteLine($"support(threshold) = %{this.support}");
            Console.WriteLine($"trust(threshold)  = %{this.confidence}");
            Console.WriteLine();

            this.PrintCounts(productCounts, "Support Values");
            productCounts = this.RemoveValueLowerMinSup(productCounts);
            this.PrintCounts(productCounts, "Products with equal to or greater than the threshold support value");

            Dictionary<string[], int> grouped = this.GroupProduct(productCounts);
            this.PrintGroups(grouped, "Support values for dual product groups");
            grouped = this.RemoveValueLowerMinSup(grouped);
            this.PrintGroups(grouped, "Two product groups with equal to or greater than the threshold support value");
            grouped = this.MergeGroupProducts(grouped);
            this.PrintGroups(grouped, "Three product groups with equal to or greater than the threshold support value");
            this.PrintFinalValues(grouped);
        }
    }
    public sealed class ArrayComparer : IEqualityComparer<string[]>
    {
        public bool Equals(string[] x, string[] y)
        {
            if (x[0] == y[0])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(string[] obj)
        {
            return obj[0].GetHashCode() + obj[1].GetHashCode();
        }
    }
}
