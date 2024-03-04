using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Code
{
    //Khoi tao cau truc cua 1 itemset
    public struct ItemSet
    {
        public int TID { get; set;}
        public List<string> Items { get; set;}
        public ItemSet (int TID, List<string> items)
        {
            this.TID = TID;
            this.Items = items;
        }
    }
    public sealed class ItemSets
    {
        public List<ItemSet> ListProduct { get; set;}
        private bool check = true;
        public ItemSets ()
        {
            this.ListProduct = new List<ItemSet> ();
        }
        public int CountItemSet()
        {
            return this.ListProduct.Count;
        }
        public void CreateFrequentItemSet (ItemSet item)
        {
            if (this.check)
                if (!this.ListProduct.Contains(item))
                    this.ListProduct.Add(item);
                else throw new Exception("Ung vien da nam trong tap danh muc");
            else throw new Exception("Khong the them ung vien");
        }
        public void Lock()
        {
            this.check = false;
        }
    }
   
}
