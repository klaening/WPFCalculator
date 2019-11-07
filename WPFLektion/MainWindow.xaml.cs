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

namespace WPFLektion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<decimal> Numbers = new List<decimal>();
        decimal result = 0;
        string operation = "";
        bool newNumber = true;
        bool buttonPressed = false;
        //För att veta vilken plats i listan jag är på
        int ctr = 0;

        int charactersPressed = 0;
        int currentNumberCharacters = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            string output = labelCurrentOperation.Content.ToString();

            output = output.Remove((charactersPressed - currentNumberCharacters), currentNumberCharacters);
            charactersPressed -= currentNumberCharacters;
            currentNumberCharacters = 0;

            labelCurrentOperation.Content = output;

            //ctr--;
            Numbers.Remove(Numbers[ctr]);

            txtDisplay.Text = "0";
            newNumber = true;
        }

        public void CreateNumber()
        {
            if (newNumber)
            {
                decimal number = 0;
                Numbers.Add(number);
                //ctr++;
                newNumber = false;
            }
        }

        public void ChangeNumber(decimal aNumber)
        {
            switch (operation)
            {
                case "+":
                    result = result + aNumber;
                    break;
                case "-":
                    result -= aNumber;
                    break;
                case "*":
                    result = result * aNumber;
                    break;
                case "/":
                    result /= aNumber;
                    break;
                default:
                    result = result + aNumber;
                    break;
            }
        }

        public void NumAppend(int btnNum)
        {
            //if (buttonPressed)
            //{
                
            //}

            charactersPressed++;
            currentNumberCharacters++;

            CreateNumber();


            decimal number = Numbers[ctr];

            number = number * 10 + btnNum;
            Numbers[ctr] = number;

            //Display
            txtDisplay.Text = number.ToString();
            labelCurrentOperation.Content = labelCurrentOperation.Content + btnNum.ToString();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            NumAppend(1);
        }
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            NumAppend(2);
        }
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            NumAppend(3);
        }
        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            NumAppend(4);
        }
        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            NumAppend(5);
        }
        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            NumAppend(6);
        }
        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            NumAppend(7);
        }
        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            NumAppend(8);
        }
        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            NumAppend(9);
        }
        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            NumAppend(0);
        }


        private void OperationSituation(string operand)
        {
            if (newNumber == false)
            {
                labelCurrentOperation.Content = labelCurrentOperation.Content + operand;
                ChangeNumber(Numbers.Last());
                txtDisplay.Text = result.ToString();
                operation = operand;
                newNumber = true;

                ctr++;
                charactersPressed++;
                currentNumberCharacters = 0;
            }
        }

        private void btnDecimal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            OperationSituation("+");

            //operation = "+";
            //labelCurrentOperation.Content = labelCurrentOperation.Content + operation;
            //ChangeNumber(Numbers[ctr - 1]);
            //txtDisplay.Text = result.ToString();
            //initButtonPress = true;
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            OperationSituation("-");
        }

        private void btnTimes_Click(object sender, RoutedEventArgs e)
        {
            OperationSituation("*");
        }

        private void btnDivide_Click(object sender, RoutedEventArgs e)
        {
            OperationSituation("/");
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (!buttonPressed)
            {
                ChangeNumber(Numbers.Last());
                buttonPressed = true;
            }

            txtDisplay.Text = result.ToString();

            newNumber = true;
        }

        private void btnPositiveNegative_Click(object sender, RoutedEventArgs e)
        {
            if (newNumber == false)
            {
                string output = labelCurrentOperation.Content.ToString();

                if (Numbers[ctr] < 0)
                {
                    currentNumberCharacters++;
                    output = output.Remove((charactersPressed - currentNumberCharacters), (currentNumberCharacters));
                }
                else
                {

                }

                output = output.Remove((charactersPressed - currentNumberCharacters), currentNumberCharacters);

                if (output.Last() == '-')
                {
                    output.Remove((output.Last() - 1), 1);
                    currentNumberCharacters--;
                    charactersPressed--;
                }

                Numbers[ctr] *= -1;

                if (Numbers[ctr] < 0)
                {
                    currentNumberCharacters++;
                    charactersPressed++;
                }

                txtDisplay.Text = Numbers[ctr].ToString();

                labelCurrentOperation.Content = output + Numbers[ctr].ToString();
            }
        }



        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            result = 0;
            Numbers.Clear();
            operation = "";
            labelCurrentOperation.Content = "";
            txtDisplay.Text = "0";
            ctr = 0;
            newNumber = true;
            buttonPressed = false;
        }

        private void btnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            List<char> IndivNumbers = new List<char>();
            if (operation == "")
            {
                //IndivNumbers = number1.ToString().ToList();
                //IndivNumbers.Remove(IndivNumbers[IndivNumbers.Count-1]);
                //number1 = int.Parse(IndivNumbers.ToString());
            }
            else
            {
                //IndivNumbers = number2.ToString().ToList();
                //IndivNumbers.Remove(IndivNumbers[IndivNumbers.Count-1]);
                //number2 = int.Parse(IndivNumbers.ToString());
            }
        }

        private void txtDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
