using System;
using System.Collections.Generic;
namespace BinaryTree
{ 



    public class Program
    {
        public static void Main()
        {

            Random rnd = new Random();
            const int digitsRange = 100;
            int count = 0;
            BTree bt = new BTree(digitsRange / 2); // setting root 
            Console.WriteLine("Count of elements - ");
            count =  Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < count; i++) 
            {
                bt.Add(rnd.Next(digitsRange));
            }
            BTreePrinter.Print(bt.root);
        }
    }
}
