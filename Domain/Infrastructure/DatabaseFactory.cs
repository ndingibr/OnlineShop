
namespace Model.Infrastructure
{
public class DatabaseFactory : Disposable, IDatabaseFactory
{
    private OnlineShopContext dataContext;
    public OnlineShopContext Get()
    {
        return dataContext ?? (dataContext = new OnlineShopContext());
    }
    protected override void DisposeCore()
    {
        if (dataContext != null)
            dataContext.Dispose();
    }
}
}
