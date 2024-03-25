using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.FPGrowthAlgorithm
{
    public class FPTree
    {
        public Node root;
        IDictionary<string, Node> headerTable;
        float minimumSupport;
        //flist
        List<Item> frequentItems;
        List<List<string>> transaction;
        private const int Radius = 20;
        private const int Gap = 50;

        public List<Item> FrequentItems
        {
            get { return frequentItems; }
            set { frequentItems = value; }
        }
        private FPTree()
        {
            root = new Node("");
            headerTable = new Dictionary<string, Node>();
            minimumSupport = 0f;
            frequentItems = new List<Item>();
        }
        public FPTree(List<List<string>> data, float minSup)
            : this()
        {
            minimumSupport = minSup;
            transaction = data;

            //tinh tan so xuat hien cua cac item trong database
            CalculateFrequentItems();
            //sắp xếp theo thứ tự giảm dần của flist
            frequentItems = frequentItems.OrderByDescending(x => x.SupportCount).ToList();

            //sắp xếp các giao dịch theo thứ tự trong flist
            List<string> aTransaction = new List<string>();
            foreach(var trans in transaction)
            {
                aTransaction = trans;
                InsertTransaction(aTransaction);
            }
        }

        private void InsertTransaction(List<string> aTransaction)
        {
            //filter transactions to get frequent items in sorted order of frequentItems
            //sắp xếp các giao dịch theo thứ tự trong flist
            List<Item> items = frequentItems.FindAll
                (
                    delegate (Item anItem)
                    {
                        return aTransaction.Exists(x => x == anItem.nameItem);
                    }
                );
            //khởi tạo nút gốc root{null,0}
            Node tempRoot = root;
            Node tempNode;
            //vẽ nhánh cây FP theo từng giao dịch
            foreach (Item anItem in items)
            {
                //khởi tạo nút con mới sau nút gốc
                Node aNode = new Node(anItem.nameItem);
                //giá trị tích lũy ban đầu bằng 1
                aNode.FpCount = 1;
                //kiểm tra trong nút gốc đã có nút con hiện tại hay chưa
                if ((tempNode = tempRoot.Children.Find(c => c.NameNode == aNode.NameNode)) != null)
                {
                    //nếu có thì tăng giá trị tích lũy lên 1
                    tempNode.FpCount++;
                    //gán nút hiện tại thành nút cha
                    tempRoot = tempNode;
                }
                else
                {
                    //thêm nút mới vào cây FP tree
                    tempRoot.AddChild(aNode);
                    tempRoot = aNode;
                    if (headerTable.ContainsKey(aNode.NameNode))
                    {
                        aNode.NextHeader = headerTable[aNode.NameNode];
                        headerTable[aNode.NameNode] = aNode;
                    }
                    else
                    {
                        headerTable[aNode.NameNode] = aNode;
                    }
                }
            }
        }

        private void CalculateFrequentItems()
        {
            List<Item> items = CalculateFrequencyAllItems();

            foreach (Item anItem in items)
            {
                if (anItem.SupportCount >= minimumSupport)
                {
                    frequentItems.Add(anItem.Clone());
                }
            }
        }
        //chèn nhánh vào cây FP
        private void InsertBranch(List<Node> branch)
        {
            Node tempRoot = root;
            for (int i = 0; i < branch.Count; ++i)
            {
                Node aNode = branch[i];
                Node tempNode = tempRoot.Children.Find(x => x.NameNode == aNode.NameNode);
                if (null != tempNode)
                {
                    tempNode.FpCount += aNode.FpCount;
                    tempRoot = tempNode;
                }
                else
                {
                    while (i < branch.Count)
                    {
                        aNode = branch[i];
                        aNode.Parent = tempRoot;
                        tempRoot.AddChild(aNode);
                        if (headerTable.ContainsKey(aNode.NameNode))
                        {
                            aNode.NextHeader = headerTable[aNode.NameNode];
                        }

                        headerTable[aNode.NameNode] = aNode;

                        tempRoot = aNode;
                        ++i;

                    }
                    break;
                }
            }
        }
        public Node TreeRoot()
        {
            Node node = root;
            return node;
        }
        public int GetTotalSupportCount(string itemSymbol)
        {
            int sCount = 0;
            Node node = headerTable[itemSymbol];
            while (null != node)
            {
                sCount += node.FpCount;
                node = node.NextHeader;
            }
            return sCount;
        }
        private void PrintNode(Node node, int level)
        {
            if (node == null)
                return;

            // In nút hiện tại
            string indent = new string(' ', level * 4);
            Console.WriteLine(indent + (node.NameNode ?? "Root") + " (" + node.FpCount + ")");

            // In các nút con
            foreach (var child in node.Children)
            {
                PrintNode(child, level + 1);
            }
        }
        public void PrintTree()
        {
            PrintNode(root, 0);
        }
        
        public FPTree Project(Item anItem)
        {
            FPTree tree = new FPTree();
            tree.minimumSupport = minimumSupport;

            Node startNode = headerTable[anItem.nameItem];

            while (startNode != null)
            {
                //Giá trị hỗ trợ của tập thường xuyên đang xét.
                int projectedFPCount = startNode.FpCount;
                Console.Write("\nSupport: " + projectedFPCount);
                Node tempNode = startNode;
                List<Node> aBranch = new List<Node>();
                while (null != tempNode.Parent)
                {
                    Node parentNode = tempNode.Parent;
                    //Kiểm tra nút hiện tại có phải nút con trực tiếp của nút gốc hay không
                    if (parentNode.IsNull())
                    {
                        //nếu nút cha của nút hiện tại là root thì dừng vòng lặp
                        break;
                    }
                    //nếu nút không phải nút cha thì khởi tạo nút mới với giá trị tích lũy bằng gía trị tích lũy của nút đang xét
                    Node newNode = new Node(parentNode.NameNode);
                    newNode.FpCount = projectedFPCount;
                    //thêm nút vừa khởi tạo vào nhánh
                    aBranch.Add(newNode);
                    //tiếp tục xét lên nút cha của nút hiện tại
                    tempNode = tempNode.Parent;
                }
                //đảo ngược nhánh để gán vào gốc root
                aBranch.Reverse();
                tree.InsertBranch(aBranch);
                startNode = startNode.NextHeader;
            }
            
            IDictionary<string, Node> inFrequentHeaderTable = tree.headerTable.
                Where(x => tree.GetTotalSupportCount(x.Value.NameNode) < minimumSupport).
                ToDictionary(p => p.Key, p => p.Value);

            tree.headerTable = tree.headerTable.
                Where(x => tree.GetTotalSupportCount(x.Value.NameNode) >= minimumSupport).
                ToDictionary(p => p.Key, p => p.Value);

            foreach (KeyValuePair<string, Node> hEntry in inFrequentHeaderTable)
            {
                Node temp = hEntry.Value;
                while (null != temp)
                {
                    Node tempNext = temp.NextHeader;
                    Node tempParent = temp.Parent;
                    tempParent.Children.Remove(temp);
                    temp = tempNext;
                }
            }

            tree.frequentItems = frequentItems.FindAll
            (
                delegate (Item item)
                {
                    return tree.headerTable.ContainsKey(item.nameItem);
                }
            );
            return tree;
        }

        //tính độ hỗ trợ của 1-itemset trong cơ sở dữ liệu 
        public List<Item> CalculateFrequencyAllItems()
        {
            List<Item> items = new List<Item>();
            IDictionary<string, int> dictionary = new Dictionary<string, int>(); // temporary associative array for counting frequency of items
            if (transaction != null)
            {
                foreach (var trans in transaction)
                {
                    foreach(var item in trans)
                    {
                        if (item.Length == 0) continue;
                        if (dictionary.ContainsKey(item))
                            dictionary[item]++; // increase frequency of item
                        else
                            dictionary[item] = 1; //set initial frequency
                    }
                }
                //insert all the item, frequency pair in items list
                foreach (KeyValuePair<string, int> pair in dictionary)
                {
                    Item anItem = new Item(pair.Key, pair.Value);
                    items.Add(anItem);
                }
            }

            return items;
        }


    }
}
