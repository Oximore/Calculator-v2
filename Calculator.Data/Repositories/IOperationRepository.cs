using Calculator.Data.Models;

namespace Calculator.Data.Repositories
{
    public interface IOperationRepository
    {
        IEnumerable<Operation> ReadAll();
        Operation Create(Operation model);
        void Delete(int id);
    }
}