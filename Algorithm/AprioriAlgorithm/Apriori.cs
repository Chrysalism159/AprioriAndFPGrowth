
using Algorithm.Base;
using Algorithm.Code;
using System;
using System.Collections.Generic;
using System.IO;
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
        System.IO.StreamWriter file;
        List<string> product = new List<string>();

        public Apriori(ItemSets itemSets, int support, int confidence) {
            this.ItemSet = itemSets;
            this.support = support > 100 ? 100 : (support < 0 ? 0 : support);
            this.confidence = confidence > 100 ? 100 : (confidence < 0 ? 0 : confidence);
            this.min_support = this.ItemSet.ListProduct.Count * support / 100;
        }
        public int GetGroupCountInItemSets(string[] group)
        {
            int temp = 0;
            foreach (var product in this.ItemSet.ListProduct)
            {
                if (!group.Except(product.Items).Any())
                {
                    temp++;
                }
            }
            return temp;
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
        public Dictionary<string, int> RemoveValueLowerMinSup(Dictionary<string, int> products, int min_support)
        {
            return products.Where(pair => pair.Value >= min_support).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        public Dictionary<string[], int> RemoveValueLowerMinSup(Dictionary<string[], int> products, int min_support)
        {
            return products.Where(pair => pair.Value >= min_support).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        static List<List<string>> GenerateSubsets(List<string> data, int subsetSize)
        {
            List<List<string>> subsets = new List<List<string>>();
            GenerateSubsetsRecursive(data, subsetSize, 0, new List<string>(), subsets);
            return subsets;
        }

        static void GenerateSubsetsRecursive<T>(List<T> data, int subsetSize,int currentIndex,  List<T> currentSubset, List<List<T>> subsets)
        {
            if (currentSubset.Count == subsetSize)
            {
                subsets.Add(new List<T>(currentSubset));
                return;
            }
            for (int i = currentIndex; i < data.Count; i++)
            {
                currentSubset.Add(data[i]);
                GenerateSubsetsRecursive(data, subsetSize, i + 1, currentSubset, subsets);
                currentSubset.RemoveAt(currentSubset.Count - 1);
            }
        }
        public Dictionary<string[], int> GroupProduct(Dictionary<string, int> products)
        {
            Dictionary<string[], int> value = new Dictionary<string[], int>();
            foreach (KeyValuePair<string, int> main in products)
            {
                string mainKey = main.Key;
                foreach (KeyValuePair<string, int> sub in products)
                {
                    string subKey = sub.Key;
                    if (!mainKey.Equals(subKey))
                    {
                        string[] group1 = new string[] { mainKey, subKey };
                        string[] group2 = group1.Reverse().ToArray();
                        if (!value.ContainsKey(group1) && !value.ContainsKey(group2))
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
            file.WriteLine(title);
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            file.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("Product\t\t\t\tCount");
            file.WriteLine("Sản phẩm\t\t\t\tĐộ hỗ trợ");
            Console.WriteLine("--------------------");
            file.WriteLine("--------------------");
            foreach (KeyValuePair<string[], int> product in productCounts)
            {
                Console.WriteLine($"{string.Join(",", product.Key)}\t\t\t\t{product.Value}");
                file.WriteLine($"{string.Join(",", product.Key)}\t\t\t\t{product.Value}");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            file.WriteLine("---------------------------------------------------------------------------------------------");
        }
        public void PrintCounts(Dictionary<string, int> productCounts, string title)
        {
            Console.WriteLine(title);
            file.WriteLine(title);
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            file.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("Sản phẩm\t\t\t\tĐộ hỗ trợ");
            file.WriteLine("Sản phẩm\t\t\t\tĐộ hỗ trợ");
            Console.WriteLine("--------------------");
            file.WriteLine("--------------------");
            foreach (KeyValuePair<string, int> product in productCounts)
            {
                Console.WriteLine($"count({product.Key})\t\t\t\t{product.Value}");
                file.WriteLine($"{product.Key}\t\t\t\t{product.Value}");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            file.WriteLine("---------------------------------------------------------------------------------------------");
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
            string[] XY = itemset.ToArray();
            int sup_XY = GetGroupCountInItemSets(XY);
            subsets.RemoveAt(0); // Loại bỏ tập rỗng
            subsets.Remove(itemset); // Loại bỏ tập hợp chính nó
            foreach (var subset in subsets)
            {
                string[] X = subset.ToArray();
                int sup_X = GetGroupCountInItemSets(X);
                var remainingItems = itemset.Except(subset).ToList();
                foreach (var item in remainingItems)
                {
                    double confX_Y = ((double)sup_XY / (double)sup_X) * 100;
                    confX_Y = Math.Round(confX_Y, 2);
                    if (confX_Y > this.confidence)
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

        //tinh va in do ho tro cua cac luat ket hop
        public void PrintFinalValues(Dictionary<string[], int> group)
        {
            //
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            file.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("RESULTS");
            file.WriteLine("Dự đoán khả năng mua hàng của sản phẩm {A} khi mua sản phầm {B}");
            Console.WriteLine("--------------------");
            file.WriteLine("--------------------");
            foreach (KeyValuePair<string[], int> product in group)
            {
                string[] keys = product.Key;
                List<string> freq = new List<string>(keys);
                List<AssociationRule> rules = new List<AssociationRule>();
                rules = GenerateAssociationRules(freq);
                foreach (var rule in rules)
                {
                    file.WriteLine($"Khả năng mua hàng của sản phẩm [{string.Join(", ", rule.Rule.Item1)} ] khi mua [ {string.Join(", ", rule.Rule.Item2)}:  {rule.Support}%");
                }
            }
            file.WriteLine("---------------------------------------------------------------------------------------------");
        }

        public void PrintThresholdRule(string[] keys, string[] ins, string[] outs)
        {
            int XYZ = GetGroupCountInItemSets(keys);
            int N = GetGroupCountInItemSets(ins);
            double result = (double)XYZ / (double)N * 100;
            //Console.Write($"trust({string.Join(",", ins)} -> {string.Join(",", outs)})");
            file.Write($"Độ tin cậy ({string.Join(",", ins)} -> {string.Join(",", outs)})");
            //Console.Write($"The probability of being [{string.Join(",", outs)}] on the product set [{string.Join(",", ins)}] \t{Math.Round(result, 2)}%");
            file.Write($"Khả năng mua hàng của sản phẩm [{string.Join(",", outs)}] khi mua [{string.Join(",", ins)}] \t{Math.Round(result, 2)}%");
        }

        public Dictionary<string[], int> MergeGroupProducts(Dictionary<string[], int> grouped)
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
        public bool CreateFileRead(string path)
        {
            if(path != null)
            {
                try
                {
                    string fileName = path + "_" + "AprioriAlgorithm.txt";
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                    file = new System.IO.StreamWriter(fileName);
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

        public void RunAlgorithm(string path)
        {
            Dictionary<string, int> productCounts = this.CountNumberOfItem();
            if(!CreateFileRead(path))
            {
                Console.WriteLine("Path is empty");
            }
            List<string> product = new List<string>();
            foreach (KeyValuePair<string, int> item in productCounts)
            {
                product.Add(item.Key);
            }
            file.WriteLine($"Độ hỗ trợ tối thiểu = {this.support}%");
            file.WriteLine($"Độ tin cậy tối thiểu  = {this.confidence}%");
            Console.WriteLine();
            
            this.PrintCounts(productCounts, "Giá trị hỗ trợ của các tập mục đơn");
            productCounts = this.RemoveValueLowerMinSup(productCounts, this.min_support);
            this.PrintCounts(productCounts, "Danh mục các tập mục đơn thỏa mãn độ hỗ trợ tối thiểu");
            Dictionary<string[], int> freqItemset = new Dictionary<string[], int>();
            for (int i = 2; i <= productCounts.Count; i++)
            {
                List<List<string>> subsets = GenerateSubsets(product, i);
                foreach (var subset in subsets)
                {
                    string[] keys = subset.ToArray();
                    int values = this.GetGroupCountInItemSets(keys);
                    if(!freqItemset.ContainsKey(keys))
                        freqItemset.Add(keys, values);

                }
                freqItemset = this.RemoveValueLowerMinSup(freqItemset, this.support);
                Dictionary<string[], int> frequentItemset = new Dictionary<string[], int>();
                frequentItemset = freqItemset;
                this.PrintGroups(frequentItemset, "Danh sách " + i + " sản phẩm thỏa mãn độ hỗ trợ tối thiểu");
            }
            this.PrintFinalValues(freqItemset);

            Dictionary<string[], int> grouped = this.GroupProduct(productCounts);
            //this.PrintGroups(grouped, "Support values for dual product groups");
            //grouped = this.RemoveValueLowerMinSup(grouped, this.min_support);
            //this.PrintGroups(grouped, "Two product groups with equal to or greater than the threshold support value");

            //grouped = this.MergeGroupProducts(grouped);
            ////this.PrintGroups(grouped, "Three product groups with equal to or greater than the threshold support value");
            //this.PrintFinalValues(grouped);
            file.Close();
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
