using System;

namespace ListeriaArch {
  public interface IContext : IRelease {
    void ResolveAll(); //TODO temp
    void Processing<T>(Func<T, Action> getProcessing); //TODO temp
  }
}
