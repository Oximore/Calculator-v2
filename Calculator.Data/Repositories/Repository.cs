namespace Calculator.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> ReadAll();

        public T? ReadOne(int id);

        public T Update(T model);

        public T Create(T model);

        public void Delete(int id);
    }
}