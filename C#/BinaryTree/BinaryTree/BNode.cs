using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class BNode
    {
        public int item;
        public BNode right;
        public BNode left;

        public BNode(int item)
        {
            this.item = item;
        }
    }
}
