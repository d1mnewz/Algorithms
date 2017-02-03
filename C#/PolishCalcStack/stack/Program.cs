using System;
using System.Collections.Generic;
namespace StackRPN
{
   
    class Program
    {
        public static void Main()
        {
            RPNCalc obj = new RPNCalc();
           
            while (true)
            {
                obj.ReadInput();
                if (obj.GetCount() == 0) // case of empty string
                {
                    continue;
                }
                try
                {
                    double result = obj.EvaluateResult();
                    if (obj.GetCount() != 0) 
                    {
                        throw new Exception();
                    }
                    Console.WriteLine(result);
                }
                catch (Exception) { Console.WriteLine("error"); }
            }
        }
    }
}