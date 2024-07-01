
using Algorithm.Base;
using Algorithm.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Algorithm
{
    public sealed class Apriori
    {
        private ItemSets ItemSet;
        private int support;
        private int confidence;
        private int min_support;
        System.IO.StreamWriter file;
        string path;

        public Apriori(ItemSets itemSets, int support, int confidence)
        {
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

        static void GenerateSubsetsRecursive<T>(List<T> data, int subsetSize, int currentIndex, List<T> currentSubset, List<List<T>> subsets)
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

        private int GetSupportCount(Dictionary<string[], int> freqItemset, string[] properties)
        {
            int returnValue = 0;
            foreach (KeyValuePair<string[], int> product in freqItemset)
            {
                if (properties.SequenceEqual(product.Key))
                    returnValue = product.Value;
            }
            return returnValue;
        }
        //ghep cac tap con thanh luat ket hop
        private List<AssociationRule> GenerateAssociationRules(List<string> itemset, Dictionary<string[], int> freqItemset)
        {
            //itemset = "a,b,c,d"
            List<AssociationRule> rules = new List<AssociationRule>();
            var subsets = GeneratePowerSet(itemset);
            string[] XY = itemset.ToArray();
            int sup_XY = GetSupportCount(freqItemset, XY);
            foreach (var subset in subsets)
            {
                string[] X = subset.Key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                int sup_X = 0;
                if (X.Length > 1)
                    sup_X = GetSupportCount(freqItemset, X);
                else
                    sup_X = GetGroupCountInItemSets(X);
                double confX_Y = ((double)sup_XY / (double)sup_X) * 100;
                confX_Y = Math.Round(confX_Y, 2);
                if (confX_Y > this.confidence)
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
        public void PrintGroups(Dictionary<string[], int> productCounts, string title)
        {
            file.WriteLine(title);
            file.WriteLine("---------------------------------------------------------------------------------------------");
            file.WriteLine("Sản phẩm\t\t\t\tĐộ hỗ trợ");
            file.WriteLine("--------------------");
            foreach (KeyValuePair<string[], int> product in productCounts)
            {
                file.WriteLine($"{string.Join(",", product.Key)}\t\t\t\t{product.Value}");
            }
            file.WriteLine("---------------------------------------------------------------------------------------------");
        }
        public void PrintCounts(Dictionary<string, int> productCounts, string title)
        {
            file.WriteLine(title);
            file.WriteLine("---------------------------------------------------------------------------------------------");
            file.WriteLine("Sản phẩm\t\t\t\tĐộ hỗ trợ");
            file.WriteLine("--------------------");
            foreach (KeyValuePair<string, int> product in productCounts)
            {
                file.WriteLine($"{product.Key}\t\t\t\t{product.Value}");
            }
            file.WriteLine("---------------------------------------------------------------------------------------------");
        }
        //tinh va in do ho tro cua cac luat ket hop
        public void PrintFinalValues(Dictionary<string[], int> group, string path)
        {
            //DataTable table = new DataTable();
            //table.Columns.Add("Luật kết hợp", typeof(string));
            //table.Columns.Add("Tỷ lệ mua", typeof(string));
            file.WriteLine("---------------------------------------------------------------------------------------------");
            file.WriteLine("**************************************");
            file.WriteLine("--------------------");
            List<AssociationRule> totalRules = new List<AssociationRule>();
            foreach (KeyValuePair<string[], int> product in group)
            {
                string[] keys = product.Key;
                List<string> freq = new List<string>(keys);
                List<AssociationRule> rules = new List<AssociationRule>();
                rules = GenerateAssociationRules(freq, group);
                totalRules.AddRange(rules);
                

            }
            totalRules = totalRules.OrderByDescending(m => m.Support).ToList();
            int i = 1;
            foreach (var rule in totalRules)
            {
                //string luat = $"[{string.Join(", ", rule.Rule.Item1)} ] ----> [ {string.Join(", ", rule.Rule.Item2)}]";
                //string percent = rule.Support + "%";
                //table.Rows.Add(luat, percent);
                //file.WriteLine($"Khả năng mua hàng của sản phẩm [{string.Join(", ", rule.Rule.Item1)} ] khi mua [ {string.Join(", ", rule.Rule.Item2)}]:  {rule.Support}%");
                file.WriteLine($"{i}. [{string.Join(", ", rule.Rule.Item1)}] --> [{string.Join(", ", rule.Rule.Item2)}]:  {rule.Support}%");
                i++;
            }

            //WriteDataTableToCsv(table);
            file.WriteLine("---------------------------------------------------------------------------------------------");
        }

        public bool CreateFileRead(string path)
        {
            if (path != null)
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
            this.path = path;
            Dictionary<string, int> productCounts = this.CountNumberOfItem();
            if (!CreateFileRead(path))
            {
                Console.WriteLine("Path is empty");
            }
            List<string> product = new List<string>();
            foreach (KeyValuePair<string, int> item in productCounts)
            {
                product.Add(item.Key);
            }
            file.WriteLine($"Độ hỗ trợ tối thiểu = {this.support}");
            file.WriteLine($"Độ tin cậy tối thiểu  = {this.confidence}%");
            Console.WriteLine();

            this.PrintCounts(productCounts, "Giá trị hỗ trợ của các tập mục đơn");
            productCounts = this.RemoveValueLowerMinSup(productCounts, this.min_support);
            this.PrintCounts(productCounts, "Danh mục các tập mục đơn thỏa mãn độ hỗ trợ tối thiểu");
            Dictionary<string[], int> freqItemset = new Dictionary<string[], int>();
            for (int i = 2; i <= productCounts.Count; i++)
            {
                Dictionary<string[], int> frequentItemset = new Dictionary<string[], int>();
                List<List<string>> subsets = GenerateSubsets(product, i);
                foreach (var subset in subsets)
                {
                    string[] keys = subset.ToArray();
                    int values = this.GetGroupCountInItemSets(keys);
                    if (!frequentItemset.ContainsKey(keys))
                        frequentItemset.Add(keys, values);

                }
                frequentItemset = this.RemoveValueLowerMinSup(frequentItemset, this.support);
                if (frequentItemset.Count > 0)
                {
                    this.PrintGroups(frequentItemset, "Danh sách " + i + " sản phẩm thỏa mãn độ hỗ trợ tối thiểu");
                }
                foreach (var freqItem in frequentItemset)
                {
                    freqItemset.Add(freqItem.Key, freqItem.Value);
                }
            }
            this.PrintFinalValues(freqItemset, path);
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
