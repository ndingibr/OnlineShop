using System;

namespace Model.Infrastructure
{
  public interface IDatabaseFactory : IDisposable
  {
    OnlineShopContext Get();
  }
}
