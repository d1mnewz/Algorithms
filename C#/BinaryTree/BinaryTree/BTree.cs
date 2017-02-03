using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class BTree
    {
        public BNode root;
        private int _count;
        private IComparer<int> _comparer = Comparer<int>.Default;


        public BTree(int data)
        {
            root = new BNode(data);
            _count = 1;
        }


        public bool Add(int Item)
        {
            if (root == null)
            {
                root = new BNode(Item);
                _count++;
                return true;
            }
            else
            {
                return Add_Sub(root, Item);
            }
        }

        private bool Add_Sub(BNode Node, int Item)
        {
            
            if (_comparer.Compare(Node.item, Item) < 0) // if node.item < item
            {
                if (Node.right == null)
                {
                    Node.right = new BNode(Item);
                    _count++;
                    return true;
                }
                else
                {
                    return Add_Sub(Node.right, Item);
                }
            }
            else if (_comparer.Compare(Node.item, Item) > 0) // if node.item > item
            {
                if (Node.left == null)
                {
                    Node.left = new BNode(Item);
                    _count++;
                    return true;
                }
                else
                {
                    return Add_Sub(Node.left, Item);
                }
            }
            else // if node.item == item then dont add new node because it already exists;
            {
                return false; 
            }
        }
    }
}
