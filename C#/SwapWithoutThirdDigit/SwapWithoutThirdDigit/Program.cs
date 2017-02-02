using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapWithoutThirdDigit
{
    class Program
    {
        public static void Swap(ref int a, ref int b)
        {
                       // a = 15 ; b = 10;
            a = a + b; // a = 15 ; b = 10;
            b = a - b; // a == 15; b = 5 ;
            a = a - b; // a == 10; b == 5;
        }
        static void Main(string[] args)
        {
            int First = 15;
            int Second = 10;
            Console.WriteLine($"First - {First} & Second - {Second}");
            Swap(ref First, ref Second);
            Console.WriteLine($"First - {First} & Second - {Second}");
        }
    }
}
