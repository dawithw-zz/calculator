using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class OperatorNames
    {
        private const string add = "+";
        private const string subtract = "—";
        private const string multiply = "×";
        private const string divide = "/";
        private const string power = "^";
        private const string percent = "%";
        private const string minus = "-";
        private const string openParenthesis = "(";
        private const string closedParenthesis = ")";

        public static string Add { get { return add; } }
        public static string Subtract { get { return subtract; } }
        public static string Multiply { get { return multiply; } }
        public static string Divide { get { return divide; } }
        public static string Power { get { return power; } }
        public static string Percent { get { return percent; } }
        public static string Minus { get { return minus; } }
        public static string OpenParenthesis { get { return openParenthesis; } }
        public static string ClosedParenthesis { get { return closedParenthesis; } }

    }

    public static class OperatorPrecedence
    {
        private enum Precedence {ClosedParenthesis, Subtract, Add, Multiply, Divide, Power, Percent, Minus, OpenParenthesis}

        public static int NonOperator { get { return -1; } }

        public static int Add { get { return (int)Precedence.Add; } }
        public static int Subtract { get { return (int)Precedence.Subtract; } }
        public static int Multiply { get { return (int)Precedence.Multiply; } }
        public static int Divide { get { return (int)Precedence.Divide; } }
        public static int Power { get { return (int)Precedence.Power; } }
        public static int Percent { get { return (int)Precedence.Percent; } }
        public static int Minus { get { return (int)Precedence.Minus; } }
        public static int OpenParenthesis { get { return (int)Precedence.OpenParenthesis; } }
        public static int ClosedParenthesis { get { return (int)Precedence.ClosedParenthesis; } }
    }

    public class Operator
    {
        private string name;
        public string Name { get { return name; } }

        Operator() { }

        public Operator(string s)
        {
            name = s;
        }

        public bool IsUnary()
        {
            return (name.Equals(OperatorNames.Percent) || name.Equals(OperatorNames.Minus) || name.Equals(OperatorNames.OpenParenthesis));
        }

        public bool IsLeftAssociative()
        {
            return (name.Equals(OperatorNames.Power) || name.Equals(OperatorNames.Divide));
        }

        public static bool operator <(Operator lhs, Operator rhs)
        {
            return (Precedence(lhs) < Precedence(rhs));
        }

        public static bool operator >(Operator lhs, Operator rhs)
        {
            return (Precedence(lhs) > Precedence(rhs));
        }

        public static bool operator ==(Operator lhs, Operator rhs)
        {
            return (Precedence(lhs) == Precedence(rhs));
        }

        public static bool operator !=(Operator lhs, Operator rhs)
        {
            return (Precedence(rhs) != Precedence(rhs));
        }

        public static int Precedence(Operator op)
        {
            if (op.Name == OperatorNames.Add)
            {
                return OperatorPrecedence.Add;
            }
            else if (op.Name == OperatorNames.Subtract)
            {
                return OperatorPrecedence.Subtract;
            }
            else if (op.Name == OperatorNames.Multiply)
            {
                return OperatorPrecedence.Multiply;
            }
            else if (op.Name == OperatorNames.Divide)
            {
                return OperatorPrecedence.Divide;
            }
            else if (op.Name == OperatorNames.Power)
            {
                return OperatorPrecedence.Power;
            }
            else if (op.Name == OperatorNames.Percent)
            {
                return OperatorPrecedence.Percent;
            }
            else if (op.Name == OperatorNames.Minus)
            {
                return OperatorPrecedence.Minus;
            }
            else if (op.Name == OperatorNames.OpenParenthesis)
            {
                return OperatorPrecedence.OpenParenthesis;
            }
            else if (op.Name == OperatorNames.ClosedParenthesis)
            {
                return OperatorPrecedence.ClosedParenthesis;
            }
            else
            {
                return OperatorPrecedence.NonOperator;
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
