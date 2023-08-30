using System;
using System.Collections.Generic;

namespace ListeriaArch {
  public interface ILinksCreator {
    object Create(Type type);
  }

  public interface ILinks<T> : ILinks { 
    new IEnumerable<T> Links { get; }
  }

  public interface ILinks {
    IEnumerable<object> Links { get; }
  }

  public interface ILinksProvider {
    ILinks<T> Get<T>();
  }
}
