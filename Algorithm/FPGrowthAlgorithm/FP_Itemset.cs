using Algorithm.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.FPGrowthAlgorithm
{
    public class Item
    {
        private string valueItem;
        private int supportCount;

        public int SupportCount
        {
            get { return supportCount; }
            set { supportCount = value; }
        }
        public string nameItem
        {
            get { return valueItem; }
        }

        //constructors
        public Item()
        {
            valueItem = null;
            supportCount = -1;
        }
        public Item(string _symbol)
            : this()
        {
            valueItem = _symbol;
        }
        public Item(string _symbol, int _supportCount)
            : this()
        {
            valueItem = _symbol;
            supportCount = _supportCount;
        }
        public Item Clone()
        {
            Item item = new Item(valueItem, supportCount);
            return item;
        }
    }
    public class FP_Itemset
    {
        public List<Item> _itemset; //list of transactions in database


        private int supportCount; // support count of this item set

        public int SupportCount
        {
            get { return supportCount; }
            set { supportCount = value; }
        }

        //constructor
        public FP_Itemset()
        {
            _itemset = new List<Item>();
            supportCount = -1;
        }
        //add item into item set
        public void AddItem(Item item)
        {
            _itemset.Add(item);
            supportCount = -1;
        }
        //remove item
        public Item GetItem(int position)
        {
            if (position < _itemset.Count)
                return _itemset[position];
            else
                return null;
        }
        //add item into item set
        public bool IsEmpty()
        {
            return _itemset.Count == 0;
        }
        //add item into item set
        public int GetLength()
        {
            return _itemset.Count;
        }
        public FP_Itemset Clone()
        {
            FP_Itemset itemset = new FP_Itemset();
            itemset.SupportCount = SupportCount;
            foreach (Item item in _itemset)
            {
                itemset.AddItem(item.Clone());
            }
            return itemset;
        }
        public string GetInfoString()
        {
            string info = "";

            foreach (Item item in _itemset)
            {
                info += (" " + item.nameItem.ToString());
            }

            return info;
        }
        public void Print()
        {
            Console.Write("Support count: " + SupportCount + "\nName: ");
            foreach (Item item in _itemset)
            {
                Console.Write(item.nameItem.ToString() + " ");
            }
            Console.WriteLine();
        }

        public Item GetLastItem()
        {
            return _itemset.Last();
        }
    }
}
