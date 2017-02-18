using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calculator;

namespace CalculatorApplication
{
    public partial class MainWindow : Window
    {
        // Equation Format Tool
        private bool canUseOperator = false;
        private bool showMore = false;

        private int parenthesisBalance = 0;
        private string answer = String.Empty;

        private readonly double originalWindowWidth;

        public MainWindow()
        {

            InitializeComponent();
            originalWindowWidth = mainWindow.Width;
        }

        private void one_Click(object sender, RoutedEventArgs e)
        {
            equationBox.Text += "1";
            canUseOperator = true;
        }

        private void two_Click(object sender, RoutedEventArgs e)
        {
            equationBox.Text += "2";
            canUseOperator = true;
        }

        private void three_Click(object sender, RoutedEventArgs e)
        {
            equationBox.Text += "3";
            canUseOperator = true;
        }

        private void four_Click(object sender, RoutedEventArgs e)
        {
            equationBox.Text += "4";
            canUseOperator = true;
        }

        private void five_Click(object sender, RoutedEventArgs e)
        {
            equationBox.Text += "5";
            canUseOperator = true;
        }

        private void six_Click(object sender, RoutedEventArgs e)
        {
            equationBox.Text += "6";
            canUseOperator = true;
        }

        private void seven_Click(object sender, RoutedEventArgs e)
        {   
            equationBox.Text += "7";
            canUseOperator = true;
        }

        private void eight_Click(object sender, RoutedEventArgs e)
        {
            equationBox.Text += "8";
            canUseOperator = true;
        }

        private void nine_Click(object sender, RoutedEventArgs e)
        {
            equationBox.Text += "9";
            canUseOperator = true;
        }

        private void zero_Click(object sender, RoutedEventArgs e)
        {
            equationBox.Text += "0";
            canUseOperator = true;
        }

        private void decimal_Click(object sender, RoutedEventArgs e)
        {
            // auto decimal
            if (!char.IsDigit(equationBox.Text.Last()))
                equationBox.Text += "0";

            equationBox.Text += ".";
            canUseOperator = false;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (equationBox.Text != String.Empty)
                equationBox.Text = equationBox.Text.Substring(0, equationBox.Text.Length - 1);
        }

        private void divide_Click(object sender, RoutedEventArgs e)
        {
            if (canUseOperator)
                equationBox.Text += OperatorNames.Divide;
        }

        private void multiply_Click(object sender, RoutedEventArgs e)
        {
            if (canUseOperator)
            {
                equationBox.Text += OperatorNames.Multiply;
                canUseOperator = false;
            }
                
        }

        private void power_Click(object sender, RoutedEventArgs e)
        {
            if (canUseOperator)
            {
                equationBox.Text += OperatorNames.Power;
                canUseOperator = false;
            }
        }

        private void subtract_Click(object sender, RoutedEventArgs e)
        {
            if (canUseOperator)
            {
                equationBox.Text += OperatorNames.Subtract;
                canUseOperator = false;
            }
            else
            {
                equationBox.Text += OperatorNames.Minus;
            }
        }

        private void percent_Click(object sender, RoutedEventArgs e)
        {
            if (canUseOperator)
                equationBox.Text += OperatorNames.Percent;
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            equationBox.Text = String.Empty;
            solutionBox.Text = String.Empty;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (canUseOperator)
            {
                equationBox.Text += OperatorNames.Add;
                canUseOperator = false;
            }  
        }

        private void openParenthesis_Click(object sender, RoutedEventArgs e)
        {
        
            if (canUseOperator)
            {
                // auto multiply
                equationBox.Text += OperatorNames.Multiply;
                canUseOperator = false;
            }
            if (equationBox.Text == String.Empty || !equationBox.Text.Last().Equals('.'))   // check for bad decimal
            {
                equationBox.Text += OperatorNames.OpenParenthesis;
                ++parenthesisBalance;
            }
            
        }

        private void closeParenthesis_Click(object sender, RoutedEventArgs e)
        {
            if (parenthesisBalance > 0 && canUseOperator)
            {
                equationBox.Text += OperatorNames.ClosedParenthesis;
                --parenthesisBalance;
            }
        }

        private void equals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!canUseOperator || parenthesisBalance != 0)
                {
                    throw new FormatException("Unbalanced Equation");
                }
                solutionBox.Text = Parser.Parse(equationBox.Text);
                if (parenthesisBalance > 0)
                    throw new FormatException("Bad equation");
            }
            catch (FormatException ex)
            {
                solutionBox.Text = ex.Message;
            }
            catch (Exception)
            {
                solutionBox.Text = "Internal error";
            } 
            finally
            {

            }
        }

        private void more_Click(object sender, RoutedEventArgs e)
        {
            if (showMore)
            {
                mainWindow.Width = originalWindowWidth;
                showMore = false;
            }
            else
            {
                mainWindow.Width = mainWindow.MaxWidth;
                showMore = true;
            }
        }
    }
}
