﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.FPGrowthAlgorithm
{
    public class Node
    {
        string nameNode;
        int fpCount;
        Node nextHeader;
        Node parent;
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public Node Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public Node NextHeader
        {
            get { return nextHeader; }
            set { nextHeader = value; }
        }
        List<Node> children;

        public List<Node> Children
        {
            get { return children; }
            set { children = value; }
        }
        public string NameNode
        {
            get { return nameNode; }
        }
        public int FpCount
        {
            get { return fpCount; }
            set { fpCount = value; }
        }
        public void PrintNode()
        {
            Console.WriteLine("Name node: " + nameNode + " ------- value: " + fpCount);
        }
        private Node()
        {
            fpCount = 0;
            nextHeader = null;
            children = new List<Node>();
            parent = null;
        }
        public Node(string _symbol)
            : this()
        {
            nameNode = _symbol;
            if (nameNode.Length != 0)
                fpCount = 1;
        }
        public bool IsNull()
        {
            return nameNode.Length == 0;
        }
        public void AddChild(Node child)
        {
            child.parent = this;
            children.Add(child);
        }

    }
}
