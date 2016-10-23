namespace Model.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
