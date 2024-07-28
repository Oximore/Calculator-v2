namespace Calculator.Data.Models
{


    public interface IOperation
    {
        public int Id { get; }
        public enum Operator { Sum, Substaction, Multiplication, Division }
        public List<double> Operands { get; }
        public List<Operator> Operators { get; }
        public bool Computable { get;  }
    }
}
