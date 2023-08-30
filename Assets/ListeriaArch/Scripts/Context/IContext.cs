using System;

namespace ListeriaArch {
  public interface IContext : IRelease {
    ILinksCreator LinksCreator { get; init; }
    ILinksProvider LinksProvider { get; init; }
    
    void Processing<T>(Func<T, Action> getProcessing);
    T Get<T>(); //get or create and resolve

    void Add<T>(T obj);
    void Add(ILinksProvider obj); //Temp
  }
}
