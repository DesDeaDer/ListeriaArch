using System;
using System.Collections.Generic;

namespace ListeriaArch {
  public class Context : IContext {
    public ILinksCreator LinksCreator { get; init; }
    public ILinksProvider LinksProvider { get; init; }

    public void Add<T>(T obj) {
      throw new NotImplementedException();
    }

    public void Add(ILinksProvider obj) {
      throw new NotImplementedException();
    }

    public T Get<T>() {
      throw new NotImplementedException();
    }

    public void Processing<T>(Func<T, Action> getProcessing) { //TODO: use code Tree for parce static version of method
      var objs = default(IEnumerable<T>); //TODO

      foreach (var obj in objs) {
        getProcessing(obj)();
      }
    }

    public void Release() {
      throw new NotImplementedException();
    }
  }
}
