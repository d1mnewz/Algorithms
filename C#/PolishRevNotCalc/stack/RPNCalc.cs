using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StackRPN
{
    public enum TokenType
    {
        None,
        Number,
        Constant,
        Plus,
        Minus,
        Multiply,
        Divide,
        Exponent,
        UnaryMinus,
        Sine,
        Cosine,
        Tangent,
        LeftParenthesis,
        RightParenthesis
    }

    public struct ReversePolishNotationToken
    {
        public string TokenValue;
        public TokenType TokenValueType;
    }

    public class ReversePolishNotation
    {
        private Queue output;
        private Stack ops;

        private string sOriginalExpression;
        public string OriginalExpression
        {
            get { return sOriginalExpression; }
        }

        private string sTransitionExpression;
        public string TransitionExpression
        {
            get { return sTransitionExpression; }
        }

        private string sPostfixExpression;
        public string PostfixExpression
        {
            get { return sPostfixExpression; }
        }

        public ReversePolishNotation()
        {
            sOriginalExpression = string.Empty;
            sTransitionExpression = string.Empty;
            sPostfixExpression = string.Empty;
        }

        public void Parse(string Expression)
        {
            output = new Queue();
            ops = new Stack();

            sOriginalExpression = Expression;

            string sBuffer = Expression.ToLower();
            // captures numbers. Anything like 11 or 22.34 is captured
            sBuffer = Regex.Replace(sBuffer, @"(?<number>\d+(\.\d+)?)", " ${number} ");
            // captures these symbols: + - * / ^ ( )
            sBuffer = Regex.Replace(sBuffer, @"(?<ops>[+\-*/^()])", " ${ops} ");
            // captures alphabets. Currently captures the two math constants PI and E,
            // and the 3 basic trigonometry functions, sine, cosine and tangent
            sBuffer = Regex.Replace(sBuffer, "(?<alpha>(pi|e|sin|cos|tan))", " ${alpha} ");
            // trims up consecutive spaces and replace it with just one space
            sBuffer = Regex.Replace(sBuffer, @"\s+", " ").Trim();

            // The following chunk captures unary minus operations.
            // 1) We replace every minus sign with the string "MINUS".
            // 2) Then if we find a "MINUS" with a number or constant in front,
            //    then it's a normal minus operation.
            // 3) Otherwise, it's a unary minus operation.

            // Step 1.
            sBuffer = Regex.Replace(sBuffer, "-", "MINUS");
            // Step 2. Looking for pi or e or generic number \d+(\.\d+)?
            sBuffer = Regex.Replace(sBuffer, @"(?<number>(pi|e|(\d+(\.\d+)?)))\s+MINUS", "${number} -");
            // Step 3. Use the tilde ~ as the unary minus operator
            sBuffer = Regex.Replace(sBuffer, "MINUS", "~");

            sTransitionExpression = sBuffer;

            // tokenise it!
            string[] saParsed = sBuffer.Split(new char[] { ' ', '\t' });
            int i = 0;
            double tokenvalue;
            ReversePolishNotationToken token, opstoken;
            for (i = 0; i < saParsed.Length; ++i)
            {
                token = new ReversePolishNotationToken();
                token.TokenValue = saParsed[i];
                token.TokenValueType = TokenType.None;

                try
                {
                    tokenvalue = double.Parse(saParsed[i]);
                    token.TokenValueType = TokenType.Number;
                    // If the token is a number, then add it to the output queue.
                    output.Enqueue(token);
                }
                catch
                {
                    switch (saParsed[i])
                    {
                        case "+":
                            token.TokenValueType = TokenType.Plus;
                            if (ops.Count > 0)
                            {
                                opstoken = (ReversePolishNotationToken)ops.Peek();
                                // while there is an operator, o2, at the top of the stack
                                while (IsOperatorToken(opstoken.TokenValueType))
                                {
                                    // pop o2 off the stack, onto the output queue;
                                    output.Enqueue(ops.Pop());
                                    if (ops.Count > 0)
                                    {
                                        opstoken = (ReversePolishNotationToken)ops.Peek();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            // push o1 onto the operator stack.
                            ops.Push(token);
                            break;
                        case "-":
                            token.TokenValueType = TokenType.Minus;
                            if (ops.Count > 0)
                            {
                                opstoken = (ReversePolishNotationToken)ops.Peek();
                                // while there is an operator, o2, at the top of the stack
                                while (IsOperatorToken(opstoken.TokenValueType))
                                {
                                    // pop o2 off the stack, onto the output queue;
                                    output.Enqueue(ops.Pop());
                                    if (ops.Count > 0)
                                    {
                                        opstoken = (ReversePolishNotationToken)ops.Peek();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            // push o1 onto the operator stack.
                            ops.Push(token);
                            break;
                        case "*":
                            token.TokenValueType = TokenType.Multiply;
                            if (ops.Count > 0)
                            {
                                opstoken = (ReversePolishNotationToken)ops.Peek();
                                // while there is an operator, o2, at the top of the stack
                                while (IsOperatorToken(opstoken.TokenValueType))
                                {
                                    if (opstoken.TokenValueType == TokenType.Plus || opstoken.TokenValueType == TokenType.Minus)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        // Once we're in here, the following algorithm condition is satisfied.
                                        // o1 is associative or left-associative and its precedence is less than (lower precedence) or equal to that of o2, or
                                        // o1 is right-associative and its precedence is less than (lower precedence) that of o2,

                                        // pop o2 off the stack, onto the output queue;
                                        output.Enqueue(ops.Pop());
                                        if (ops.Count > 0)
                                        {
                                            opstoken = (ReversePolishNotationToken)ops.Peek();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            // push o1 onto the operator stack.
                            ops.Push(token);
                            break;
                        case "/":
                            token.TokenValueType = TokenType.Divide;
                            if (ops.Count > 0)
                            {
                                opstoken = (ReversePolishNotationToken)ops.Peek();
                                // while there is an operator, o2, at the top of the stack
                                while (IsOperatorToken(opstoken.TokenValueType))
                                {
                                    if (opstoken.TokenValueType == TokenType.Plus || opstoken.TokenValueType == TokenType.Minus)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        // Once we're in here, the following algorithm condition is satisfied.
                                        // o1 is associative or left-associative and its precedence is less than (lower precedence) or equal to that of o2, or
                                        // o1 is right-associative and its precedence is less than (lower precedence) that of o2,

                                        // pop o2 off the stack, onto the output queue;
                                        output.Enqueue(ops.Pop());
                                        if (ops.Count > 0)
                                        {
                                            opstoken = (ReversePolishNotationToken)ops.Peek();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            // push o1 onto the operator stack.
                            ops.Push(token);
                            break;
                        case "^":
                            token.TokenValueType = TokenType.Exponent;
                            // push o1 onto the operator stack.
                            ops.Push(token);
                            break;
                        case "~":
                            token.TokenValueType = TokenType.UnaryMinus;
                            // push o1 onto the operator stack.
                            ops.Push(token);
                            break;
                        case "(":
                            token.TokenValueType = TokenType.LeftParenthesis;
                            // If the token is a left parenthesis, then push it onto the stack.
                            ops.Push(token);
                            break;
                        case ")":
                            token.TokenValueType = TokenType.RightParenthesis;
                            if (ops.Count > 0)
                            {
                                opstoken = (ReversePolishNotationToken)ops.Peek();
                                // Until the token at the top of the stack is a left parenthesis
                                while (opstoken.TokenValueType != TokenType.LeftParenthesis)
                                {
                                    // pop operators off the stack onto the output queue
                                    output.Enqueue(ops.Pop());
                                    if (ops.Count > 0)
                                    {
                                        opstoken = (ReversePolishNotationToken)ops.Peek();
                                    }
                                    else
                                    {
                                        // If the stack runs out without finding a left parenthesis,
                                        // then there are mismatched parentheses.
                                        throw new Exception("Unbalanced parenthesis!");
                                    }

                                }
                                // Pop the left parenthesis from the stack, but not onto the output queue.
                                ops.Pop();
                            }

                            if (ops.Count > 0)
                            {
                                opstoken = (ReversePolishNotationToken)ops.Peek();
                                // If the token at the top of the stack is a function token
                                if (IsFunctionToken(opstoken.TokenValueType))
                                {
                                    // pop it and onto the output queue.
                                    output.Enqueue(ops.Pop());
                                }
                            }
                            break;
                        case "pi":
                            token.TokenValueType = TokenType.Constant;
                            // If the token is a number, then add it to the output queue.
                            output.Enqueue(token);
                            break;
                        case "e":
                            token.TokenValueType = TokenType.Constant;
                            // If the token is a number, then add it to the output queue.
                            output.Enqueue(token);
                            break;
                        case "sin":
                            token.TokenValueType = TokenType.Sine;
                            // If the token is a function token, then push it onto the stack.
                            ops.Push(token);
                            break;
                        case "cos":
                            token.TokenValueType = TokenType.Cosine;
                            // If the token is a function token, then push it onto the stack.
                            ops.Push(token);
                            break;
                        case "tan":
                            token.TokenValueType = TokenType.Tangent;
                            // If the token is a function token, then push it onto the stack.
                            ops.Push(token);
                            break;
                    }
                }
            }

            // When there are no more tokens to read:

            // While there are still operator tokens in the stack:
            while (ops.Count != 0)
            {
                opstoken = (ReversePolishNotationToken)ops.Pop();
                // If the operator token on the top of the stack is a parenthesis
                if (opstoken.TokenValueType == TokenType.LeftParenthesis)
                {
                    // then there are mismatched parenthesis.
                    throw new Exception("Unbalanced parenthesis!");
                }
                else
                {
                    // Pop the operator onto the output queue.
                    output.Enqueue(opstoken);
                }
            }

            sPostfixExpression = string.Empty;
            foreach (object obj in output)
            {
                opstoken = (ReversePolishNotationToken)obj;
                sPostfixExpression += string.Format("{0} ", opstoken.TokenValue);
            }
        }

        public double Evaluate()
        {
            Stack result = new Stack();
            double oper1 = 0.0, oper2 = 0.0;
            ReversePolishNotationToken token = new ReversePolishNotationToken();
            // While there are input tokens left
            foreach (object obj in output)
            {
                // Read the next token from input.
                token = (ReversePolishNotationToken)obj;
                switch (token.TokenValueType)
                {
                    case TokenType.Number:
                        // If the token is a value
                        // Push it onto the stack.
                        result.Push(double.Parse(token.TokenValue));
                        break;
                    case TokenType.Constant:
                        // If the token is a value
                        // Push it onto the stack.
                        result.Push(EvaluateConstant(token.TokenValue));
                        break;
                    case TokenType.Plus:
                        // NOTE: n is 2 in this case
                        // If there are fewer than n values on the stack
                        if (result.Count >= 2)
                        {
                            // So, pop the top n values from the stack.
                            oper2 = (double)result.Pop();
                            oper1 = (double)result.Pop();
                            // Evaluate the function, with the values as arguments.
                            // Push the returned results, if any, back onto the stack.
                            result.Push(oper1 + oper2);
                        }
                        else
                        {
                            // (Error) The user has not input sufficient values in the expression.
                            throw new Exception("Evaluation error!");
                        }
                        break;
                    case TokenType.Minus:
                        // NOTE: n is 2 in this case
                        // If there are fewer than n values on the stack
                        if (result.Count >= 2)
                        {
                            // So, pop the top n values from the stack.
                            oper2 = (double)result.Pop();
                            oper1 = (double)result.Pop();
                            // Evaluate the function, with the values as arguments.
                            // Push the returned results, if any, back onto the stack.
                            result.Push(oper1 - oper2);
                        }
                        else
                        {
                            // (Error) The user has not input sufficient values in the expression.
                            throw new Exception("Evaluation error!");
                        }
                        break;
                    case TokenType.Multiply:
                        // NOTE: n is 2 in this case
                        // If there are fewer than n values on the stack
                        if (result.Count >= 2)
                        {
                            // So, pop the top n values from the stack.
                            oper2 = (double)result.Pop();
                            oper1 = (double)result.Pop();
                            // Evaluate the function, with the values as arguments.
                            // Push the returned results, if any, back onto the stack.
                            result.Push(oper1 * oper2);
                        }
                        else
                        {
                            // (Error) The user has not input sufficient values in the expression.
                            throw new Exception("Evaluation error!");
                        }
                        break;
                    case TokenType.Divide:
                        // NOTE: n is 2 in this case
                        // If there are fewer than n values on the stack
                        if (result.Count >= 2)
                        {
                            // So, pop the top n values from the stack.
                            oper2 = (double)result.Pop();
                            oper1 = (double)result.Pop();
                            // Evaluate the function, with the values as arguments.
                            // Push the returned results, if any, back onto the stack.
                            result.Push(oper1 / oper2);
                        }
                        else
                        {
                            // (Error) The user has not input sufficient values in the expression.
                            throw new Exception("Evaluation error!");
                        }
                        break;
                    case TokenType.Exponent:
                        // NOTE: n is 2 in this case
                        // If there are fewer than n values on the stack
                        if (result.Count >= 2)
                        {
                            // So, pop the top n values from the stack.
                            oper2 = (double)result.Pop();
                            oper1 = (double)result.Pop();
                            // Evaluate the function, with the values as arguments.
                            // Push the returned results, if any, back onto the stack.
                            result.Push(Math.Pow(oper1, oper2));
                        }
                        else
                        {
                            // (Error) The user has not input sufficient values in the expression.
                            throw new Exception("Evaluation error!");
                        }
                        break;
                    case TokenType.UnaryMinus:
                        // NOTE: n is 1 in this case
                        // If there are fewer than n values on the stack
                        if (result.Count >= 1)
                        {
                            // So, pop the top n values from the stack.
                            oper1 = (double)result.Pop();
                            // Evaluate the function, with the values as arguments.
                            // Push the returned results, if any, back onto the stack.
                            result.Push(-oper1);
                        }
                        else
                        {
                            // (Error) The user has not input sufficient values in the expression.
                            throw new Exception("Evaluation error!");
                        }
                        break;
                    case TokenType.Sine:
                        // NOTE: n is 1 in this case
                        // If there are fewer than n values on the stack
                        if (result.Count >= 1)
                        {
                            // So, pop the top n values from the stack.
                            oper1 = (double)result.Pop();
                            // Evaluate the function, with the values as arguments.
                            // Push the returned results, if any, back onto the stack.
                            result.Push(Math.Sin(oper1));
                        }
                        else
                        {
                            // (Error) The user has not input sufficient values in the expression.
                            throw new Exception("Evaluation error!");
                        }
                        break;
                    case TokenType.Cosine:
                        // NOTE: n is 1 in this case
                        // If there are fewer than n values on the stack
                        if (result.Count >= 1)
                        {
                            // So, pop the top n values from the stack.
                            oper1 = (double)result.Pop();
                            // Evaluate the function, with the values as arguments.
                            // Push the returned results, if any, back onto the stack.
                            result.Push(Math.Cos(oper1));
                        }
                        else
                        {
                            // (Error) The user has not input sufficient values in the expression.
                            throw new Exception("Evaluation error!");
                        }
                        break;
                    case TokenType.Tangent:
                        // NOTE: n is 1 in this case
                        // If there are fewer than n values on the stack
                        if (result.Count >= 1)
                        {
                            // So, pop the top n values from the stack.
                            oper1 = (double)result.Pop();
                            // Evaluate the function, with the values as arguments.
                            // Push the returned results, if any, back onto the stack.
                            result.Push(Math.Tan(oper1));
                        }
                        else
                        {
                            // (Error) The user has not input sufficient values in the expression.
                            throw new Exception("Evaluation error!");
                        }
                        break;
                }
            }

            // If there is only one value in the stack
            if (result.Count == 1)
            {
                // That value is the result of the calculation.
                return (double)result.Pop();
            }
            else
            {
                // If there are more values in the stack
                // (Error) The user input too many values.
                throw new Exception("Evaluation error!");
            }
        }

        private bool IsOperatorToken(TokenType t)
        {
            bool result = false;
            switch (t)
            {
                case TokenType.Plus:
                case TokenType.Minus:
                case TokenType.Multiply:
                case TokenType.Divide:
                case TokenType.Exponent:
                case TokenType.UnaryMinus:
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        private bool IsFunctionToken(TokenType t)
        {
            bool result = false;
            switch (t)
            {
                case TokenType.Sine:
                case TokenType.Cosine:
                case TokenType.Tangent:
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        private double EvaluateConstant(string TokenValue)
        {
            double result = 0.0;
            switch (TokenValue)
            {
                case "pi":
                    result = Math.PI;
                    break;
                case "e":
                    result = Math.E;
                    break;
            }
            return result;
        }
    }
}
