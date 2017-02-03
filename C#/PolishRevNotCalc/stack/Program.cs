using System;
using System.Collections.Generic;
namespace StackRPN
{
   
    class Program
    {
        public static void Main()
        {
            ReversePolishNotation rpn = new ReversePolishNotation();
            
            while (true)
            {
                try
                {
                    rpn.Parse(Console.ReadLine());
                    Console.WriteLine(rpn.Evaluate());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}