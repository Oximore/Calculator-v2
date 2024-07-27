using Calculator.Data.Models;
using static Calculator.Data.Models.IOperation;

namespace Calculator.UI.Servicies
{
    public class OperationService
    {
        public static double Compute(Operation operation)
        {
            if (!operation.Computable)
                throw new ArgumentException();

            double result = operation.Operands.First();

            IEnumerator<double> operandsIt = operation.Operands.GetEnumerator();
            operandsIt.MoveNext();
            IEnumerator<Operator> operatorsIt = operation.Operators.GetEnumerator();
            while (operandsIt.MoveNext() && operatorsIt.MoveNext())
            {
                switch (operatorsIt.Current)
                {
                    case Operator.Sum: result += operandsIt.Current; break;
                    case Operator.Substaction: result -= operandsIt.Current; break;
                    case Operator.Multiplication: result *= operandsIt.Current; break;
                    case Operator.Division: result /= operandsIt.Current; break;
                }
            }
            return result;
        }

        public static Operation AddOperator(Operation operation, Operator @operator)
        {
            operation.Operators.Add(@operator);
            operation.Operands.Add(0);
            return operation;
        }

        protected static readonly Dictionary<Operator, string> operatorToStringDictionary = new()
        {
            { Operator.Sum ,"+" },
            { Operator.Substaction ,"−" },
            { Operator.Multiplication ,"×" },
            { Operator.Division,"÷" },
        };

        public static string ToString(Operator @operator)
        {
            return operatorToStringDictionary[@operator];
        }

        public static string ToString(Operation operation)
        {
            string result = String.Empty;

            if (!operation.Operands.Any())
                return "";
            if (!operation.Operators.Any())
                return $"{operation.Operands.First()}";

            IEnumerator<double> operandsIt = operation.Operands.GetEnumerator();
            IEnumerator<Operator> operatorsIt = operation.Operators.GetEnumerator();
            operandsIt.MoveNext();
            operatorsIt.MoveNext();
            while (true)
            {
                result += $" {operandsIt.Current} {ToString(operatorsIt.Current)}";

                if (!operandsIt.MoveNext())
                    break;
                else if (!operatorsIt.MoveNext())
                {
                    result += $" {operandsIt.Current}";
                    break;
                }
            }

            return result;
        }
    }
}