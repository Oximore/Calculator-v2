using static Calculator.Data.Models.IOperation;

namespace Calculator.Data.Models
{
    public class Operation : IOperation
    {
        public int Id { get; set; }

        public List<double> Operands { get; set; } = [];
        public List<Operator> Operators { get; set; } = [];

        public bool Computable => (Operands.Count == Operators.Count + 1);
    }
}