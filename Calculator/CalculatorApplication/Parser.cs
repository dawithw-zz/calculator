using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    // Not thread safe. Make dynamic for flexibility?
    class Parser
    {
        // Using Shunting-Yard Algorithm
        private static Stack<Operator> operators;
        private static Stack<Expression> expressions;

        private static string steps;
        public static string Steps
        {
            get { return steps; }
        }

        private enum Flags { ReadOperator, ReadNumber };

        public static string Parse(string equation)
        {
            steps = "Equation: " + equation;
            operators = new Stack<Operator>();
            expressions = new Stack<Expression>();

            int currIndex = 0;

            // while not at the end of equation
            while (currIndex < equation.Length)
            {
                // read value
                string item = readNext(equation, ref currIndex);        // currIndex is updated by the function

                if (IsOperator(item))
                {
                    // Adding operator to operators
                    AddOperator(item);
                }
                else
                {
                    // Adding value to expressions
                    try
                    {
                        expressions.Push(new Expression(Double.Parse(item)));
                    }
                    catch (FormatException)
                    {
                        throw new FormatException("Unknown operand: " + item);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Unable to process equation " + equation.Substring(currIndex));
                    }

                }
            }

            // while operators is not empty
            while (operators.Count != 0)
            {
                // remove top operator and create expression
                CreateExpression(operators.Pop());
            }

            //Expression formatted = expressions.Pop();
            return Solve(expressions.Pop()).ToString();
        }

        // This operation will change the value of currIndex supplied by the caller
        private static string readNext(string equation, ref int currIndex)
        {
            string unit = equation[currIndex].ToString();       // first "char" of Item
            ++currIndex;

            // read operator -------
            if (IsOperator(unit))
            {
                return unit;
            }
            // NOTE: Need to change above code if using operators spanning multiple characters

            // read operand -----------
            string operand = unit;

            while (currIndex < equation.Length)
            {
                unit = equation[currIndex].ToString();
                if (IsOperator(unit))
                {
                    break;      // end of operand
                }
                operand += unit;
                ++currIndex;
            }
            return operand;
        }

        private static void AddOperator(string opName)
        {
            Operator op = new Operator(opName);

            while (operators.Count > 0 &&  // there are operators in the stack  AND
                            (op < operators.First() ||      // there is an operator with higher precedence  OR
                                (op == operators.First() && op.IsLeftAssociative()) ))  // there are multiple left associative operators of the same kind */
            {
                Operator nextOp = operators.Peek();
                
                // If next operator is OpenParenthesis and current operator is not ClosedParenthesis
                if (nextOp.Name == OperatorNames.OpenParenthesis && op.Name != OperatorNames.ClosedParenthesis) 
                {
                    break;
                }
                
                // remove operator and create expression
                CreateExpression(operators.Pop());

                // If last operator was OpenParenthesis
                if (nextOp.Name == OperatorNames.OpenParenthesis)
                {
                    break;
                }
            }

            if (op.Name != OperatorNames.ClosedParenthesis)
            {
                operators.Push(op);
            }
        }

        private static void CreateExpression(Operator op)
        {
            Expression lhs;
            Expression rhs = expressions.Pop();

            if (op.IsUnary())
            {
                lhs = rhs;
                rhs = null;
            }
            else
            {
                lhs = expressions.Pop();
            }
            expressions.Push(new Expression(lhs, rhs, op));
        }

        public static double Solve(Expression ex)
        {
            // Single expressions
            if (Object.ReferenceEquals(ex.Op, null) || ex.Op.Name == OperatorNames.OpenParenthesis)
            {
                // simple number
                if (Object.ReferenceEquals(ex.Left, null))
                    return ex.Solution;

                // single expression
                return Solve(ex.Left);
            }
            // Unary expressions
            else if (ex.Op.IsUnary())
            {
                // Solve left operand and compute expression
                return ComputeUnary(Solve(ex.Left), ex.Op);
            }
            // Binary expressions
            else
            {
                // Solve both operands and compute expression
                return ComputeBinary(Solve(ex.Left), Solve(ex.Right), ex.Op);
            }
        }

        public static double ComputeBinary(double lhs, double rhs, Operator op)
        {
            double solution = 0;
            
            if (op.Name == OperatorNames.Add)
            {
                solution =  Calculator.Add(lhs, rhs);
            }
            else if (op.Name == OperatorNames.Subtract)
            {
                solution =  Calculator.Subtract(lhs, rhs);
            }
            else if (op.Name == OperatorNames.Multiply)
            {
                solution = Calculator.Multiply(lhs, rhs);
            }
            else if (op.Name == OperatorNames.Divide)
            {
                solution = Calculator.Divide(lhs, rhs);
            }
            else if (op.Name == OperatorNames.Power)
            {
                solution = Calculator.Power(lhs, rhs);
            }
            else
            {
                throw new InvalidOperationException("Unknown operation: " + op.Name);
            }
            return solution;
        }

        public static double ComputeUnary(double val, Operator op)
        {
            double solution = 0;
            if (op.Name == OperatorNames.Percent)
            {
                solution =  Calculator.Percent(val);         
            }
            else if (op.Name == OperatorNames.Minus)
            {
                solution = Calculator.Minus(val);
            }
            else
            {
                throw new InvalidOperationException("Unknown operation: " + op.Name);
            }
            return solution;
        }

        public static bool IsOperator(string val)
        {
            return (Operator.Precedence(new Operator(val)) != OperatorPrecedence.NonOperator);
        }
    }
}
