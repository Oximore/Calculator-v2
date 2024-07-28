using Calculator.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculator.Data.Repositories.SQL
{
    public class SQLOperationRepository : IOperationRepository
    {
        private readonly DataContext context;

        public SQLOperationRepository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Operation> ReadAll()
        {
            return context.Operations
                .AsNoTracking()
                .ToList();
        }

        public Operation Create(Operation model)
        {
            context.Operations.Add(model);
            context.SaveChanges(); // La requete SQL est executée ici
            return model;
        }

        public void Delete(int id)
        {
            var model = context.Operations.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return;
            context.Operations.Remove(model);
            context.SaveChanges();
        }
    }
}
