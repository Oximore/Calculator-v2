using Calculator.Data.Models;
using static Calculator.Data.Models.IOperation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calculator.UI.Servicies;
using Calculator.Data.Repositories;
using Calculator.Data.Repositories.SQL;
using Calculator.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Calculator.Data.Repositories.API;

namespace Calculator.UI.ViewModels
{
    public partial class OperationViewModel : ObservableObject
    {
        // Servicies

        private OperationService operationService = new();
        private static IOperationRepository operationRepository;

        // Observables

        [ObservableProperty]
        private string resultText = "";

        [ObservableProperty]
        private string operationText = "";

        public ObservableCollection<string> OperationHistory { get; set; } = new ObservableCollection<string>();

        // Others members
        private Operation operation = new();
        private bool newNumber = true;

        // Ctor
        public OperationViewModel()
        {
            //DbContextOptionsBuilder<DataContext> optionsBuilder = new();
            //string filename = "calculator_data.db3";
            //optionsBuilder.UseSqlite($"DataSource = {filename}");
            //DbContextOptions<DataContext> option = optionsBuilder.Options;
            //DataContext dataContext = new DataContext(option);
            //operationRepository = new SQLOperationRepository(dataContext);
            operationRepository = new APIOperationRepository();

            UpdateHistory();
        }

        // Relay Commands

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

                operationRepository.Create(operation);
                UpdateHistory();

                operation = new Operation();
                newNumber = true;
            }
        }

        [RelayCommand]
        private void ClearHistory()
        {
            foreach (Operation operation in operationRepository.ReadAll())
                operationRepository.Delete(operation.Id);
            UpdateHistory();
        }

        // Others Methods

        private void ValidateAndClearCurrentNumber(string remplace = "")
        {
            if (Double.TryParse(ResultText, out double current))
            {
                operation.Operands.Add(current);
                ResultText = remplace;
            }
        }

        private void UpdateHistory()
        {
            OperationHistory.Clear();
            foreach (Operation operation in operationRepository.ReadAll())
            {
                OperationHistory.Add($"{OperationService.ToString(operation)} = {OperationService.Compute(operation)}");
            }
        }

        private void UpdateOperationText()
        {
            OperationText = OperationService.ToString(operation);
            if (operation.Computable)
                OperationText += ResultText;
        }
    }
}
