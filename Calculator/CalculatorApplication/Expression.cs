using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Expression
    {
        private double solution;
        public double Solution {
            get { return solution; }
            set { solution = value; }
        }

        Expression left;
        public Expression Left
        {
            get { return left; }
            set { left = value; }
        }

        Expression right;
        public Expression Right
        {
            get { return right; }

            set { right = value; }
        }

        Operator op;
        public Operator Op
        {
            get { return op; }
            set { op = value; }
        }

        Expression() { }

        public Expression(double value)
        {
            solution = value;
            left = null;
            right = null;
            op = null;
        }

        public Expression(Expression lhs, Expression rhs, Operator opVal)
        {
            left = lhs;
            right = rhs;
            op = opVal;
        }

        // Check correctness
        override public string ToString()
        {
            string expression;

            if (Object.ReferenceEquals(op, null))
            {
                return solution.ToString();
            }

            expression = OperatorNames.OpenParenthesis + Left.ToString() + op.ToString();

            if (!op.IsUnary())
            {
                expression += Right.ToString();
            }
            return expression + OperatorNames.ClosedParenthesis;
        }
    }
}
