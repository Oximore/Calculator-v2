using Calculator.Data.Models;
using static Calculator.Data.Models.IOperation;
using System.Windows.Controls;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calculator.UI.Servicies;

namespace Calculator.UI.ViewModels
{

    //[ObservableObject]
    public partial class OperationViewModel : ObservableObject
    {

        // Servicies
        private OperationService operationService = new();
        private Operation operation = new();
        private bool newNumber = true;

        [ObservableProperty]
        private string resultText = "";

        [ObservableProperty]
        private string operationText = "";

        //[ObservableProperty]
        //private string currentText = "";



        //private string operand1 = "", operand2 = "";
        //private string @operator = ""; // @ car operator mot-clef c#



        [RelayCommand]
        private void AddDigit(string digit)
        {
            if (newNumber)
            {
                ResultText = "";
                newNumber = false;
            }
            string tmpCurrentText = ResultText + digit;
            if (Double.TryParse(tmpCurrentText, out double current))
            {
                ResultText = tmpCurrentText;
            }
            else
            {
                ResultText = digit;
            }
            UpdateOperationText();
        }

        private void ValidateAndClearCurrentNumber(string remplace = "") {
            if (Double.TryParse(ResultText, out double current))
            {
                operation.Operands.Add(current);
                ResultText = remplace;
            }
        }



        [RelayCommand]
        private void AddOperator(string @operator)
        {
            Operator op = Operator.Sum;
            switch (@operator)
            {
                case "+": op = Operator.Sum; break;
                case "−": op = Operator.Substaction; break;
                case "×": op = Operator.Multiplication; break;
                case "÷": op = Operator.Division; break;
                default: return;
            }

            ValidateAndClearCurrentNumber(@operator);
            operation.Operators.Add(op);
            UpdateOperationText();
            newNumber = true;
        }

        [RelayCommand]
        private void Compute(string digit)
        {
            ValidateAndClearCurrentNumber();
            UpdateOperationText();
            if (operation.Computable)
            {
                ResultText = $"{OperationService.Compute(operation)}";
                OperationText += " = ";
                operation = new Operation();
                newNumber = true;
            }
        }


        private void UpdateOperationText()
        {
            //string text = OperationService.ToString(operation);
            //OperationText = (text.Any() ? text : ResultText);
            OperationText = OperationService.ToString(operation);
            if (operation.Computable)
                OperationText += ResultText;
        }


        //private void DigitButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (sender is not Button button)
        //        return;

        //    if (button.Content is not string digit)
        //        return;

        //    if (!string.IsNullOrEmpty(@operator) &&
        //        string.IsNullOrEmpty(operand2))
        //        resultText.Text = "";

        //    if (string.IsNullOrEmpty(@operator))
        //        operand1 += digit;
        //    else
        //        operand2 += digit;

        //    resultText.Text += digit;
        //}

        //private void OperatorButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (sender is not Button button)
        //        return;

        //    if (button.Content is not string @operator)
        //        return;

        //    this.@operator = @operator;
        //}

        //private void EqualButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!double.TryParse(operand1, out double value1))
        //        return;

        //    if (!double.TryParse(operand2, out double value2))
        //        return;

        //    double result;

        //    switch (@operator)
        //    {
        //        case "+": result = value1 + value2; break;
        //        case "−": result = value1 - value2; break;
        //        case "×": result = value1 * value2; break;
        //        case "÷": result = value1 / value2; break;
        //        default: return;
        //    }

        //    resultText.Text = result.ToString("0.#########");

        //    // Reset
        //    operand1 = operand2 = @operator = "";
        //}
    }
}
