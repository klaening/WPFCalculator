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
        decimal result = 0;
        bool buttonPressed = true;

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
                    labelCurrentOperation.Content += input;
                    break;
                case "Remove":
                    string content = labelCurrentOperation.Content.ToString();
                    int contentLength = content.Length;
                    content = content.Remove(contentLength - input.Length);
                    labelCurrentOperation.Content = content;
                    break;
            }
        }

        public void MakeCalculation()
        {
            decimal number = decimal.Parse(txtDisplay.Text);

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
                    if (!buttonPressed)
                    {
                        result += number;
                    }
                    break;
            }
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
                }
                buttonPressed = false;
            }


            if (labelCurrentOperation.Content != null)
            {
                string content = labelCurrentOperation.Content.ToString();

                if (btnNum == 0 && content.Length > 2 && content.Substring(0, 2) != "0,")
                {
                    labelCurrentOperation.Content = "0";
                }
                else
                {
                    txtDisplay.Text += btnNum;
                }
            }

            if (txtDisplay.Text.Length >= 2)
            {
                char[] charArray = txtDisplay.Text.ToCharArray();

                if (txtDisplay.Text.First() == '0' && charArray[1] != ',')
                {
                    txtDisplay.Text = btnNum.ToString();
                }
            }

            ChangeLabelContent(Add, btnNum.ToString());
        }

        private void Operation(string aOperation)
        {
            //Så fort jag trycker på en operation så parseas txt.display till en decimal och uppdaterar result
            MakeCalculation();
            operation = aOperation;
            txtDisplay.Text = result.ToString();
            if (!buttonPressed)
            {
                ChangeLabelContent(Add, operation);
            }
            buttonPressed = true;
        }

        private void btnDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (!txtDisplay.Text.Contains(',') && !buttonPressed)
            {
                if (txtDisplay.Text == "0")
                {
                    txtDisplay.Clear();
                    txtDisplay.Text = "0,";
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
            labelCurrentOperation.Content = "";
            operation = "";
            buttonPressed = true;
            result = 0;
        }

        private void btnPositiveNegative_Click(object sender, RoutedEventArgs e)
        {
            //Kan man simplifiera det här med att sätta buttonPressed till true som default?


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
            labelCurrentOperation.Content = "";
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

        private void txtDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {

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
