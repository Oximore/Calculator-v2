using static Calculator.Data.Models.IOperation;

namespace Calculator.Data.Models
{
    public class Operation : IOperation
    {
        public int Id { get; private set; }

        private List<double> operands = [];
        public List<double> Operands => operands;

        private List<Operator> @operator = [];
        public List<Operator> Operators => @operator;

        public bool Computable => (Operands.Count() == Operators.Count() + 1);
    }
}