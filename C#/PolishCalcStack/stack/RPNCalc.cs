using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackRPN
{
    public class RPNCalc
    {
        private readonly char[] _separators = new char[] { ' ', '\t' };
        private Stack<string> _tokenizedStack = new Stack<string>();
        private void AssignStack(string input)
        {
            this._tokenizedStack = new Stack<string>
                      (input.Split(this._separators, StringSplitOptions.RemoveEmptyEntries));
        }

        public double EvaluateResult()
        {
            string token = this._tokenizedStack.Pop();
            double x, y;
            if (!Double.TryParse(token, out x))
            {
                y = EvaluateResult(); x = EvaluateResult();
                if (token == "+") x += y;
                else if (token == "-") x -= y;
                else if (token == "*") x *= y;
                else if (token == "/") x /= y;
                else throw new Exception();
            }
            return x;
        }
        public int GetCount()
        {
            return _tokenizedStack.Count;
        }
        public void ReadInput()
        {
            AssignStack(Console.ReadLine());
        }
    }
}
