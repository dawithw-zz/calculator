using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Calculator
    {

        public static double Add(double lhs, double rhs)
        {
            return lhs + rhs;
        }

        public static double Subtract(double lhs, double rhs)
        {
            return lhs - rhs;
        }

        public static double Multiply(double lhs, double rhs)
        {
            return lhs * rhs;
        }

        public static double Divide(double lhs, double rhs)
        {
            return lhs / rhs;
        }

        public static double Power(double lhs, double rhs)
        {
            return Math.Pow(lhs, rhs);
        }

        public static double Percent(double lhs)
        {
            return lhs / 100;
        }

        public static double Minus(double lhs)
        {
            return -1 * lhs;
        }
    }
}
