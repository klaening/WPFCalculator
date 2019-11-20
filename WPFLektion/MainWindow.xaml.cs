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
using System.Media;
using System.IO;

namespace WPFLektion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //label uppdateras beroende på vad som står i txt.Display och hur många karaktärer som finns
        //När jag trycker på ett nummer så läggs det till i txt.Display
        //Om txt.Displays första siffra är 0 så tas nollan bort och ersätts med nästa siffra
        //Om txt.Displays första char är ',' så läggs en nolla till innan
        //Om man trycker backspace så tas sista char bort från txt.Display
        //Om man trycker CE så tas hela txt.Display bort och ersätts med en nolla
        //När man trycker på en operation så parseas txt.Display till en decimal som läggs till i en lista
        //result uppdateras med det nya numret och result skrivs ut på txt.Display
        //När man trycker på likamed så parseas txt.Display och resultatet skrivs ut
        string operation = "";
        string precedingOperation = "";
        decimal result = 0;
        decimal currentNumber;
        decimal lastNumber;
        bool buttonPressed = false;

        string Add = "Add";
        string Remove = "Remove";

        //Vill jag parsea det som står i labelBox så fort jag trycker på en operation eller lika med
        //Varje knapptryckning lägger till i textbox och label.
        //Label skriver in vad som händer i textbox.
        //När man trycker på en operation eller likamed så parseas textbox till en decimal. Label går vidare
        //När en string parseas läggs den till i result beroende på vilken operation man har aktiv.

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            if (!buttonPressed && txtDisplay.Text != "0")
            {
                ChangeLabelContent(Remove, txtDisplay.Text);               
                txtDisplay.Text = "0";
            }
        }

        public void ChangeLabelContent(string addOrRemove, string input)
        {
            switch (addOrRemove)
            {
                case "Add":
                    labelCurrentOperation.Text += input;
                    break;
                case "Remove":
                    string content = labelCurrentOperation.Text;
                    int contentLength = labelCurrentOperation.Text.Length;
                    labelCurrentOperation.Text = content.Remove(contentLength - input.Length);
                    break;
            }
        }

        public void MakeCalculation()
        {
            decimal number = decimal.Parse(txtDisplay.Text);

            if (precedingOperation != "")
            {
                number = MakePrecedingCalculation(number);
            }

            //if (alternativeOperation == "^")
            //{
            //    number = MakePowCalculation(number);
            //}
            //else if (alternativeOperation == "√")
            //{
            //    number = (decimal)MakeSqrtCalculation(number);
            //}
            //else if (alternativeOperation == "%")
            //{
            //    number = MakePercentCalculation(number);
            //}

            switch (operation)
            {
                case "+":
                    result += number;
                    break;
                case "-":
                    result -= number;
                    break;
                case "*":
                    result *= number;
                    break;
                case "/":
                    result /= number;
                    break;

                default:
                    result += number;
                    break;
            }
            lastNumber = number;
        }

        public void NumAppend(int btnNum)
        {
            //När jag trycker på ett nummer så läggs det till i txt.Display
            //Om txt.Displays första siffra är 0 så tas nollan bort och ersätts med nästa siffra
            if (buttonPressed)
            {
                if (txtDisplay.Text != "-")
                {
                    txtDisplay.Clear();
                    buttonPressed = false;
                }
            }

            txtDisplay.Text += btnNum;

            if (txtDisplay.Text.Length >= 2)
            {
                if (txtDisplay.Text.First() == '0' && txtDisplay.Text.Substring(1, 1) != ",")
                {
                    txtDisplay.Text = btnNum.ToString();
                }
            }

            ChangeLabelContent(Add, btnNum.ToString());
        }

        private void Operation(string aOperation)
        {
            //Så fort jag trycker på en operation så parseas txt.display till en decimal och uppdaterar result

            if (!buttonPressed)
            {
                if (aOperation == "^" || aOperation == "%")
                {
                    currentNumber = decimal.Parse(txtDisplay.Text);
                    precedingOperation = aOperation;
                    ChangeLabelContent(Add, aOperation);
                }
                else if (aOperation == "√")
                {
                    precedingOperation = aOperation;
                    
                    if (!string.IsNullOrWhiteSpace(labelCurrentOperation.Text))
                    {
                        string content = labelCurrentOperation.Text;

                        labelCurrentOperation.Text = content.Remove(content.Length - txtDisplay.Text.Length);
                        ChangeLabelContent(Add, "√" + txtDisplay.Text);
                    }
                    else
                    {
                        ChangeLabelContent(Add, "√");
                    }
                }
                else
                {
                    MakeCalculation();
                    operation = aOperation;
                    ChangeLabelContent(Add, operation);
                    txtDisplay.Text = result.ToString();
                }
            }
            buttonPressed = true;
        }

        private decimal MakePrecedingCalculation(decimal aNumber)
        {
            decimal currentResult;
            switch (precedingOperation)
            {
                case "^":
                    currentResult = 1;
                    for (int i = 0; i < aNumber; i++)
                    {
                        currentResult *= currentNumber;
                    }
                    aNumber = currentResult;
                    break;

                case "√":
                    double num = double.Parse(txtDisplay.Text);
                    aNumber = (decimal)Math.Sqrt(num);
                    break;

                case "%":
                    aNumber = aNumber * lastNumber / 100;
                    break;
            }
            precedingOperation = "";
            return aNumber;
        }

        private void btnDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (!txtDisplay.Text.Contains(',') && !buttonPressed)
            {
                if (txtDisplay.Text == "0")
                {
                    txtDisplay.Text += ",";
                    ChangeLabelContent(Add, txtDisplay.Text);
                }
                else
                {
                    txtDisplay.Text += ",";
                    ChangeLabelContent(Add, ",");
                }
            }
            else if(buttonPressed)
            {
                txtDisplay.Clear();
                txtDisplay.Text = "0,";
                ChangeLabelContent(Add, txtDisplay.Text);
                buttonPressed = false;
            }
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            MakeCalculation();
            txtDisplay.Text = result.ToString();
            labelCurrentOperation.Text = "";
            operation = "";
            buttonPressed = true;
            result = 0;
        }

        private void btnPositiveNegative_Click(object sender, RoutedEventArgs e)
        {
            if (buttonPressed || txtDisplay.Text == "0")
            {
                txtDisplay.Clear();
                txtDisplay.Text = "-";
                ChangeLabelContent(Add, txtDisplay.Text);
                buttonPressed = false;
            }
            //Skriver bara ut om buttonPressed == false.
            else if (!buttonPressed)
            {
                //Om txt innehåller - så skrivs ett - ut.
                if (!txtDisplay.Text.Contains("-"))
                {
                    ChangeLabelContent(Remove, txtDisplay.Text);
                    string saveString = txtDisplay.Text;
                    txtDisplay.Clear();
                    txtDisplay.Text = "-" + saveString;
                    ChangeLabelContent(Add, txtDisplay.Text);
                }
                //Annars tas det bort
                else
                {
                    ChangeLabelContent(Remove, txtDisplay.Text);
                    txtDisplay.Text = txtDisplay.Text.Remove(0, 1);
                    ChangeLabelContent(Add, txtDisplay.Text);
                    if (txtDisplay.Text == "")
                    {
                        txtDisplay.Text = "0";
                    }
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Clear();
            txtDisplay.Text = "0";
            result = 0;
            labelCurrentOperation.Text = "";
            operation = "";
            precedingOperation = "";
            buttonPressed = false;
        }

        private void btnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            int length = txtDisplay.Text.Length;
            if (txtDisplay.Text != "0" && !buttonPressed)
            {
                //Uppdaterar labelCurrentOperation.Content
                ChangeLabelContent(Remove, "1");
                txtDisplay.Text = txtDisplay.Text.Remove(length - 1);

                if (txtDisplay.Text == "")
                {
                    txtDisplay.Text = "0";
                }
            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            Operation("+");
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            Operation("-");
        }

        private void btnTimes_Click(object sender, RoutedEventArgs e)
        {
            Operation("*");
        }

        private void btnDivide_Click(object sender, RoutedEventArgs e)
        {
            Operation("/");
        }

        private void btnPow_Click(object sender, RoutedEventArgs e)
        {
            if (precedingOperation == "")
            {
                Operation("^");
            }
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
            if (precedingOperation == "")
            {
                Operation("√");
            }
        }

        private void btnPercent_Click(object sender, RoutedEventArgs e)
        {
            if (precedingOperation == "")
            {
                Operation("%");
            }
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

        private void labelCurrentOperation_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void txtDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnHonk_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream stream = File.Open(@"ahooga.wav", FileMode.Open))
            {
                SoundPlayer myNewSound = new SoundPlayer(stream);
                myNewSound.Load();
                myNewSound.Play();
            }
        }

        //private decimal MakePowCalculation(decimal aNumber)
        //{
        //    decimal currentResult = 1;
        //    for (int i = 0; i < aNumber; i++)
        //    {
        //        currentResult *= currentNumber;
        //    }

        //    alternativeOperation = "";
        //    return currentResult;
        //}

        //private decimal MakeSqrtCalculation(decimal number)
        //{
        //    double num = (double)currentNumber;
        //    double currentResult = Math.Sqrt(num);

        //    alternativeOperation = "";
        //    return (decimal)currentResult;
        //}

        //private decimal MakePercentCalculation(decimal number)
        //{
        //    return number * lastNumber / 100;
        //}

    }
}
