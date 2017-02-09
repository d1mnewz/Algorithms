using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch
{
    class Program
    {
        public static int BinarySearchIterative(int[] inputArray, int key)
        {
            int min = 0;
            int max = inputArray.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (key == inputArray[mid])
                {
                    return mid;
                }
                else if (key < inputArray[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return -1;
        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int length = 20;
            int magicNumber = 42;
            int[] arr = new int[length];
            for (int i = 0; i < length; i++)
            {
                arr[i] = rnd.Next(100);

            }
            Array.Sort(arr);
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            if (arr.Contains(magicNumber))
            {
                Console.WriteLine($"Magic Number {magicNumber} was found on arr[{BinarySearchIterative(arr, magicNumber)}].");
            }
            else Console.WriteLine($"Magic number {magicNumber} wasn't found :(");
        }
    }
}
