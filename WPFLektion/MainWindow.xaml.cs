﻿using System;
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
        //Vill jag parsea det som står i labelBox så fort jag trycker på en operation eller lika med

        List<decimal> Numbers = new List<decimal>();
        decimal result = 0;
        string operation = "";
        bool newNumber = true;
        bool equalsPressed = false;
        //För att veta vilken plats i listan jag är på
        int current = 0;

        int charactersPressed;
        int currentNumberCharacters;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text != "0" && !equalsPressed)
            {
                string output = labelCurrentOperation.Content.ToString();
                charactersPressed = labelCurrentOperation.Content.ToString().Length;
                currentNumberCharacters = txtDisplay.Text.Length;

                output = output.Remove(charactersPressed - currentNumberCharacters);

                //Tar bort sista numret och skapar ett nytt vid tryckning av nummer

                Numbers.Remove(Numbers[current]);
                newNumber = true;

                //Display
                labelCurrentOperation.Content = output;
                txtDisplay.Text = "0";
            }        
        }

        public void CreateNumber()
        {
            if (newNumber)
            {
                decimal number = 0;
                Numbers.Add(number);
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
            if (!equalsPressed)
            {
                CreateNumber();

                decimal number = Numbers[current];

                number = number * 10 + btnNum;
                Numbers[current] = number;

                //Display
                txtDisplay.Text = number.ToString();
                labelCurrentOperation.Content = labelCurrentOperation.Content + btnNum.ToString();
            }
        }

        private void OperationSituation(string operand)
        {
            if (newNumber == false && !equalsPressed)
            {
                labelCurrentOperation.Content = labelCurrentOperation.Content + operand;
                ChangeNumber(Numbers.Last());
                txtDisplay.Text = result.ToString();
                operation = operand;
                newNumber = true;

                current++;
            }
        }

        private void btnDecimal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (!equalsPressed)
            {
                ChangeNumber(Numbers.Last());
                equalsPressed = true;
            }

            txtDisplay.Text = result.ToString();

            newNumber = true;
        }

        private void btnPositiveNegative_Click(object sender, RoutedEventArgs e)
        {
            if (labelCurrentOperation.Content != null && !equalsPressed)
            {
                //Gör om det som står till string så att jag kommer åt metoden Length
                string output = labelCurrentOperation.Content.ToString();
                string currentNumber = Numbers.Last().ToString();

                //Med metoden Length kan jag få reda på hur lång senaste numret är och hur många chars total som står på skärmen
                currentNumberCharacters = currentNumber.Length;
                charactersPressed = output.Length;

                //Med denna information kan vi ta bort det senaste numret som skrevs in
                output = output.Remove(charactersPressed - currentNumberCharacters);

                //Och sen omvandla numret till negativt alternativt positivt
                Numbers[current] *= -1;

                //Display
                txtDisplay.Text = Numbers.Last().ToString();
                labelCurrentOperation.Content = output + Numbers[current].ToString();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            result = 0;
            Numbers.Clear();
            operation = "";
            labelCurrentOperation.Content = "";
            txtDisplay.Text = "0";
            current = 0;
            newNumber = true;
            equalsPressed = false;
        }

        private void btnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            string currentNumber = Numbers.Last().ToString();

            if (currentNumber != "0" && !equalsPressed)
            {
                currentNumber = currentNumber.Remove(currentNumber.Length - 1);
                string output = labelCurrentOperation.Content.ToString();

                //Måste det här vara en if? Nu går den inte in hit pga förra ifen
                if (txtDisplay.Text == "")
                {
                    Numbers[current] = 0;
                    output = output.Remove(charactersPressed - 1);
                    labelCurrentOperation.Content = output;
                    txtDisplay.Text = Numbers[current].ToString();
                }
                else
                {
                    if (currentNumber == "")
                    {
                        currentNumber = "0";
                    }
                    Numbers[current] = decimal.Parse(currentNumber);

                    charactersPressed = output.Length;
                    output = output.Remove(charactersPressed - 1);
                    labelCurrentOperation.Content = output;
                    txtDisplay.Text = Numbers[current].ToString();
                }
            }
        }

        private void txtDisplay_TextChanged(object sender, TextChangedEventArgs e)
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
    }
}
