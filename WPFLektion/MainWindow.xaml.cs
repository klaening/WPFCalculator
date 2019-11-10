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
        //När jag trycker på ett nummer så läggs det till i txt.Display
        //Om txt.Displays första siffra är 0 så tas nollan bort och ersätts med nästa siffra
        //Om txt.Displays första char är ',' så läggs en nolla till innan
        //Om man trycker backspace så tas sista char bort från txt.Display
        //Om man trycker CE så tas hela txt.Display bort och ersätts med en nolla
        //När man trycker på en operation så parseas txt.Display till en decimal som läggs till i en lista
        //result uppdateras med det nya numret och result skrivs ut på txt.Display
        //När man trycker på likamed så parseas txt.Display och resultatet skrivs ut




        //Vill jag parsea det som står i labelBox så fort jag trycker på en operation eller lika med
        //Varje knapptryckning lägger till i textbox och label.
        //Label skriver in vad som händer i textbox.
        //När man trycker på en operation eller likamed så parseas textbox till en decimal. Label går vidare
        //När en string parseas läggs den till i result beroende på vilken operation man har aktiv.


        List<decimal> Numbers = new List<decimal>();

        string activeNumber = "0";

        decimal result = 0;
        string operation = "";
        bool newNumber = true;
        bool buttonPressed = false;
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
            string output = labelCurrentOperation.Content.ToString();

            //Tar bort sista numret
            output = output.Remove(output.Length - txtDisplay.Text.Length);
            txtDisplay.Text = "0";            

            //Display
            labelCurrentOperation.Content = output;
        }

        public void CreateNumber()
        {
            decimal number = 0;
            Numbers.Add(number);
            newNumber = false;
        }

        public void ChangeNumber(decimal aNumber)
        {
            switch (operation)
            {
                case "+":
                    result += aNumber;
                    break;
                case "-":
                    result -= aNumber;
                    break;
                case "*":
                    result *= aNumber;
                    break;
                case "/":
                    result /= aNumber;
                    break;
            }
        }

        public void NumAppend(int btnNum)
        {
            if (newNumber)
            {
                CreateNumber();
            }

            if (activeNumber.First() == '0')
            {
                if (activeNumber.First() + 1 != ',')
                {
                    txtDisplay.Clear();
                    activeNumber = activeNumber.Remove(activeNumber.Length - 1);
                }
            }
            activeNumber += btnNum.ToString();
            txtDisplay.Text = activeNumber;
            
            if (txtDisplay.Text.First().ToString() != "0")
            {
                labelCurrentOperation.Content += txtDisplay.Text.Last().ToString();
            }
        }

        private void OperationSituation(string operand)
        {
            //Så fort jag trycker på en operation så parseas txt.display till en decimal och läggs till i en lista av nummer.

            //if newnumber är true så går den in och skapar ett nummer och sätter bool till false.

            CreateNumber();

            if (newNumber == true)
            {
                labelCurrentOperation.Content = labelCurrentOperation.Content + operand;
                ChangeNumber(Numbers.Last());
                txtDisplay.Text = result.ToString();
                operation = operand;
                newNumber = false;
            }
        }

        private void btnDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (!activeNumber.Contains(','))
            {
                labelCurrentOperation.Content += ",";
                txtDisplay.Text += ",";
            }
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            CreateNumber();

            ChangeNumber(Numbers.Last());

            txtDisplay.Text = result.ToString();
        }

        private void btnPositiveNegative_Click(object sender, RoutedEventArgs e)
        {
            if (labelCurrentOperation.Content != null)
            {
                //Gör om det som står till string så att jag kommer åt metoden Length
                string output = labelCurrentOperation.Content.ToString();
                string currentNumber = Numbers.Last().ToString();

                //Med metoden Length kan jag få reda på hur lång senaste numret är och hur många chars total som står på skärmen
                currentNumberCharacters = currentNumber.Length;
                charactersPressed = output.Length;

                //Med denna information kan vi ta bort det senaste numret som skrevs in
                output = output.Remove((charactersPressed - currentNumberCharacters), (currentNumberCharacters));

                //Och sen omvandla numret till negativt alternativt positivt
                Numbers[current] *= -1;

                //Display
                txtDisplay.Text = Numbers[current].ToString();
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
            buttonPressed = false;
            charactersPressed = 0;
            currentNumberCharacters = 0;
        }

        private void btnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            //Skapar en tillfällig string för att kunna ta bort sista karaktären i displayen
            string output = labelCurrentOperation.Content.ToString();
            charactersPressed = labelCurrentOperation.Content.ToString().Length;
            currentNumberCharacters = txtDisplay.Text.Length;

            if (txtDisplay.Text != "0")
            {
                txtDisplay.Text = txtDisplay.Text.Remove(currentNumberCharacters - 1);
                output = output.Remove(charactersPressed - 1);
                labelCurrentOperation.Content = output;
            }
            if (txtDisplay.Text == "")
            {
                txtDisplay.Text = "0";
            }
        }

        private void txtDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            OperationSituation("+");
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
